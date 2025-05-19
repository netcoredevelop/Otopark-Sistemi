using Domain.Entities;

namespace Application.Interfaces;

public interface ILinkingReasonRepository : IRepository<LinkingReason, int>
{
    // LinkingReason'a özgü ek metodlar buraya eklenebilir
} 