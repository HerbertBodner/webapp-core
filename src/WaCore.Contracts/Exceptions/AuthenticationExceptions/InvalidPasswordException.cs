using System;

namespace WaCore.Contracts.Exceptions.AuthenticationExceptions
{
    public class InvalidPasswordException : AuthenticationException
    {
        public InvalidPasswordException(string wrongPassword) 
            : base($"Password {wrongPassword} is invalid")
        {
        }
    }
}
