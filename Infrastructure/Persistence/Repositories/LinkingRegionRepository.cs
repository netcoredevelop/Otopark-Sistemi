using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class LinkingRegionRepository : Repository<LinkingRegion, int>, ILinkingRegionRepository
{
    public LinkingRegionRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // LinkingRegion'a özgü ek metod implementasyonları buraya eklenebilir
} 