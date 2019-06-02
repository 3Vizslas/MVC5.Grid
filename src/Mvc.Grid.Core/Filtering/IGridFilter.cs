using System;
using System.Linq.Expressions;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilter
    {
        String Method { get; set; }
        String[] Values { get; set; }

        Expression Apply(Expression expression);
    }
}
