namespace Application.Interfaces;

public interface ICurrentUserService
{
    string? UserName { get; }
    int? UserId { get; }
} 