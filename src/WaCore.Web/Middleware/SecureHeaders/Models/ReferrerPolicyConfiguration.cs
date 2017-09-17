using System;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class ReferrerPolicyConfiguration : IConfigurationBase
    {
        public string OptionValue { get; set; } = "no-referrer;";

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