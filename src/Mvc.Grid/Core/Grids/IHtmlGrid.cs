﻿using System;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public interface IHtmlGrid<TModel> : IHtmlString where TModel : class
    {
        String PartialViewName { get; set; }
        IGrid<TModel> Grid { get; }

        IHtmlGrid<TModel> Build(Action<IGridColumns<TModel>> builder);
        IHtmlGrid<TModel> Sortable(Boolean isSortable);
        IHtmlGrid<TModel> Empty(String text);
        IHtmlGrid<TModel> Named(String name);

        IHtmlGrid<TModel> WithPager(Action<IGridPager<TModel>> builder);
        IHtmlGrid<TModel> WithPager();
    }
}
