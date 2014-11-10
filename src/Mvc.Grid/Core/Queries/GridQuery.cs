﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridQuery : NameValueCollection
    {
        public IGrid Grid { get; set; }

        public GridQuery(IGrid grid, NameValueCollection query) : base(query)
        {
            Grid = grid;
        }

        public virtual IGridSortingQuery GetSortingQuery(String columnName)
        {
            return new GridSortingQuery(this, columnName);
        }
        public virtual IGridPagingQuery GetPagingQuery()
        {
            return new GridPagingQuery(this);
        }

        public override String ToString()
        {
            List<String> query = new List<String>();
            foreach (String key in AllKeys)
                foreach (String value in GetValues(key))
                    query.Add(HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value));

            return "?" + String.Join("&", query);
        }
    }
}
