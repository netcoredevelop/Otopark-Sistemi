using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<Document?> GetByIdAsync(int id)
        => await _documentRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Document>> GetAllAsync()
        => await _documentRepository.GetAllAsync();

    public async Task AddAsync(Document document)
    {
        await _documentRepository.AddAsync(document);
        await _documentRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Document document)
    {
        _documentRepository.Update(document);
        await _documentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var document = await _documentRepository.GetByIdAsync(id);
        if (document != null)
        {
            _documentRepository.Remove(document);
            await _documentRepository.SaveChangesAsync();
        }
    }
} 