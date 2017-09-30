using System.Text;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    /// <summary>
    /// Represents the HTTP Strict Transport Security configuration
    /// </summary>
    public class HstsConfiguration : IConfigurationBase
    {
        /// <summary>
        /// (OPTIONAL) Whether this rule applies to all of the site's subdomains as well
        /// </summary>
        public bool IncludeSubDomains { get; set; }

        /// <summary>
        /// The time, in seconds, that the browser should remember that this site is only to be accessed using HTTPS
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// Google maintains an HSTS preload service. By following the guidelines and successfully submitting your domain, browsers will never connect to your domain using an insecure connection
        /// </summary>
        public bool Preload { get; set; }

        public HstsConfiguration()
        {
            MaxAge = 31536000;
        }

        public string BuildHeaderValue()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("max-age=");
            stringBuilder.Append(MaxAge);
            stringBuilder.Append(IncludeSubDomains ? "; includeSubDomains" : string.Empty);

            if (Preload)
            {
                stringBuilder.Append(IncludeSubDomains ? "; preload" : string.Empty);
            }

            return stringBuilder.ToString();
        }
    }
}
