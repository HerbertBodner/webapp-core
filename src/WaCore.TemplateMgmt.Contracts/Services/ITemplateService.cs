using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;

namespace WaCore.TemplateMgmt.Contracts.Services
{
    public interface ITemplateService<TEntity, TFilter, TDto, TNewDto> : IWacCrudService<TEntity, TFilter, TDto, TNewDto>
        where TEntity : class, new()
        where TFilter : IWacFilter
    {
        string Render(object id, object model);
        Task<string> RenderAsync(object id, object model);

        string Render(TEntity template, object model);
        Task<string> RenderAsync(TEntity template, object model);

        string Render(string templateContent, object model);
        Task<string> RenderAsync(string templateContent, object model);
    }
}
