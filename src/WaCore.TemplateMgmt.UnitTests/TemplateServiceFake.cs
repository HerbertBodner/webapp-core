using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Dtos.Filters;
using WaCore.TemplateMgmt.Contracts.Engine;
using WaCore.TemplateMgmt.Contracts.Services;
using WaCore.TemplateMgmt.Services;

namespace WaCore.TemplateMgmt.UnitTests
{
    public interface ITemplateServiceFake : IWacTemplateService<TemplateFake, WacFilter, TemplateFake, TemplateFake>
    { }

    public class TemplateServiceFake : WacTemplateService<TemplateFake, WacFilter, TemplateFake, TemplateFake>, ITemplateServiceFake
    {
        public TemplateServiceFake(IWacUnitOfWork unitOfWork, IWacTemplateEngine engine) : base(unitOfWork, engine)
        {
        }

        public override void MapDtoToEntity(TemplateFake dto, TemplateFake entityToCreateOrUpdate, Operation operation)
        {
            //nothing to do
        }

        protected override TemplateFake MapEntityToDto(TemplateFake entity)
        {
            return entity;
        }
    }
}
