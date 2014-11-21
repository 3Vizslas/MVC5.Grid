﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NonFactors.Mvc.Grid
{
    public class StringContainsFilter<TModel> : BaseGridFilter<TModel> where TModel : class
    {
        public override IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            Expression value = Expression.Constant(FilterValue);
            MethodInfo method = typeof(String).GetMethod("Contains");
            ParameterExpression parameter = FilteredExpression.Parameters[0];

            Expression notEqual = Expression.NotEqual(FilteredExpression.Body, Expression.Constant(null));
            Expression contains = Expression.Call(FilteredExpression.Body, method, value);
            Expression andAlso = Expression.AndAlso(notEqual, contains);

            Expression<Func<TModel, Boolean>> filter = Expression.Lambda<Func<TModel, Boolean>>(andAlso, parameter);

            return items.Where(filter);
        }
    }
}
