﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public class BooleanFilter<TModel> : BaseGridFilter<TModel> where TModel : class
    {
        public override IQueryable<TModel> Process(IQueryable<TModel> items)
        {
            Expression<Func<TModel, Boolean>> filter = GetFilterExpression();
            if (filter == null) return items;

            return items.Where(filter);
        }

        private Expression<Func<TModel, Boolean>> GetFilterExpression()
        {
            Object value = GetBooleanValue();
            if (value == null) return null;

            Expression expression = Expression.Equal(FilteredExpression.Body, Expression.Constant(value));

            return Expression.Lambda<Func<TModel, Boolean>>(expression, FilteredExpression.Parameters[0]);
        }
        private Object GetBooleanValue()
        {
            if (String.Equals(Value, "true", StringComparison.InvariantCultureIgnoreCase))
                return true;

            if (String.Equals(Value, "false", StringComparison.InvariantCultureIgnoreCase))
                return false;

            return null;
        }
    }
}
