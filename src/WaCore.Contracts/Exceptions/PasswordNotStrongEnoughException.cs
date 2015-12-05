using System;
using WaCore.Contracts.Enums;

namespace WaCore.Contracts.Exceptions
{
    public class PasswordNotStrongEnoughException : Exception
    {
        private PasswordScore passwordScore;
        public PasswordNotStrongEnoughException(PasswordScore passwordScore)
        {
            this.passwordScore = passwordScore;
        }

        public override string Message
        {
            get
            {
                return $"Password is not strong enough, PasswortScore is {passwordScore}";
            }
        }
    }
}
