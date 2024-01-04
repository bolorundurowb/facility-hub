using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilityHub.DataContext.EntityConfigurations;

public class UserEntityConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(x => x.Managed)
            .WithMany(x => x.Managers)
            .UsingEntity("FacilityManagers");

        builder.HasMany(x => x.Owned)
            .WithMany(x => x.Owners)
            .UsingEntity("FacilityOwners");

        base.Configure(builder);
    }
}
