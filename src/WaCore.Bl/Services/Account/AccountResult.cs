using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Bl.Services.Account;

namespace WaCore.Bl.Services.Account
{
    public class AccountResult : IAccountResult
    {
        public static AccountResult Success()
        {
            return new AccountResult();
        }

        public AccountResult(List<string> errors = null)
        {
            Errors = errors ?? new List<string>();
        }

        public IEnumerable<string> Errors { get; }
        
        public bool Succeeded => !Errors.Any();
    }
}
