using FacilityHub.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacilityHub.DataContext.EntityConfigurations;

public class DocumentEntityConfiguration : BaseEntityConfiguration<Document>
{
    public override void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        base.Configure(builder);
    }
}
