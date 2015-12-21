using System;
using WaCore.Contracts.Enums;

namespace WaCore.Contracts.Entities.Core
{
    public interface ITemplate
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string TemplateText { get; set; }
        string InputType { get; set; }
        string Description { get; set; }
        LanguageEnum LanguageEnum { get; set; }
    }
}