﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public interface IGrid
    {
        NameValueCollection Query { get; set; }

        String DataSourceUrl { get; set; }
        String CssClasses { get; set; }
        String EmptyText { get; set; }
        String Name { get; set; }

        IGridColumns Columns { get; }
        IGridRows Rows { get; }

        IGridPager Pager { get; }
    }

    public interface IGrid<T> : IGrid
    {
        IList<IGridProcessor<T>> Processors { get; set; }
        IQueryable<T> Source { get; set; }

        new IGridColumns<T> Columns { get; }
        new IGridRows<T> Rows { get; }

        new IGridPager<T> Pager { get; set; }
    }
}
