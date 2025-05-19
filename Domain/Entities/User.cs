namespace Domain.Entities;

public class User : BaseEntity<int>
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
