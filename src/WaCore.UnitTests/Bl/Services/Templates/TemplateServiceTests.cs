using System;
using System.Collections.Generic;
using NSubstitute;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Enums;
using WaCore.Contracts.Exceptions.Templates;
using WaCore.Entities.Core;
using Xunit;

namespace WaCore.UnitTests.Bl.Services.Templates
{
    public class TemplateServiceTests
    {
        private static Contracts.Bl.Services.Templates.IEmailTemplateService GetTemplateService(IEmailTemplateRepository emailTemplateRepoFake = null)
        {
            emailTemplateRepoFake = emailTemplateRepoFake ?? Substitute.For<IEmailTemplateRepository>();
            return new WaCore.Bl.Services.Templates.TemplateService(emailTemplateRepoFake);
        }

        [Fact]
        public void GetEmailMessage_WithNonExistingTemplate_Throws()
        {
            var templateService = GetTemplateService();
            Assert.Throws<TemplateNotFoundException>(() => templateService.GetEmailMessage("nonExistingTemplate", LanguageEnum.EN, null, null));
        }

        [Fact]
        public void GetEmailMessage_WithExistingNameAndNonExistingLanguage_Throws()
        {
            var fakeTemplate = new EmailTemplate
            {
                Name = "testTemplate",
                LanguageEnum = LanguageEnum.EN,
            };
            var repoFake = Substitute.For<IEmailTemplateRepository>();
            repoFake.GetTemplates("testTemplate").Returns(new List<IEmailTemplate>() { fakeTemplate });

            var templateService = GetTemplateService(repoFake);
            Assert.Throws<TemplateNotFoundException>(() => templateService.GetEmailMessage("testTemplate", LanguageEnum.DE, null, null));
        }

        [Fact]
        public void GetEmailMessage_WithExistingTemplate_ReturnsTransformedText()
        {
            var fakeTemplate = new EmailTemplate
            {
                Name = "testTemplate",
                LanguageEnum = LanguageEnum.EN,
                TemplateText = "Hello @Model.Name"
            };
            var repoFake = Substitute.For<IEmailTemplateRepository>();
            repoFake.GetTemplates("testTemplate").Returns(new List<IEmailTemplate>() {fakeTemplate});

            var templateService = GetTemplateService(repoFake);
            var email = templateService.GetEmailMessage("testTemplate", LanguageEnum.EN, new {Name="Max Mustermann"}, null);
            Assert.Equal("Hello Max Mustermann", email.Text);
        }

    }
}
