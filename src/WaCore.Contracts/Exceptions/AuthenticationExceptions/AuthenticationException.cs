using System;

namespace WaCore.Contracts.Exceptions.AuthenticationExceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base()
        { }

        public AuthenticationException(string message) : base(message)
        { }

        public AuthenticationException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
