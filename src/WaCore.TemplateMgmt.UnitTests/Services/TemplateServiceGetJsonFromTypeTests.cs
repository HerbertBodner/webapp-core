using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WaCore.Contracts.Data;
using WaCore.TemplateMgmt.Contracts.Engine;
using WaCore.TemplateMgmt.Services.Attributes;

namespace WaCore.TemplateMgmt.UnitTests.Services
{
    [TestClass]
    public class TemplateServiceGetJsonFromTypeTests
    {
        [TestMethod]
        public void GetJsonFromSimpleTypeReturnsCorrectString()
        {
            var templateServiceFake = GetTemplateServiceFake();
            string result = templateServiceFake.GetJsonFromType(typeof(Simple), false);

            Assert.AreEqual("{\"Nr\":0," +
                "\"NrWithDefault\":1," +
                "\"Name\":null," +
                "\"NameWithDefault\":\"John Doe\"}", result);
        }


        [TestMethod]
        public void GetJsonFromComplicatedTypeReturnsCorrectString()
        {
            var templateServiceFake = GetTemplateServiceFake();
            string result = templateServiceFake.GetJsonFromType(typeof(Complicated), false);

            Assert.AreEqual("{\"Complicated2\":{" +
                "\"Name2\":null," +
                "\"Complicated3\":{" +
                    "\"Nr3\":123}}}", result);
        }

        

        private ITemplateServiceFake GetTemplateServiceFake()
        {
            var templateServiceFake = Substitute.For<TemplateServiceFake>(Substitute.For<IWacUnitOfWork>(), Substitute.For<ITemplateEngine>());

            return templateServiceFake;
        }
    }

    public class Simple
    {
        public int Nr { get; set; }

        [TemplateTest(Value = 1)]
        public int NrWithDefault { get; set; }

        public string Name { get; set; }

        [TemplateTest(Value = "John Doe")]
        public string NameWithDefault { get; set; }

    }


    public class Complicated
    {
        public Complicated2 Complicated2 { get; set; }
    }

    public class Complicated2
    {
        public string Name2 { get; set; }
        public Complicated3 Complicated3 { get; set; }
    }

    public class Complicated3
    {
        [TemplateTest(Value = 123)]
        public int Nr3 { get; set; }
    }

}
