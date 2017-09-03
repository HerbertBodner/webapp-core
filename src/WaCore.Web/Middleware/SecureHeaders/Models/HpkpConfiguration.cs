using System.Collections.Generic;
using System.Text;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class HpkpConfiguration : IConfigurationBase
    {
        /// <summary>
        /// The quoted string is the Base64 encoded Subject Public Key Information (SPKI) fingerprint
        /// </summary>
        public List<string> PinSha256 { get; set; }

        /// <summary>
        /// The time, in seconds, that the browser should remember that this site is only to be accessed using one of the pinned keys
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// (OPTIONAL) Whether this rule applies to all of the site's subdomains as well
        /// </summary>
        public bool IncludeSubDomains { get; set; }

        /// <summary>
        /// (OPTIONAL) The URL which pin validation failures are reported to
        /// </summary>
        public string ReportUri { get; set; }

        public HpkpConfiguration()
        {
            PinSha256 = new List<string>();
            ReportUri = null;
            MaxAge = 10000;
            IncludeSubDomains = true;
        }

        public string BuildHeaderValue()
        {
            var stringBuilder = new StringBuilder();
            foreach (var pinSha256 in PinSha256)
            {
                stringBuilder.Append("pin-sha256=\"");
                stringBuilder.Append(pinSha256);
                stringBuilder.Append("\";");
            }

            if (!string.IsNullOrEmpty(ReportUri))
            {
                stringBuilder.AppendFormat("report-url=\"");
                stringBuilder.Append(ReportUri);
            }

            stringBuilder.Append("\";max-age=");
            stringBuilder.Append(MaxAge);
            stringBuilder.Append(IncludeSubDomains ? "; includeSubDomains" : string.Empty);
            
            return stringBuilder.ToString();
        }
    }
}
