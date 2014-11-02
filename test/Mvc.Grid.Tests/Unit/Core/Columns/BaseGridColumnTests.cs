﻿using NSubstitute;
using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class BaseGridColumnTests
    {
        private BaseGridColumn column;

        [SetUp]
        public void SetUp()
        {
            column = Substitute.For<BaseGridColumn>();
        }

        #region Method: Sortable(Boolean enabled)

        [Test]
        public void Sortable_SetsIsSortable()
        {
            Boolean? actual = column.Sortable(true).IsSortable;
            Boolean? expected = true;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Sortable_ReturnsSameGrid()
        {
            IGridColumn actual = column.Sortable(false);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Formatted(String format)

        [Test]
        public void Formatted_SetsFormat()
        {
            String actual = column.Formatted("Format").Format;
            String expected = "Format";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Formatted_ReturnsSameGrid()
        {
            IGridColumn actual = column.Formatted("Format");
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Encoded(Boolean encode)

        [Test]
        public void Encoded_SetsIsEncoded()
        {
            Boolean actual = column.Encoded(false).IsEncoded;
            Boolean expected = false;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Encoded_ReturnsSameGrid()
        {
            IGridColumn actual = column.Encoded(false);
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Css(String cssClasses)

        [Test]
        public void Css_SetsCssClasses()
        {
            String actual = column.Css("column-class").CssClasses;
            String expected = "column-class";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Css_ReturnsSameGrid()
        {
            IGridColumn actual = column.Css("column-class");
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region Method: Titled(String title)

        [Test]
        public void Titled_SetsTitle()
        {
            String actual = column.Titled("Title").Title;
            String expected = "Title";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Titled_ReturnsSameGrid()
        {
            IGridColumn actual = column.Titled("Title");
            IGridColumn expected = column;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
