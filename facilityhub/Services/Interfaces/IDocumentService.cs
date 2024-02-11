using FacilityHub.Models.Data;

namespace FacilityHub.Services.Interfaces;

public interface IDocumentService
{
    Task Delete(Document document);
}
