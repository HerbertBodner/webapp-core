using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace WaCore.Web.Infrastructure
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            if (request.Headers != null && request.Headers.ContainsKey("X-Requested-With"))
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }
    }
}
