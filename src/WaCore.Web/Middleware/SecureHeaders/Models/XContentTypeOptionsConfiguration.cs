using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class XContentTypeOptionsConfiguration : IConfigurationBase
    {
        public string OptionValue { get; set; } = "nosniff;";

        public string BuildHeaderValue()
        {
            if (string.IsNullOrWhiteSpace(OptionValue))
            {
                throw new ArgumentNullException(nameof(OptionValue));
            }

            return OptionValue;
        }
    }
}
