using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Bl.Services.Templates;

namespace WaCore.Bl.Services.Templates
{

    public class EmailMessage : Message, IEmailMessage
    {
        public string Subject { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }
    }
}
