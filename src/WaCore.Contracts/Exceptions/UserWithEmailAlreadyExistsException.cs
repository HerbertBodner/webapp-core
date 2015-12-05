using System;

namespace WaCore.Contracts.Exceptions
{
    public class UserWithEmailAlreadyExistsException : Exception
    {
        public string Email { get; set; }

        public UserWithEmailAlreadyExistsException(string email) : base()
        { }

        public override string Message
        {
            get
            {
                return $"User with Email {Email} already exists";
            }
        }
    }
}
