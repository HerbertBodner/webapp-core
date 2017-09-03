using System;
using System.Text;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class XssConfiguration : IConfigurationBase
    {
        public enum XssMode
        {
            /// <summary>
            /// Disables XSS filtering
            /// </summary>
            zero,

            /// <summary>
            /// Enables XSS filtering (usually default in browsers). If a cross-site scripting attack is detected, the browser will sanitize the page (remove the unsafe parts)
            /// </summary>
            one,

            /// <summary>
            /// Enables XSS filtering. Rather than sanitizing the page, the browser will prevent rendering of the page if an attack is detected
            /// </summary>
            oneBlock,

            /// <summary>
            /// Enables XSS filtering. If a cross-site scripting attack is detected, the browser will sanitize the page and report the violation. This uses the functionality of the CSP report-uri directive to send a report
            /// </summary>
            oneReport
        };

        public XssMode XssSetting { get; set; }
        public string ReportUri { get; set; }

        public XssConfiguration()
        {
            XssSetting = XssMode.oneBlock;
            ReportUri = string.Empty;
        }

        public string BuildHeaderValue()
        {
            var stringBuilder = new StringBuilder();

            switch(XssSetting)
            {
                case XssMode.zero:
                    stringBuilder.Append(0);
                    break;
                case XssMode.one:
                    stringBuilder.Append(1);
                    break;
                case XssMode.oneBlock:
                    stringBuilder.Append("1; mode=block");
                    break;
                case XssMode.oneReport:
                    if (string.IsNullOrWhiteSpace(ReportUri))
                    {
                        throw new ArgumentNullException(nameof(ReportUri));
                    }
                    stringBuilder.Append("1; report=");
                    stringBuilder.Append(ReportUri);
                    break;
            }

            return stringBuilder.ToString();
        }
    }
}
