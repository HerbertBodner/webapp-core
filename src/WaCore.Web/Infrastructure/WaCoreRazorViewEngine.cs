using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.OptionsModel;

namespace WaCore.Web.Infrastructure
{
    public class WaCoreRazorViewEngine : RazorViewEngine
    {
        public WaCoreRazorViewEngine(
            IRazorPageFactory pageFactory,
            IRazorViewFactory viewFactory,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            IViewLocationCache viewLocationCache) : base(pageFactory, viewFactory, optionsAccessor, viewLocationCache)
        {
            //https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNet.Mvc.Razor/RazorViewEngine.cs

            // {0} represents the name of the view
            // {1} represents the name of the controller
            // {2} represents the name of the area
        }

        private const string ViewExtension = ".cshtml";


        public override IEnumerable<string> ViewLocationFormats => new[]
        {
            "/Views/{1}/{0}" + ViewExtension,
            "/Views/Shared/{0}" + ViewExtension,
            "/Views/WaCore/{1}/{0}" + ViewExtension,
            "/Views/WaCore/Shared/{0}" + ViewExtension,
        };

        public override IEnumerable<string> AreaViewLocationFormats => new[]
        {
            "/Areas/{2}/Views/{1}/{0}" + ViewExtension,
            "/Areas/{2}/Views/Shared/{0}" + ViewExtension,
            "/Views/Shared/{0}" + ViewExtension,
            "/Areas/{2}/Views/WaCore/{1}/{0}" + ViewExtension,
            "/Areas/{2}/Views/WaCore/Shared/{0}" + ViewExtension,
            "/Views/WaCore/Shared/{0}" + ViewExtension,
        };
    }
}
