﻿using System.Collections.Generic;

namespace NonFactors.Mvc.Grid
{
    public interface IGridRows : IEnumerable<IGridRow>
    {
    }

    public interface IGridRows<T> : IGridRows where T : class
    {

    }
}
