using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Enums;

namespace WaCore.Contracts.Bl.Services
{
    public interface IPasswordService
    {
        PasswordScore CheckStrength(string password);
    }
}
