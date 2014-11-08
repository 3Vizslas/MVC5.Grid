﻿using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridSortingQueryTests
    {
        #region Constructor: GridSortingQuery(IGridQuery query, String columnName)

        [Test]
        [TestCase("Column", "MG-Sort-Column=Asc", null)]
        [TestCase("Column", "MG-Sort-Grid -Column=", null)]
        [TestCase("Column", "MG-Sort-Grid -Column=Asc", GridSortOrder.Asc)]
        [TestCase("Column", "MG-Sort-Grid -Column=Desc", GridSortOrder.Desc)]
        public void GridSortingQuery_SetsSortOrder(String columnName, String query, GridSortOrder? expected)
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = HttpUtility.ParseQueryString(query);
            gridQuery.Grid.Name = "Grid ";

            GridSortOrder? actual = new GridSortingQuery(gridQuery, columnName).SortOrder;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridSortingQuery_SetsColumnName()
        {
            IGridQuery gridQuery = Substitute.For<IGridQuery>();
            gridQuery.Query = new NameValueCollection();

            String actual = new GridSortingQuery(gridQuery, "ColumnName").ColumnName;
            String expected = "ColumnName";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
