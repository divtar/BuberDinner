using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication;
using FluentResults;
using BuberDinner.Application.Common.Errors;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Application.Services.Authentication.Common;
using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using MapsterMapper;

namespace BuberDinner.Api.Controllers;


[Route("auth")]
public class AutheticationController: ApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
 
    public AutheticationController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var registerCommand = mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> authResult = await mediator.Send(registerCommand);

        return authResult.Match(
            authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors)

        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var loginQuery = mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResult = await mediator.Send(loginQuery);

         if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
         {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
         }
         return authResult.Match(
            authResult => Ok(mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors)

        );
    }
}