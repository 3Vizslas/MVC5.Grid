﻿using System;
using System.Linq;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridPager<TModel> : IGridPager<TModel> where TModel : class
    {
        public String PartialViewName { get; set; }
        public IGrid<TModel> Grid { get; set; }

        public GridProcessorType Type { get; set; }
        public Int32 PagesToDisplay { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 RowsPerPage { get; set; }
        public Int32 TotalRows { get; set; }
        public Int32 StartingPage
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
        public Int32 TotalPages
        {
            get
            {
                return (Int32)(Math.Ceiling(TotalRows / (Double)RowsPerPage));
            }
        }

        public GridPager(IGrid<TModel> grid)
        {
            PartialViewName = "MvcGrid/_Pager";

            CurrentPage = grid.Query.GetPagingQuery().CurrentPage;
            TotalRows = grid.Source.Count();
            Type = GridProcessorType.Post;
            PagesToDisplay = 5;
            RowsPerPage = 20;
            Grid = grid;
        }

        public IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            return items.Skip((CurrentPage - 1) * RowsPerPage).Take(RowsPerPage);
        }

        public String LinkForPage(Int32 page)
        {
            GridQuery query = new GridQuery(Grid, Grid.Query);
            query[Grid.Name + "-Page"] = page.ToString();

            return "?" + String.Join("&", query.AllKeys.Select(key => HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(query[key])));
        }
    }
}
