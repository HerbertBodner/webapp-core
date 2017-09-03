using System;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class ReferrerPolicy : IConfigurationBase
    {
        public string OptionValue { get; set; } = "no-referrer;";

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