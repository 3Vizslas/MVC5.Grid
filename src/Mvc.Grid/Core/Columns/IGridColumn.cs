﻿using System;
using System.Linq.Expressions;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn : ISortableColumn
    {
        Boolean IsEncoded { get; set; }
        String CssClasses { get; set; }
        String Format { get; set; }
        String Title { get; set; }
        String Name { get; set; }

        IGridColumn Encoded(Boolean isEncoded);
        IGridColumn Formatted(String format);
        IGridColumn Css(String cssClasses);
        IGridColumn Titled(String title);
        IGridColumn Named(String name);

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<TModel, TValue> : ISortableColumn<TModel>, IGridColumn where TModel : class
    {
        Expression<Func<TModel, TValue>> Expression { get; set; }
        IGrid<TModel> Grid { get; set; }
    }
}
