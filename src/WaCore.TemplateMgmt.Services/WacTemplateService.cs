using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Services;
using WaCore.TemplateMgmt.Contracts.Engine;
using WaCore.TemplateMgmt.Contracts.Services;
using WaCore.TemplateMgmt.Contracts.ValueObjects;

namespace WaCore.TemplateMgmt.Services
{
    public abstract class WacTemplateService<TEntity, TFilter, TDto, TNewDto> : WacCrudService<TEntity, TFilter, TDto, TNewDto>, IWacTemplateService<TEntity, TFilter, TDto, TNewDto>
        where TFilter : IWacFilter
        where TEntity : class, IWacTemplate, new()
    {
        protected IWacTemplateEngine Engine;
        public WacTemplateService(IWacUnitOfWork unitOfWork, IWacTemplateEngine engine) : base(unitOfWork)
        {
            Engine = engine;
        }

        public string Render(object id, object model)
        {
            var template = Repo.Get(id);
            return Render(template.Content, model);
        }

        public async Task<string> RenderAsync(object id, object model)
        {
            var template = await Repo.GetAsync(id);
            return await RenderAsync(template.Content, model);
        }


        public string Render(TEntity template, object model)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            return Render(template.Content, model);
        }

        public async Task<string> RenderAsync(TEntity template, object model)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            return await RenderAsync(template.Content, model);
        }



        public string Render(string templateContent, object model)
        {
            return Engine.Render(templateContent, model);
        }

        public async Task<string> RenderAsync(string templateContent, object model)
        {
            return await Engine.RenderAsync(templateContent, model);
        }
    }
}
