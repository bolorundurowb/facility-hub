using FacilityHub.Models.Data;
using FacilityHub.Models.Response;
using Mapster;

namespace FacilityHub;

public class Mappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Document, DocumentRes>()
            .Map(x => x.CreatedBy, y => y.CreatedBy == null ? null : y.CreatedBy.FullName())
            .Compile();
    }
}
