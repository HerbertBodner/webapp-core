using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace WaCore.Entities.Core
{
    public class MessageLogCorrelations
    {
        public Guid MessageLogId { get; set; }

        public Guid CorreclationId { get; set; }

        public MessageLog MessageLog { get; set; }
    }
}
