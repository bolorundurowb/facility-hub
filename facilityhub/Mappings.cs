﻿using FacilityHub.Models.Data;
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

        config.NewConfig<Tenant, TenantRes>()
            .Map(x => x.Name, y => y.User == null ? null : y.User.FullName())
            .AfterMapping((model, vm) =>
            {
                var currentTenancy = model.History.MaxBy(x => x.CreatedAt);

                if (currentTenancy != null)
                    vm = vm with { StartsAt = currentTenancy.PeriodStart, EndsAt = currentTenancy.PeriodEnd };
            })
            .Compile();
    }
}
