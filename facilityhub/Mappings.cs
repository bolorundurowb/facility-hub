﻿using FacilityHub.Models.Data;
using FacilityHub.Models.DTOs;
using FacilityHub.Models.Response;
using Mapster;
using NetTopologySuite.Geometries;

namespace FacilityHub;

public class Mappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Document, DocumentRes>()
            .Map(x => x.CreatedBy, y => y.CreatedBy == null ? null : y.CreatedBy.FullName())
            .Compile();

        config.NewConfig<Tenant, TenantRes>()
            .AfterMapping((model, vm) =>
            {
                vm.Name = model.User?.FullName();
                var currentTenancy = model.History.MaxBy(x => x.CreatedAt);

                if (currentTenancy == null)
                    return;

                vm.StartsAt = currentTenancy.PeriodStart;
                vm.EndsAt = currentTenancy.PeriodEnd;
            })
            .Compile();

        config.NewConfig<Point, LocationDto>()
            .Map(x => x.Longitude, y => y.X)
            .Map(x => x.Latitude, y => y.Y)
            .Compile();
    }
}
