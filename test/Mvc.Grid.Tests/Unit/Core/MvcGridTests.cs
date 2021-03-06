﻿using System;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class MvcGridTests
    {
        [Fact]
        public void MvcGrid_SetsFiltersToDefaultImplementation()
        {
            Type actual = MvcGrid.Filters.GetType();
            Type expected = typeof(GridFilters);

            Assert.Equal(expected, actual);
        }
    }
}
