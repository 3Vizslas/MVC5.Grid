﻿using NSubstitute;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Web.Mvc;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class HtmlGridTests
    {
        private HtmlGrid<GridModel> htmlGrid;

        public HtmlGridTests()
        {
            HtmlHelper html = HtmlHelperFactory.CreateHtmlHelper("id=3&name=jim");
            IGrid<GridModel> grid = new Grid<GridModel>(new GridModel[8]);

            htmlGrid = new HtmlGrid<GridModel>(html, grid);

            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Sum);
        }

        [Fact]
        public void HtmlGrid_DoesNotChangeQuery()
        {
            Object? expected = htmlGrid.Grid.Query = new NameValueCollection();
            Object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.Query;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsGridQuery()
        {
            Object? expected = htmlGrid.Html.ViewContext.HttpContext.Request.QueryString;
            Object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.Query;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_DoesNotChangeViewContext()
        {
            htmlGrid.Grid.ViewContext = new ViewContext();

            Object? expected = htmlGrid.Grid.ViewContext;
            Object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.ViewContext;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsViewContext()
        {
            htmlGrid.Grid.ViewContext = null;

            Object? expected = htmlGrid.Html.ViewContext;
            Object? actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid.ViewContext;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsPartialViewName()
        {
            String actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).PartialViewName;
            String expected = "MvcGrid/_Grid";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsHtml()
        {
            Object actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Html;
            Object expected = htmlGrid.Html;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void HtmlGrid_SetsGrid()
        {
            Object actual = new HtmlGrid<GridModel>(htmlGrid.Html, htmlGrid.Grid).Grid;
            Object expected = htmlGrid.Grid;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void ToHtmlString_RendersPartialView()
        {
            IView view = Substitute.For<IView>();
            IViewEngine engine = Substitute.For<IViewEngine>();
            ViewEngineResult result = Substitute.For<ViewEngineResult>(view, engine);
            engine.FindPartialView(Arg.Any<ControllerContext>(), htmlGrid.PartialViewName, Arg.Any<Boolean>()).Returns(result);
            view.When(sub => sub.Render(Arg.Any<ViewContext>(), Arg.Any<TextWriter>())).Do(sub =>
            {
                Assert.Equal(htmlGrid.Grid, sub.Arg<ViewContext>().ViewData.Model);
                sub.Arg<TextWriter>().Write("Rendered");
            });

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine);

            String actual = htmlGrid.ToHtmlString();
            String expected = "Rendered";

            Assert.Equal(expected, actual);
        }
    }
}
