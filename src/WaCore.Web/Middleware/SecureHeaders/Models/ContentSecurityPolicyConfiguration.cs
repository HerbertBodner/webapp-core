using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaCore.Web.Middleware.SecureHeaders.Models
{
    public class ContentSecurityPolicyConfiguration : IConfigurationBase
    {
        /// <summary>
        /// The base-uri values to use (which can be used in a document's base element)
        /// </summary>
        public List<string> BaseUri { get; set; }

        /// <summary>
        /// The default-src values to use (as a fallback for the other CSP rules)
        /// </summary>
        public List<string> DefaultSrc { get; set; }

        /// <summary>
        /// The script-src values to use (valid sources for sources for JavaScript)
        /// </summary>
        public List<string> ScriptSrc { get; set; }

        /// <summary>
        /// The object-src values to use (valid sources for the object, embed, and applet elements)
        /// </summary>
        public List<string> ObjectSrc { get; set; }

        /// <summary>
        /// The style-src values to use (valid sources for style sheets)
        /// </summary>
        public List<string> StyleSrc { get; set; }

        /// <summary>
        /// The img-src values to use (valid sources for images and favicons)
        /// </summary>
        public List<string> ImgSrc { get; set; }

        /// <summary>
        /// The media-src values to use (valid sources for loading media using the audio and video elements)
        /// </summary>
        public List<string> MediaSrc { get; set; }

        /// <summary>
        /// The frame-src values to use (valid sources for nested browsing contexts loading using elements such as frame and iframe)
        /// </summary>
        public List<string> FrameSrc { get; set; }

        /// <summary>
        /// The frame-ancestors values to use (valid parents that may embed a page using frame, iframe, object, embed, or applet)
        /// </summary>
        public List<string> FrameAncestors { get; set; }

        /// <summary>
        /// The font-src values to use (valid sources for fonts loaded using @font-face)
        /// </summary>
        public List<string> FontSrc { get; set; }

        /// <summary>
        /// The connect-src values to use (restricts the URLs which can be loaded using script interfaces)
        /// </summary>
        public List<string> ConnectSrc { get; set; }

        /// <summary>
        /// The manifest-src values to use (which manifest can be applied to the resource)
        /// </summary>
        public List<string> ManifestSrc { get; set; }

        /// <summary>
        /// The form-action values to use (restricts the URLs which can be used as the target of a form submissions from a given context)
        /// </summary>
        public List<string> FormAction { get; set; }

        /// <summary>
        /// The directive restricts the set of plugins that can be embedded into a document by limiting the types of resources which can be loaded
        /// </summary>
        public List<string> PluginTypes { get; set; }

        /// <summary>
        /// The directive instructs the client to require the use of Subresource Integrity for scripts or styles on the page
        /// </summary>
        public List<string> RequireSriFor { get; set; }

        /// <summary>
        /// The directive specifies valid sources for Worker, SharedWorker, or ServiceWorker scripts
        /// </summary>
        public List<string> WorkerSrc { get; set; }

        /// <summary>
        /// Whether to include the block-all-mixed-content directive (prevents loading any assets using HTTP when the page is loaded using HTTPS)
        /// </summary>
        public bool BlockAllMixedContent { get; set; }

        /// <summary>
        /// Whether to include the upgrade-insecure-requests directive (instructs user agents to treat all of a site's insecure URLs as though they have been replaced with secure URLs)
        /// </summary>
        public bool UpgradeInsecureRequests { get; set; }

        /// <summary>
        /// Enables a sandbox for the requested resource similar to the iframe sandbox attribute
        /// </summary>
        public string Sandbox { get; set; }

        public ContentSecurityPolicyConfiguration()
        {
            BaseUri = new List<string>();
            DefaultSrc = new List<string>();
            ScriptSrc = new List<string>();
            ObjectSrc = new List<string>();
            StyleSrc = new List<string>();
            ImgSrc = new List<string>();
            MediaSrc = new List<string>();
            FrameSrc = new List<string>();
            FrameAncestors = new List<string>();
            FontSrc = new List<string>();
            ConnectSrc = new List<string>();
            ManifestSrc = new List<string>();
            FormAction = new List<string>();
            PluginTypes = new List<string>();
            RequireSriFor = new List<string>();
            WorkerSrc = new List<string>();

            BlockAllMixedContent = true;
            UpgradeInsecureRequests = true;

            Sandbox = null;
        }

        public string BuildHeaderValue()
        {
            var sb = new StringBuilder();

            if (AnyValues())
            {
                if (BaseUri.Any())
                {
                    sb.Append(BuildValuesForDirective("base-uri", BaseUri));
                }
                if (DefaultSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("default-src", DefaultSrc));
                }
                if (ScriptSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("script-src", ScriptSrc));
                }
                if (ObjectSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("object-src", ObjectSrc));
                }
                if (StyleSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("style-src", StyleSrc));
                }
                if (ImgSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("img-src", ImgSrc));
                }
                if (MediaSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("media-src", MediaSrc));
                }
                if (FrameSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("frame-src", FrameSrc));
                }
                if (FrameAncestors.Any())
                {
                    sb.Append(BuildValuesForDirective("frame-ancestors", FrameAncestors));
                }
                if (FontSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("font-src", FontSrc));
                }
                if (ConnectSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("connect-src", ConnectSrc));
                }
                if (ManifestSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("manifest-src", ManifestSrc));
                }
                if (FormAction.Any())
                {
                    sb.Append(BuildValuesForDirective("form-action", FormAction));
                }
                if (PluginTypes.Any())
                {
                    sb.Append(BuildValuesForDirective("plugin-types", PluginTypes));
                }
                if (RequireSriFor.Any())
                {
                    sb.Append(BuildValuesForDirective("require-sri-for", RequireSriFor));
                }
                if (WorkerSrc.Any())
                {
                    sb.Append(BuildValuesForDirective("worker-src", WorkerSrc));
                }
            }

            if (BlockAllMixedContent)
            {
                sb.Append("block-all-mixed-content; ");
            }
            if (UpgradeInsecureRequests)
            {
                sb.Append("upgrade-insecure-requests; ");
            }
            if (!string.IsNullOrEmpty(Sandbox))
            {
                sb.AppendFormat("sandbox {0}", Sandbox);
            }

            return sb.ToString();
        }

        private string BuildValuesForDirective(string directiveName, List<string> directiveValues)
        {
            var sb = new StringBuilder();
            
            sb.Append(directiveName);
            directiveValues.ForEach(s => sb.Append($"'{s}' "));
            sb.Append(";");

            return sb.ToString();
        }

        private bool AnyValues()
        {
            return BaseUri.Any()    || DefaultSrc.Any() || ScriptSrc.Any()  || ObjectSrc.Any()
                || StyleSrc.Any()   || ImgSrc.Any()     || MediaSrc.Any()   || FrameSrc.Any()
                || FrameAncestors.Any() || FontSrc.Any()|| ConnectSrc.Any() || ManifestSrc.Any()
                || FormAction.Any() || RequireSriFor.Any() || PluginTypes.Any() || WorkerSrc.Any();
        }
    }
}
