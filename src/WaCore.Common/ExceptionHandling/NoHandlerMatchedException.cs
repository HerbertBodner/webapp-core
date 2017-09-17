using System;

namespace WaCore.Common.ExceptionHandling
{
    public class NoHandlerMatchedException: Exception
    {
        private const string DefaultMessage = "No handler matched the exception to catch";

        public NoHandlerMatchedException(Exception innerException)
            : base(DefaultMessage, innerException)
        {

        }
    }
}
