﻿using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    [TestFixture]
    public class StringStartsWithFilterTests : BaseGridFilterTests
    {
        #region Method: Apply(Expression expression)

        [Test]
        public void Apply_FiltersItemsWithCaseInsensitiveComparison()
        {
            Expression<Func<GridModel, String>> expression = (model) => model.Name;
            StringStartsWithFilter filter = new StringStartsWithFilter();
            filter.Value = "Test";

            IQueryable<GridModel> items = new[]
            {
                new GridModel { Name = null },
                new GridModel { Name = "Tes" },
                new GridModel { Name = "test" },
                new GridModel { Name = "Test" },
                new GridModel { Name = "TTEST2" }
            }.AsQueryable();

            IQueryable expected = items.Where(model => model.Name != null && model.Name.ToUpper().StartsWith("TEST"));
            IQueryable actual = Filter(items, filter.Apply(expression.Body), expression);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
    }
}
