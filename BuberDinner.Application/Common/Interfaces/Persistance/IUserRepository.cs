using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{

    User? GetUser (string email);
    void Add(User user);
}