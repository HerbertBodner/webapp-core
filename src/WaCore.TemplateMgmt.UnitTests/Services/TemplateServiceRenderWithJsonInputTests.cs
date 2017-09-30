using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WaCore.Contracts.Data;
using WaCore.TemplateMgmt.Engine.Stubble;

namespace WaCore.TemplateMgmt.UnitTests.Services
{
    [TestClass]
    public class TemplateServiceRenderWithJsonInputTests
    {
        [TestMethod]
        public void RenderWithJsonInputWithComplicatedTypeReturnsCorrectString()
        {
            var templateContent = "{{#Complicated2}}" +
                    "{{Name2}} {{#Complicated3}}{{Nr3}}{{/Complicated3}}" +
                "{{/Complicated2}}";

            var jsonInput = "{\"Complicated2\":{" +
                "\"Name2\":\"john\"," +
                "\"Complicated3\":{" +
                    "\"Nr3\":12345}}}";

            var templateServiceFake = GetTemplateServiceFake();
            string result = templateServiceFake.RenderWithJsonInput<Complicated>(templateContent, jsonInput);

            Assert.AreEqual("john 12345", result);
        }

        private ITemplateServiceFake GetTemplateServiceFake()
        {
            var templateServiceFake = Substitute.For<TemplateServiceFake>(Substitute.For<IWacUnitOfWork>(), new StubbleEngine());

            return templateServiceFake;
        }
    }
}
