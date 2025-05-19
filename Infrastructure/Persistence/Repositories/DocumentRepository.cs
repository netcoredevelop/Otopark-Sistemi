using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class DocumentRepository : Repository<Document, int>, IDocumentRepository
{
    public DocumentRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // Document'a özgü ek metod implementasyonları buraya eklenebilir
} 