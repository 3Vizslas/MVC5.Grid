using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid
{
    public class GridHtmlAttributes : Dictionary<String, Object?>, IHtmlString
    {
        public GridHtmlAttributes()
        {
        }
        public GridHtmlAttributes(Object? attributes)
            : base(HtmlHelper.AnonymousObjectToHtmlAttributes(attributes))
        {
        }

        public String ToHtmlString()
        {
            StringBuilder html = new StringBuilder();
            foreach (KeyValuePair<String, Object?> attribute in this)
            {
                if (attribute.Value == null)
                    continue;

                html.Append(" ");
                html.Append(attribute.Key);
                html.Append("=\"");

                html.Append(WebUtility.HtmlEncode(attribute.Value.ToString()));

                html.Append("\"");
            }

            return html.ToString();
        }
    }
}
