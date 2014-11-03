﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridColumnTests
    {
        private GridColumn<GridModel, Object> column;

        [SetUp]
        public void SetUp()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);
        }

        #region Constructor: GridColumn(Func<TModel, TValue> expression)

        [Test]
        public void GridColumn_Expression_SetsTypeAsPreProcessor()
        {
            GridProcessorType actual = new GridColumn<GridModel, Object>(model => model.Name).Type;
            GridProcessorType expected = GridProcessorType.Pre;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridColumn_Expression_SetsExpression()
        {
            Func<GridModel, String> expression = (model) => model.Name;

            Func<GridModel, String> actual = new GridColumn<GridModel, String>(expression).Expression;
            Func<GridModel, String> expected = expression;

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GridColumn_Expression_SetsIsEncodedToTrue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);

            Assert.IsTrue(column.IsEncoded);
        }

        #endregion

        #region Method: Process(IEnumerable<TModel> items)

        [Test]
        public void Process_OnExpressionNullReturnsSameItems()
        {
            column.Expression = null;
            column.IsSortable = true;
            column.SortOrder = GridSortOrder.Asc;

            IEnumerable<GridModel> expected = new GridModel[2];
            IEnumerable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_OnIsSortableNullReturnsSameItems()
        {
            column.IsSortable = null;
            column.SortOrder = GridSortOrder.Desc;
            column.Expression = (model) => model.Name;

            IEnumerable<GridModel> expected = new GridModel[2];
            IEnumerable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_OnIsSortableFalseReturnsSameItems()
        {
            column.IsSortable = false;
            column.SortOrder = GridSortOrder.Desc;
            column.Expression = (model) => model.Name;

            IEnumerable<GridModel> expected = new GridModel[2];
            IEnumerable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_OnSortOrderNullReturnsSameItems()
        {
            column.SortOrder = null;
            column.IsSortable = true;
            column.Expression = (model) => model.Name;

            IEnumerable<GridModel> expected = new GridModel[2];
            IEnumerable<GridModel> actual = column.Process(expected);

            Assert.AreSame(expected, actual);
        }

        [Test]
        public void Process_ReturnsItemsSortedByAscendingOrder()
        {
            column.IsSortable = true;
            column.SortOrder = GridSortOrder.Asc;
            column.Expression = (model) => model.Name;
            GridModel[] models = { new GridModel { Name = "B" }, new GridModel { Name = "A" }};

            IEnumerable<GridModel> expected = models.OrderBy(model => model.Name);
            IEnumerable<GridModel> actual = column.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Process_ReturnsItemsSortedByDescendingOrder()
        {
            column.IsSortable = true;
            column.SortOrder = GridSortOrder.Desc;
            column.Expression = (model) => model.Name;
            GridModel[] models = { new GridModel { Name = "A" }, new GridModel { Name = "B" } };

            IEnumerable<GridModel> expected = models.OrderByDescending(model => model.Name);
            IEnumerable<GridModel> actual = column.Process(models);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Method: ValueFor(IGridRow row)

        [Test]
        public void ValueFor_OnNullFormatReturnsEncodedValue()
        {
            IGridRow row = new GridRow(new GridModel { Name = "<script />" });
            column = new GridColumn<GridModel, Object>(model => model.Name);
            column.Formatted(null);
            column.Encoded(true);

            String expected = WebUtility.HtmlEncode("<script />");
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_ReturnsEncodedAndFormattedValue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Sum);
            IGridRow row = new GridRow(new GridModel { Sum = 100 });
            column.Formatted("<script value='{0:C2}' />");
            column.Encoded(true);

            String expected = WebUtility.HtmlEncode(String.Format("<script value='{0:C2}' />", 100));
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullExpressionReturnsEmpty()
        {
            column = new GridColumn<GridModel, Object>(null);

            String actual = column.ValueFor(null).ToString();
            String expected = String.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullValueReturnsEmpty()
        {
            column = new GridColumn<GridModel, Object>(model => model.Name);
            IGridRow row = new GridRow(new GridModel { Name = null });

            String actual = column.ValueFor(row).ToString();
            String expected = String.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_OnNullFormatReturnsNotEncodedValue()
        {
            IGridRow row = new GridRow(new GridModel { Name = "<script />" });
            column = new GridColumn<GridModel, Object>(model => model.Name);
            column.Formatted(null);
            column.Encoded(false);

            String actual = column.ValueFor(row).ToString();
            String expected = "<script />";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValueFor_ReturnsNotEncodedButFormattedValue()
        {
            column = new GridColumn<GridModel, Object>(model => model.Sum);
            IGridRow row = new GridRow(new GridModel { Sum = 100 });
            column.Formatted("<script value='{0:C2}' />");
            column.Encoded(false);

            String expected = String.Format("<script value='{0:C2}' />", 100);
            String actual = column.ValueFor(row).ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
