using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public interface IGridFilters
    {
        Func<String> BooleanTrueOptionText { get; set; }
        Func<String> BooleanFalseOptionText { get; set; }
        Func<String> BooleanEmptyOptionText { get; set; }

        IGridFilter Create(Type type, String method, String[] values);
        IEnumerable<SelectListItem> GetFilterOptions<T, TValue>(IGridColumn<T, TValue> column);

        void Register(Type type, String method, Type filter);
        void Unregister(Type type, String method);
    }
}
