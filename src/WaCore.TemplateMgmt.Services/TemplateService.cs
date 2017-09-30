using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Services;
using WaCore.TemplateMgmt.Contracts.Engine;
using WaCore.TemplateMgmt.Contracts.Services;
using WaCore.TemplateMgmt.Contracts.ValueObjects;

namespace WaCore.TemplateMgmt.Services
{
    public abstract class TemplateService<TEntity, TFilter, TDto, TNewDto> : WacCrudService<TEntity, TFilter, TDto, TNewDto>, ITemplateService<TEntity, TFilter, TDto, TNewDto>
        where TFilter : IWacFilter
        where TEntity : class, ITemplate, new()
    {
        protected ITemplateEngine Engine;
        public TemplateService(IWacUnitOfWork unitOfWork, ITemplateEngine engine) : base(unitOfWork)
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


        public string GetJsonFromType(Type modeltype, bool formattingIndented = true, int maxHierarchyDepth = 5)
        {
            var instance = InstanceCreatorHelper.CreateInstanceRecursively(modeltype, maxHierarchyDepth);

            return JsonConvert.SerializeObject(instance, formattingIndented ? Formatting.Indented : Formatting.None);
        }

        
        public string RenderWithJsonInput<TModel>(string templateContent, string jsonObject)
        {
            var model = JsonConvert.DeserializeObject<TModel>(jsonObject);
            return Render(templateContent, model);
        }
    }
}
