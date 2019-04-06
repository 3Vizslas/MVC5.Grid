using NSubstitute;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Tests
{
    public class HtmlHelperFactory
    {
        public static HtmlHelper CreateHtmlHelper(String query)
        {
            ViewContext context = CreateViewContext(CreateControllerContext(query));

            return new HtmlHelper(context, new ViewPage { ViewData = context.ViewData });
        }

        private static ControllerContext CreateControllerContext(String query)
        {
            HttpContextBase http = HttpContextFactory.CreateHttpContextBase(query);

            return new ControllerContext(http, http.Request.RequestContext.RouteData, Substitute.For<ControllerBase>());
        }
        private static ViewContext CreateViewContext(ControllerContext controller)
        {
            ViewContext context = new ViewContext(
                controller,
                Substitute.For<IView>(),
                new ViewDataDictionary(),
                new TempDataDictionary(),
                new StringWriter());

            return context;
        }
    }
}