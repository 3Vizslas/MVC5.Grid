﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public abstract class BaseGridColumn<T, TValue> : IGridColumn<T>
    {
        LambdaExpression IGridColumn<T>.Expression { get { return Expression; } }
        public Expression<Func<T, TValue>> Expression { get; set; }
        public Func<T, TValue> ExpressionValue { get; set; }
        public GridProcessorType ProcessorType { get; set; }
        public Func<T, Object> RenderValue { get; set; }
        public IGrid<T> Grid { get; set; }

        public virtual GridSortOrder? FirstSortOrder { get; set; }
        public virtual GridSortOrder? SortOrder { get; set; }
        public virtual Boolean? IsSortable { get; set; }

        public virtual IGridFilter<T> Filter { get; set; }
        public virtual Boolean? IsFilterable { get; set; }
        public virtual String FilterValue { get; set; }
        public virtual String FilterType { get; set; }
        public virtual String FilterName { get; set; }

        public Boolean IsEncoded { get; set; }
        public String CssClasses { get; set; }
        public String Format { get; set; }
        public String Title { get; set; }
        public String Name { get; set; }

        public virtual IGridColumn<T> RenderedAs(Func<T, Object> value)
        {
            RenderValue = value;

            return this;
        }

        public virtual IGridColumn<T> Filterable(Boolean isFilterable)
        {
            IsFilterable = isFilterable;

            return this;
        }
        public virtual IGridColumn<T> FilteredAs(String filterName)
        {
            FilterName = filterName;

            return this;
        }

        public virtual IGridColumn<T> FirstSortIn(GridSortOrder order)
        {
            FirstSortOrder = order;

            return this;
        }
        public virtual IGridColumn<T> Sortable(Boolean isSortable)
        {
            IsSortable = isSortable;

            return this;
        }

        public virtual IGridColumn<T> Encoded(Boolean isEncoded)
        {
            IsEncoded = isEncoded;

            return this;
        }
        public virtual IGridColumn<T> Formatted(String format)
        {
            Format = format;

            return this;
        }
        public virtual IGridColumn<T> Css(String cssClasses)
        {
            CssClasses = cssClasses;

            return this;
        }
        public virtual IGridColumn<T> Titled(String title)
        {
            Title = title;

            return this;
        }
        public virtual IGridColumn<T> Named(String name)
        {
            Name = name;

            return this;
        }

        public abstract IQueryable<T> Process(IQueryable<T> items);
        public abstract IHtmlString ValueFor(IGridRow row);
    }
}
