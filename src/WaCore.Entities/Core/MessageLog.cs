using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace WaCore.Entities.Core
{
    public class MessageLog
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual List<MessageLogCorrelations> MessageLogCorrelationLst { get; set; }
    }
}
