using FacilityHub.DataContext;
using FacilityHub.Models.Data;
using FacilityHub.Services.Interfaces;

namespace FacilityHub.Services.Implementations;

public class DocumentService : IDocumentService
{
    private readonly FacilityHubDbContext _dbContext;

    public DocumentService(FacilityHubDbContext dbContext) => _dbContext = dbContext;

    public async Task Delete(Document document)
    {
        _dbContext.Documents.Remove(document);
        await _dbContext.SaveChangesAsync();
    }
}
