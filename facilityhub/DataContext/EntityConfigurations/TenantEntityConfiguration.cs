using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilityHub.DataContext.EntityConfigurations;

public class TenantEntityConfiguration : BaseEntityConfiguration<Tenant>
{
    public override void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.Property(x => x.History)
            .HasColumnType("jsonb");

        base.Configure(builder);
    }
}
