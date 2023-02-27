using BuberDinner.Api;
using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.Application.Services;
using BuberDinner.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
  // Add services to the container.



  
  builder.Services.AddApplication()
                  .AddPresentation() 
                  .AddInfrastructure(builder.Configuration)
                  ;
}

var app = builder.Build();
{
  
  // Configure the HTTP request pipeline.
  //if (app.Environment.IsDevelopment())
  //{
    //  app.UseSwagger();
      //app.UseSwaggerUI();
  //}

  //Option 2 Middleware for error handling
  //app.UseMiddleware<ErrorHandlingMiddleware>();

  //Option 3 A route (controller) to handle all errors
  app.UseExceptionHandler("/error");

  app.UseHttpsRedirection();

  //app.UseAuthorization();

  app.MapControllers();

  app.Run();
}

