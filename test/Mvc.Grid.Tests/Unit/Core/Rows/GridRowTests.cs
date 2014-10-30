﻿using NonFactors.Mvc.Grid.Tests.Objects;
using NUnit.Framework;
using System;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class GridRowTests
    {
        #region Constructor: GridRow(Object model)

        [Test]
        public void GridRow_SetsModel()
        {
            Object expected = new GridModel();
            Object actual = new GridRow(expected).Model;

            Assert.AreSame(expected, actual);
        }

        #endregion
    }
}
