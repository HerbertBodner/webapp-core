using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Enums;

namespace WaCore.Contracts.Data.Repositories
{
    public interface IEmailTemplateRepository
    {
        List<IEmailTemplate> GetTemplates(string name);
    }
}
