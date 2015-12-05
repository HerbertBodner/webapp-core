using System;
using WaCore.Contracts.Enums;

namespace WaCore.Contracts.Exceptions.AuthenticationExceptions
{
    public class PasswordNotStrongEnoughException : AuthenticationException
    {
        public PasswordNotStrongEnoughException(PasswordScore passwordScore) : base($"Password is not strong enough, PasswortScore is {passwordScore}")
        {
        }
    }
}
