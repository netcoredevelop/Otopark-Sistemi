using Domain.Entities;

namespace Application.Interfaces;

public interface IEnforcementOfficeRepository : IRepository<EnforcementOffice, int>
{
    // EnforcementOffice'a özgü ek metodlar buraya eklenebilir
} 