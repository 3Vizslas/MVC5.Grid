using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NonFactors.Mvc.Grid
{
    public class StringContainsFilter : BaseGridFilter
    {
        public override Expression Apply(Expression expression)
        {
            if (Values.Length == 0 || Values.Any(value => String.IsNullOrEmpty(value)))
                return null;

            return base.Apply(expression);
        }

        protected override Expression Apply(Expression expression, String value)
        {
            Expression valueExpression = Expression.Constant(value.ToUpper());
            MethodInfo containsMethod = typeof(String).GetMethod("Contains");
            MethodInfo toUpperMethod = typeof(String).GetMethod("ToUpper", new Type[0]);

            Expression toUpper = Expression.Call(expression, toUpperMethod);
            Expression contains = Expression.Call(toUpper, containsMethod, valueExpression);
            Expression notNull = Expression.NotEqual(expression, Expression.Constant(null, expression.Type));

            return Expression.AndAlso(notNull, contains);
        }
    }
}
