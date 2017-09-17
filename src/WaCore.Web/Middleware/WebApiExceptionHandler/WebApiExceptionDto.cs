using System.Net;

namespace WaCore.Web.Middleware.WebApiExceptionHandler
{
    public class WebApiExceptionDto
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorReference { get; set; }
        public string Message { get; set; }
    }
}
