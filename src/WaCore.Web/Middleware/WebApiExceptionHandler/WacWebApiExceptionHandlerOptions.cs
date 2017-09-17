using WaCore.Common.ExceptionHandling;

namespace WaCore.Web.Middleware.WebApiExceptionHandler
{
    /// <summary>
    /// Configuration settings for <see cref="WacWebApiExceptionHandlerMiddleware"/>
    /// </summary>
    public class WacWebApiExceptionHandlerOptions
    {
        /// <summary>
        /// If set to True then middleware will send 500 response in case exception was not handled by
        /// defined handlers.
        /// </summary>
        public bool CatchAll { get; set; }

        /// <summary>
        /// Exception handling configuration. 
        /// If this value is not set then default settings for handling configuration will be used.
        /// </summary>
        public WacHandlingConfiguration<object, WebApiExceptionDto> HandlingConfiguration { get; set; }
    }
}
