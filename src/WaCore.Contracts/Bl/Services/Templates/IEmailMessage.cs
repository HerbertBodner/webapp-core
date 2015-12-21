using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Contracts.Bl.Services.Templates
{
    public interface IEmailMessage
    {
        string Subject { get; set; }
        string From { get; set; }
        string To { get; set; }
        string Cc { get; set; }
        string Bcc { get; set; }
        Guid TemplateId { get; set; }
        string Text { get; set; }
    }
}
