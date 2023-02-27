
using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Api;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        // Option 1 Filters for error handling
        //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());

        services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        //Overrride ProblemDetailsFactory
        //This method is in the MS source code in the AddController. Implementation in Github
        services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
                services.AddMappings();

        return services;
    }
}