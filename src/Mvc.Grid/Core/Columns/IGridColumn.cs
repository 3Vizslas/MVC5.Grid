﻿using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IGridColumn : ISortableColumn
    {
        Boolean IsEncoded { get; set; }
        String CssClasses { get; set; }
        String Format { get; set; }
        String Title { get; set; }

        IGridColumn Formatted(String format);
        IGridColumn Encoded(Boolean encode);
        IGridColumn Css(String cssClasses);
        IGridColumn Titled(String title);

        IHtmlString ValueFor(IGridRow row);
    }

    public interface IGridColumn<TModel, TValue> : ISortableColumn<TModel>, IGridColumn where TModel : class
    {
        Func<TModel, TValue> Expression { get; set; }
    }
}
