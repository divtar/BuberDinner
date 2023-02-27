using BuberDinner.Application.Common.Interfaces.Persistance;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistance;

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = new();
    public void Add(User user)
    {
        users.Add(user);
    }

    public User? GetUser(string email)
    {
        return users.SingleOrDefault(user=>user.Email == email);
    }
}