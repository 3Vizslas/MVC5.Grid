﻿using System;

namespace NonFactors.Mvc.Grid
{
    public interface IGridPager
    {
        String PartialViewName { get; set; }
        String CssClasses { get; set; }

        Int32 PagesToDisplay { get; set; }
        Int32 RowsPerPage { get; set; }
        Int32 InitialPage { get; set; }
        Int32 FirstDisplayPage { get; }
        Int32 CurrentPage { get; }
        Int32 TotalPages { get; }
        Int32 TotalRows { get; }
    }

    public interface IGridPager<T> : IGridProcessor<T>, IGridPager
    {
        IGrid<T> Grid { get; }
    }
}
