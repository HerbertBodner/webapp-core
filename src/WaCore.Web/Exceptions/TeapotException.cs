using System.Net;

namespace WaCore.Web.Exceptions
{
    public class TeapotException : WebApiException
    {
        private const string BaseMessage = "I'm a teapot";

        public TeapotException()
            : base((HttpStatusCode)418, BaseMessage)
        {
        }
    }
}
