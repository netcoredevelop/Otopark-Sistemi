namespace Domain.Entities;

public class Role : BaseEntity<int>
{
    public string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
