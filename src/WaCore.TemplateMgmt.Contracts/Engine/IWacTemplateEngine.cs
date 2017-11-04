using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WaCore.TemplateMgmt.Contracts.Engine
{
    public interface IWacTemplateEngine
    {
        string Render(string input, object model);
        Task<string> RenderAsync(string input, object model);
    }
}
