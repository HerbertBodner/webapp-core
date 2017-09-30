using Stubble.Core;
using Stubble.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaCore.TemplateMgmt.Contracts.Engine;

namespace WaCore.TemplateMgmt.Engine.Stubble
{
    public class StubbleEngine : ITemplateEngine
    {
        private StubbleVisitorRenderer _stubble;
        public StubbleEngine(RendererSettings settings=null)
        {
            if (settings == null)
            { 
                _stubble = new StubbleVisitorRenderer();
            }
            else
            {
                _stubble = new StubbleVisitorRenderer(settings);
            }
        }

        public string Render(string input, object model)
        {
            return _stubble.Render(input, model);
        }

        public async Task<string> RenderAsync(string input, object model)
        {
            return await _stubble.RenderAsync(input, model);
        }
    }
}
