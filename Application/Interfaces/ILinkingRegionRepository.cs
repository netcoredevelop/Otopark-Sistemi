using Domain.Entities;

namespace Application.Interfaces;

public interface ILinkingRegionRepository : IRepository<LinkingRegion, int>
{
    // LinkingRegion'a özgü ek metodlar buraya eklenebilir
} 