using Stubble.Core.Settings;
using WaCore.TemplateMgmt.Contracts.Engine;
using WaCore.TemplateMgmt.Engine.Stubble;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StubbleServiceCollectionExtensions
    {
        public static IServiceCollection AddStubble(this IServiceCollection serviceCollection, RendererSettings rendererSettings = null)
        {
            serviceCollection.AddSingleton(typeof(IWacTemplateEngine), new StubbleEngine(rendererSettings));

            return serviceCollection;
        }
    }
}
