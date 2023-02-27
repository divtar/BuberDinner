using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistance;
using ErrorOr;
using MediatR;
using BuberDinner.Domain.Entities;
using BuberDinner.Domain.Common.Errors;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if(userRepository.GetUser(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
            //throw new Exception("User with given email does not exist!");
        }
        if(user.Password != query.Password)
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
}