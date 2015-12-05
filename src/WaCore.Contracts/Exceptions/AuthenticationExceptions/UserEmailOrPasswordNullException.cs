using System;

namespace WaCore.Contracts.Exceptions.AuthenticationExceptions
{
    public class UserEmailOrPasswordNullException : AuthenticationException
    {
        public UserEmailOrPasswordNullException() : base("User Email or Password cannot be empty")
        { }
    }
}
