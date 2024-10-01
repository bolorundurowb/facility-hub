using FacilityHub.DataContext;
using FacilityHub.Models.Data;
using FacilityHub.Services.Interfaces;

namespace FacilityHub.Services.Implementations;

public class DocumentService(FacilityHubDbContext dbContext) : IDocumentService
{
    public async Task Delete(Document document)
    {
        dbContext.Documents.Remove(document);
        await dbContext.SaveChangesAsync();
    }
}
