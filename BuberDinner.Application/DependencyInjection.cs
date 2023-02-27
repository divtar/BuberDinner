
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        //services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}