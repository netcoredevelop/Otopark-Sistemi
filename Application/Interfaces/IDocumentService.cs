using Domain.Entities;

namespace Application.Interfaces;

public interface IDocumentService
{
    Task<Document?> GetByIdAsync(int id);
    Task<IEnumerable<Document>> GetAllAsync();
    Task AddAsync(Document document);
    Task UpdateAsync(Document document);
    Task DeleteAsync(int id);
} 