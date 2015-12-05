using System;

namespace WaCore.Contracts.Exceptions
{
    public class UserEmailOrPasswordNullException : Exception
    {
        public UserEmailOrPasswordNullException() : base()
        { }

        public override string Message
        {
            get
            {
                return "User Email or Password cannot be empty";
            }
        }
    }
}
