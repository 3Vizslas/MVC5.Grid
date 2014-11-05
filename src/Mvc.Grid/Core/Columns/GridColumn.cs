﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : BaseGridColumn, IGridColumn<TModel, TValue> where TModel : class
    {
        public Func<TModel, TValue> Expression { get; set; }
        public GridProcessorType Type { get; set; }
        public IGrid<TModel> Grid { get; set; }

        public GridColumn(IGrid<TModel> grid, Expression<Func<TModel, TValue>> expression)
        {
            Grid = grid;
            IsEncoded = true;
            Type = GridProcessorType.Pre;
            Expression = expression.Compile();
            Name = ExpressionHelper.GetExpressionText(expression);
            SortOrder = grid.Query.GetSortingQuery(Name).SortOrder;
        }

        public IEnumerable<TModel> Process(IEnumerable<TModel> items)
        {
            if (IsSortable != true)
                return items;

            if (SortOrder == null)
                return items;

            if (SortOrder == GridSortOrder.Asc)
                return items.OrderBy(Expression);
            
            return items.OrderByDescending(Expression);
        }

        public override IHtmlString ValueFor(IGridRow row)
        {
            String value = GetRawValueFor(row);
            if (IsEncoded) value = WebUtility.HtmlEncode(value);

            return new HtmlString(value);
        }

        private String GetRawValueFor(IGridRow row)
        {
            TValue value = Expression(row.Model as TModel);
            if (value == null)
                return String.Empty;

            if (Format == null)
                return value.ToString();

            return String.Format(Format, value);
        }
    }
}
