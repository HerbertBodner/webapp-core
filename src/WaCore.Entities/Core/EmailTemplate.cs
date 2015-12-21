using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Entities.Core;

namespace WaCore.Entities.Core
{
    public class EmailTemplate : Template, IEmailTemplate
    {
        public string Subject { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

    }
}
