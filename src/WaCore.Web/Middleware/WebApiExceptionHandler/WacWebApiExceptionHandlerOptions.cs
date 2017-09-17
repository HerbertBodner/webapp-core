using WaCore.Common.ExceptionHandling;

namespace WaCore.Web.Middleware.WebApiExceptionHandler
{
    public class WacWebApiExceptionHandlerOptions
    {
        public bool CatchAll { get; set; }

        public WacHandlingConfiguration<object, WebApiExceptionDto> HandlingConfiguration { get; set; }
    }
}
