using System.Net;

namespace WaCore.Web.Exceptions
{
    public class ResourceNotFoundException : WebApiException
    {
        private const string MessageFormat = "Resource '{0}' not found";

        public ResourceNotFoundException(string resource)
            : base(HttpStatusCode.NotFound, string.Format(MessageFormat, resource))
        {
            Resource = resource;
        }

        public ResourceNotFoundException(string resource, string message)
            : base(HttpStatusCode.NotFound, message)
        {
            Resource = resource;
        }

        public string Resource { get; }
    }
}
