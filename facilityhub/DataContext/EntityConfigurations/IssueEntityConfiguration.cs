using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilityHub.DataContext.EntityConfigurations;

public class IssueEntityConfiguration : BaseEntityConfiguration<Issue>
{
    public override void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.Property(x => x.Log)
            .HasColumnType("jsonb");

        builder.Property(x => x.Repairer)
            .HasColumnType("jsonb");

        base.Configure(builder);
    }
}
