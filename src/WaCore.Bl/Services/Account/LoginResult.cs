using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Bl.Services.Account;

namespace WaCore.Bl.Services.Account
{
    public class LoginResult : ILoginResult
    {
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool Succeeded { get; set; }
    }
}
