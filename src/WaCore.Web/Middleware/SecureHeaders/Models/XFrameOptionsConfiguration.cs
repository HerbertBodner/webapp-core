using System;
using System.Text;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class XFrameOptionsConfiguration : IConfigurationBase
    {
        public enum XFrameOptions
        {
            /// <summary>
            /// The page cannot be displayed in a frame, regardless of the site attempting to do so
            /// </summary>
            deny,

            /// <summary>
            /// The page can only be displayed in a frame on the same origin as the page itself
            /// </summary>
            sameorigin,

            /// <summary>
            /// The page can only be displayed in a frame on the specified origin
            /// </summary>
            allowfrom
        };

        public XFrameOptions OptionValue { get; set; }
        public string AllowFromDomain { get; set; }

        public XFrameOptionsConfiguration()
        {
            OptionValue = XFrameOptions.sameorigin;
        }

        public string BuildHeaderValue()
        {
            var stringBuilder = new StringBuilder();
            switch (OptionValue)
            {
                case XFrameOptions.deny:
                    stringBuilder.Append("deny");
                    break;
                case XFrameOptions.sameorigin:
                    stringBuilder.Append("sameorigin");
                    break;
                case XFrameOptions.allowfrom:
                    if (string.IsNullOrWhiteSpace(AllowFromDomain))
                    {
                        throw new ArgumentNullException(nameof(AllowFromDomain));
                    }
                    stringBuilder.Append("allow-from: ");
                    stringBuilder.Append(AllowFromDomain);
                    break;
            }

            return stringBuilder.ToString();
        }
    }
}
