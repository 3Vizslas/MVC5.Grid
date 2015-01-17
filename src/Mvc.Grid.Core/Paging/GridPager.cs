﻿using System;
using System.Linq;

namespace NonFactors.Mvc.Grid
{
    public class GridPager<T> : IGridPager<T>
    {
        public GridProcessorType ProcessorType { get; set; }
        public virtual Int32 PagesToDisplay { get; set; }
        public virtual Int32 RowsPerPage { get; set; }
        public virtual Int32 TotalRows { get; set; }
        public String PartialViewName { get; set; }
        public String CssClasses { get; set; }
        public IGrid<T> Grid { get; set; }

        private Boolean CurrentPageIsFromQuery { get; set; }
        private Int32 CurrentPageValue { get; set; }
        public virtual Int32 StartingPage
        {
            get
            {
                Int32 middlePage = (PagesToDisplay / 2) + (PagesToDisplay % 2);
                if (CurrentPage < middlePage)
                    return 1;

                if (CurrentPage - middlePage + PagesToDisplay > TotalPages)
                    return Math.Max(TotalPages - PagesToDisplay + 1, 1);

                return CurrentPage - middlePage + 1;
            }
        }
        public virtual Int32 CurrentPage
        {
            get
            {
                if (CurrentPageIsFromQuery)
                    return CurrentPageValue;

                String key = Grid.Name + "-Page";
                String value = Grid.Query[key];
                Int32 page;

                if (Int32.TryParse(value, out page))
                    CurrentPageValue = page;

                CurrentPageValue = CurrentPageValue <= 0 ? 1 : CurrentPageValue;
                CurrentPageIsFromQuery = true;

                return CurrentPageValue;
            }
            set
            {
                CurrentPageIsFromQuery = false;
                CurrentPageValue = value;
            }
        }
        public virtual Int32 TotalPages
        {
            get
            {
                return (Int32)(Math.Ceiling(TotalRows / (Double)RowsPerPage));
            }
        }

        public GridPager(IGrid<T> grid)
        {
            Grid = grid;
            RowsPerPage = 20;
            PagesToDisplay = 5;
            PartialViewName = "MvcGrid/_Pager";
            ProcessorType = GridProcessorType.Post;
        }

        public virtual IQueryable<T> Process(IQueryable<T> items)
        {
            TotalRows = items.Count();

            return items.Skip((CurrentPage - 1) * RowsPerPage).Take(RowsPerPage);
        }
    }
}
