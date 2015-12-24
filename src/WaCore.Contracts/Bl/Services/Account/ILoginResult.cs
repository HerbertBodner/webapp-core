using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Contracts.Bl.Services.Account
{
    public interface ILoginResult
    {
        bool IsLockedOut { get; set; }
        bool IsNotAllowed { get; set; }
        bool RequiresTwoFactor { get; set; }
        bool Succeeded { get; set; }
    }
}
