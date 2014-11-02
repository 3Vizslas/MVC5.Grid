﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridColumn<TModel, TValue> : BaseGridColumn, IGridColumn<TModel, TValue> where TModel : class
    {
        public Func<TModel, TValue> Expression { get; set; }
        public GridProcessorType Type { get; set; }

        public GridColumn() : this(null)
        {
        }
        public GridColumn(Func<TModel, TValue> expression)
        {
            Type = GridProcessorType.Pre;
            Expression = expression;
            IsEncoded = true;
        }

        public IEnumerable<TModel> Process(IEnumerable<TModel> items)
        {
            if (Expression == null)
                return items;

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
            if (Expression == null)
                return String.Empty;

            TValue value = Expression(row.Model as TModel);
            if (value == null)
                return String.Empty;

            if (Format == null)
                return value.ToString();

            return String.Format(Format, value);
        }
    }
}
