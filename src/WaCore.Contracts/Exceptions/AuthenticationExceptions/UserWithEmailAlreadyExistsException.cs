using System;

namespace WaCore.Contracts.Exceptions.AuthenticationExceptions
{
    public class UserWithEmailAlreadyExistsException : AuthenticationException
    {
        public UserWithEmailAlreadyExistsException(string email) : 
            base($"User with Email {email} already exists")
        { }
    }
}
