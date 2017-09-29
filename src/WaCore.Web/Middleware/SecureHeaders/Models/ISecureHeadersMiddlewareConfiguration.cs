namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public interface ISecureHeadersMiddlewareConfiguration
    {
        HstsConfiguration HstsConfiguration { get; set; }

        HpkpConfiguration HpkpConfiguration { get; set; }

        XFrameOptionsConfiguration XFrameOptionsConfiguration { get; set; }

        XssConfiguration XssConfiguration { get; set; }

        ContentSecurityPolicyConfiguration ContentSecurityPolicyConfiguration { get; set; }

        ReferrerPolicyConfiguration ReferrerPolicyConfiguration { get; set; }

        XContentTypeOptionsConfiguration XContentTypeOptionsConfiguration { get; set; }
    }
}
