using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Contracts.Bl.Services.Account
{
    public interface IAccountResult
    {
        IEnumerable<string> Errors { get; }

        bool Succeeded { get; }
    }
}
