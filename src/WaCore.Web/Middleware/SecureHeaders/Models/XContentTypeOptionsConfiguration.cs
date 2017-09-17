using System;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class XContentTypeOptionsConfiguration : IConfigurationBase
    {
        public string OptionValue { get; set; } = "nosniff;";

        public string BuildHeaderValue()
        {
            if (string.IsNullOrWhiteSpace(OptionValue))
            {
                throw new InvalidOperationException($"{nameof(OptionValue)} property is not set");
            }

            return OptionValue;
        }
    }
}
