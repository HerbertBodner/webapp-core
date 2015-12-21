using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Enums;

namespace WaCore.Entities.Core
{
    public class Template : ITemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string TemplateText { get; set; }

        public string InputType { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public LanguageEnum LanguageEnum { get; set; }
    }
}
