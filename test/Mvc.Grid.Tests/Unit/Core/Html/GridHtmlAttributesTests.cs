using System;
using Xunit;

namespace NonFactors.Mvc.Grid.Tests.Unit
{
    public class GridHtmlAttributesTests
    {
        [Fact]
        public void GridHtmlAttributes_Empty()
        {
            Assert.Empty(new GridHtmlAttributes());
        }

        [Fact]
        public void GridHtmlAttributes_ChangesUnderscoresToDashes()
        {
            String expected = " id=\"\" src=\"test.png\" data-temp=\"10000\"";
            String actual = new GridHtmlAttributes(new
            {
                id = "",
                src = "test.png",
                data_temp = 10000,
                data_null = (String?)null
            }).ToHtmlString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteTo_EncodesValues()
        {
            String actual = new GridHtmlAttributes(new { value = "Temp \"str\"" }).ToHtmlString();
            String expected = " value=\"Temp &quot;str&quot;\"";

            Assert.Equal(expected, actual);
        }
    }
}
