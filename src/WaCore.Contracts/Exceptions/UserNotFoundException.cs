using System;

namespace WaCore.Contracts.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string Email { get; set; }
        public UserNotFoundException(string email) : base()
        { }

        public override string Message
        {
            get
            {
                return $"User with Email {Email} not found";
            }
        }
    }
}
