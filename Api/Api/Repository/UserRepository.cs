using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class UserRepository:IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
        
    public async Task<ICollection<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<User> GetUserByRegister(string username, string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
    }
    
}