using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WaCore.Web.Exceptions
{
    public class BadRequestException : WebApiException
    {
        public BadRequestException(string message, Dictionary<string, string[]> errorDetails = null)
            : base(HttpStatusCode.BadRequest, message)
        {
            ErrorDetails = errorDetails;
        }

        public Dictionary<string, string[]> ErrorDetails { get; set; }

        public override string ToString()
        {
            var basicMessage = base.ToString();
            var stringBuilder = new StringBuilder(basicMessage);

            if (ErrorDetails != null && ErrorDetails.Any())
            {
                stringBuilder.Append(Environment.NewLine);
                var errorDetailsText = string.Join(";", ErrorDetails.Select(x => $"{x.Key}: {x.Value}").ToArray());
                stringBuilder.Append(errorDetailsText);
            }

            return stringBuilder.ToString();
        }
    }
}
