using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class LinkingReasonRepository : Repository<LinkingReason, int>, ILinkingReasonRepository
{
    public LinkingReasonRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // LinkingReason'a özgü ek metod implementasyonları buraya eklenebilir
} 