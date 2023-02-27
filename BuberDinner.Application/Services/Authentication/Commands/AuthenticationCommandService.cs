using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistance;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using FluentResults;

namespace BuberDinner.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

  
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if(userRepository.GetUser(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
            //throw new Exception("User with given email does not exist!");
        }
        if(user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
            //throw new Exception("Invalid password.");
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult
        (
            user,
            token
        );
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if(userRepository.GetUser(email) is not null)
        {
            return Errors.User.DuplicateEmail;
            //return Result.Fail<AuthenticationResult>(new [] {new DuplicateEmailError()});

        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        userRepository.Add(user);


        var token = jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult
        (
            user,
            token
        );
    }
}
