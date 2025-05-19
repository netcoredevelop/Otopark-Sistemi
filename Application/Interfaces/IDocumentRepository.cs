using Domain.Entities;

namespace Application.Interfaces;

public interface IDocumentRepository : IRepository<Document, int>
{
    // Document'a özgü ek metodlar buraya eklenebilir
} 