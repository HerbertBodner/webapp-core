using System;

namespace WaCore.Contracts.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        private string wrongPassword;
        public InvalidPasswordException(string wrongPassword) : base()
        {
            this.wrongPassword = wrongPassword;
        }

        public override string Message
        {
            get
            {
                return $"Password {wrongPassword} is invalid";
            }
        }
    }
}
