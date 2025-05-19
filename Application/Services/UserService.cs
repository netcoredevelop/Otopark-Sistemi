using Application.Interfaces;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _userRepository.GetByIdAsync(id);

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _userRepository.GetAllAsync();

    public async Task AddAsync(User user)
    {
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            _userRepository.Remove(user);
            await _userRepository.SaveChangesAsync();
        }
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var users = await _userRepository.FindAsync(u => u.Username == username);
        return users.FirstOrDefault();
    }

    public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
    {
        return await _userRepository.FindAsync(predicate);
    }
} 