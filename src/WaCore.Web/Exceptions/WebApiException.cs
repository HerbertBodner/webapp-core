using System;
using System.Net;
using System.Text;

namespace WaCore.Web.Exceptions
{
    public class WebApiException: Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public WebApiException(HttpStatusCode httpStatusCode, string message) :
            base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Status Code: {0} - {1}", (int)HttpStatusCode, HttpStatusCode.ToString());
            stringBuilder.Append(Environment.NewLine);

            stringBuilder.AppendFormat(Message);

            return stringBuilder.ToString();
        }
    }
}
