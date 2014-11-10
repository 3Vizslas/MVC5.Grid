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
        #region Constructor: GridSortingQuery(GridQuery query, String columnName)

        [Test]
        [TestCase("Column", "Sort-Column=Asc", null)]
        [TestCase("Column", "Grid -Sort=Col&Grid -Order=", null)]
        [TestCase("Column", "Grid -Sort=Column&Grid -Order=Asc", GridSortOrder.Asc)]
        [TestCase("Column", "Grid -Sort=Column&Grid -Order=Desc", GridSortOrder.Desc)]
        public void GridSortingQuery_SetsSortOrder(String columnName, String query, GridSortOrder? expected)
        {
            GridQuery gridQuery = new GridQuery(Substitute.For<IGrid>(), HttpUtility.ParseQueryString(query));
            gridQuery.Grid.Name = "Grid ";

            GridSortOrder? actual = new GridSortingQuery(gridQuery, columnName).SortOrder;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridSortingQuery_SetsColumnName()
        {
            GridQuery gridQuery = new GridQuery(Substitute.For<IGrid>(), new NameValueCollection());

            String actual = new GridSortingQuery(gridQuery, "ColumnName").ColumnName;
            String expected = "ColumnName";

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
