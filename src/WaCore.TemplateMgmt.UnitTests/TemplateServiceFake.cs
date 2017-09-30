using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Dtos.Filters;
using WaCore.TemplateMgmt.Contracts.Engine;
using WaCore.TemplateMgmt.Contracts.Services;
using WaCore.TemplateMgmt.Services;

namespace WaCore.TemplateMgmt.UnitTests
{
    public interface ITemplateServiceFake : ITemplateService<Template, WacFilter, Template, Template>
    { }

    public class TemplateServiceFake : TemplateService<Template, WacFilter, Template, Template>, ITemplateServiceFake
    {
        public TemplateServiceFake(IWacUnitOfWork unitOfWork, ITemplateEngine engine) : base(unitOfWork, engine)
        {
        }

        public override void MapDtoToEntity(Template dto, Template entityToCreateOrUpdate, Operation operation)
        {
            //nothing to do
        }

        protected override Template MapEntityToDto(Template entity)
        {
            return entity;
        }
    }
}
