using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class EnforcementOffice:BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public virtual ICollection<EnforcementRecord> EnforcementRecords { get; set; }

    public EnforcementOffice()
    {
        EnforcementRecords = new List<EnforcementRecord>();
    }
}
