using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<T, TValue> : IGridColumn<T, TValue> where T : class
    {
        public IGrid<T> Grid { get; set; }

        public String Name { get; set; }
        public Object Title { get; set; }
        public String Format { get; set; }
        public Boolean IsHidden { get; set; }
        public String CssClasses { get; set; }
        public Boolean IsEncoded { get; set; }
        public GridProcessorType ProcessorType { get; set; }

        public Func<T, TValue> ExpressionValue { get; set; }
        public Func<T, Int32, Object> RenderValue { get; set; }
        public Expression<Func<T, TValue>> Expression { get; set; }

        IGridColumnSort IGridColumn.Sort => Sort;
        public virtual IGridColumnSort<T, TValue> Sort { get; set; }

        IGridColumnFilter IGridColumn.Filter => Filter;
        public virtual IGridColumnFilter<T, TValue> Filter { get; set; }

        public GridColumn(IGrid<T> grid, Expression<Func<T, TValue>> expression)
        {
            Grid = grid;
            IsEncoded = true;
            Expression = expression;
            Name = NameFor(expression);
            Title = TitleFor(expression);
            ProcessorType = GridProcessorType.Pre;
            ExpressionValue = expression.Compile();
            Sort = new GridColumnSort<T, TValue>(this);
            Filter = new GridColumnFilter<T, TValue>(this);
        }

        public virtual IQueryable<T> Process(IQueryable<T> items)
        {
            return Sort.Apply(Filter.Apply(items));
        }
        public virtual IHtmlString ValueFor(IGridRow<Object> row)
        {
            Object value = ColumnValueFor(row);

            if (value == null)
                return MvcHtmlString.Empty;

            if (value is IHtmlString content)
                return content;

            if (Format != null)
                value = String.Format(Format, value);

            if (IsEncoded)
                return new HtmlString(WebUtility.HtmlEncode(value.ToString()));

            return new HtmlString(value.ToString());
        }

        private String TitleFor(Expression<Func<T, TValue>> expression)
        {
            MemberExpression body = expression.Body as MemberExpression;
            DisplayAttribute display = body?.Member.GetCustomAttribute<DisplayAttribute>();

            return display?.GetShortName() ?? "";
        }
        private String NameFor(Expression<Func<T, TValue>> expression)
        {
            String text = ExpressionHelper.GetExpressionText(expression).Replace("_", "-");

            return String.Join("-", Regex.Split(text, "(?<=[a-zA-Z])(?=[A-Z])")).ToLower();
        }
        private Object ColumnValueFor(IGridRow<Object> row)
        {
            try
            {
                if (RenderValue != null)
                    return RenderValue(row.Model as T, row.Index);

                Type type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
                if (type.GetTypeInfo().IsEnum)
                    return EnumValue(type, ExpressionValue(row.Model as T).ToString());

                return ExpressionValue(row.Model as T);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
        private String EnumValue(Type type, String value)
        {
            return type
                .GetMember(value)
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName() ?? value;
        }
    }
}
