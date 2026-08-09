using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features.Commands.RegisterUser;
using Users.Application.Features.Queries.UserLogin;

namespace Users.api.Modules
{
    public class UsersModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async (IMediator mediator, [FromBody] RegisterUserRequest command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
            app.MapPost("/login", async (IMediator mediator, [FromBody] UserLoginRequest command) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
        }
    }
}
