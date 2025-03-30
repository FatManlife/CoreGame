using Api.Models;

namespace Api.Interfaces;

public interface IUserRepository
{
    Task<ICollection<User>> GetUsers();
    Task<User> GetUserById(int id);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(User user);
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserByRegister(string username, string email);
    
}