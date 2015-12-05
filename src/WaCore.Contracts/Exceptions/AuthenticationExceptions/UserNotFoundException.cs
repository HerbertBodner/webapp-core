using System;

namespace WaCore.Contracts.Exceptions.AuthenticationExceptions
{
    public class UserNotFoundException : AuthenticationException
    {
        public UserNotFoundException(string email) : base($"User with Email {email} not found")
        { }
    }
}
