using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Enums;

namespace WaCore.Contracts.Bl.Services.Templates
{
    public interface IEmailTemplateService
    {
        IEmailMessage GetEmailMessage(string templateName, LanguageEnum language, object inputObject,
            List<Guid> corellationIds);
    }
}
