using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Dtos.Filters;
using WaCore.TemplateMgmt.Engine.Stubble;

namespace WaCore.TemplateMgmt.UnitTests
{
    [TestClass]
    public class TemplateServiceTests
    {
        [TestMethod]
        public void RenderWithSimpleObjectReturnsCorrectString()
        {
            var template = new Template
            {
                Content = "{{name}}"
            };
            var templateServiceFake = GetTemplateServiceFake(template);
            var result = templateServiceFake.Render(1, new { name = "Herbi" });

            Assert.AreEqual("Herbi", result);
        }


        [TestMethod]
        public async Task RenderWithComplexObjectReturnsCorrectString()
        {
            var template = new Template
            {
                Content =
                "{{#customer}}" +
                    "{{firstName}} {{lastName}}, living in {{#address}}{{city}}, {{street}}{{/address}}, born on {{birthday}} " +
                    "has following phones: {{#contactList}}{{type}}: {{phone}}; {{/contactList}}" +
                "{{/customer}}"
            };
            var templateServiceFake = GetTemplateServiceFake(template);
            var result = await templateServiceFake.RenderAsync(1,
                new
                {
                    customer = new
                    {
                        firstName = "John",
                        lastName = "Doe",
                        address = new
                        {
                            street = "Street 1",
                            city = "Amsterdam"
                        },
                        birthday = "2017-01-01",
                        contactList = new[]
                        {
                            new { type = "Private", phone="12345" },
                            new { type = "Work", phone="54321" }
                        }
                    }
                });

            Assert.AreEqual("John Doe, living in Amsterdam, Street 1, born on 2017-01-01 " +
                "has following phones: Private: 12345; Work: 54321; ", result);
        }


        private ITemplateServiceFake GetTemplateServiceFake(Template template)
        {
            var templateRepoFake = Substitute.For<IWacListDataRepository<Template, WacFilter>>();
            templateRepoFake.Get(Arg.Any<object>()).Returns(template);
            templateRepoFake.GetAsync(Arg.Any<object>()).Returns(template);

            var uow = Substitute.For<IWacUnitOfWork>();
            uow.GetRepository<IWacListDataRepository<Template, WacFilter>>().Returns(templateRepoFake);

            var templateServiceFake = Substitute.For<TemplateServiceFake>(uow, new StubbleEngine());
            
            return templateServiceFake;
        }
    }
}
