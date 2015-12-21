using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Enums;
using WaCore.Contracts.Bl.Services.Templates;
using WaCore.Contracts.Exceptions.Templates;
#if DNX451
using RazorEngine;
using RazorEngine.Templating;
#endif


namespace WaCore.Bl.Services.Templates
{
    public class TemplateService : Contracts.Bl.Services.Templates.IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        public TemplateService(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }


        public IEmailMessage GetEmailMessage(string templateName, LanguageEnum language, object inputObject,
            List<Guid> corellationIds)
        {

            var template = GetEmailTemplate(templateName, language);

            var messageText = GetTextFromTemplate(template.TemplateText, template.Id.ToString(), inputObject);

            return new EmailMessage
            {
                From = template.From,
                To = template.To,
                Cc = template.Cc,
                Bcc = template.Bcc,
                Subject = template.Subject,
                TemplateId = template.Id,
                Text = messageText
            };
        }

        private string GetTextFromTemplate(string templateText, string templateKey, object inputObject)
        {
#if DNX451
            var text = Engine.Razor.RunCompile(templateText, templateKey, null, inputObject);
            return text;
#endif
#if DNXCORE50
            //TODO: RazorEngine does not work for DNXCORE50 until yet
            return templateText;
#endif
        }

        private IEmailTemplate GetEmailTemplate(string templateName, LanguageEnum language)
        {
            var templates = _emailTemplateRepository.GetTemplates(templateName);

            if (templates == null)
            {
                throw new TemplateNotFoundException($"No template with name {templateName} found!");
            }

            var template = templates.FirstOrDefault(x => x.LanguageEnum == language);

            if (template == null)
            {
                throw new TemplateNotFoundException($"No template with name {templateName} for language {language} found!");
            }
            return template;
        }
    }
}
