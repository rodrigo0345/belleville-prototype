using BelleVillePrototype.ApiService.Contracts;
using BelleVillePrototype.ApiService.Contracts.UserContract;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Shared.Result;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class Register 
{
    public class Command : IRequest<Result<QueryUser>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        
        public string Username { get; set; }
    }

    public class ControllerResult : QueryUser
    {
    }

    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é um parâmetro obrigatório");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password é um parâmetro obrigatório");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName é um parâmetro obrigatório");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName é um parâmetro obrigatório");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone é um parâmetro obrigatório");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username é um parâmetro obrigatório");
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<QueryUser>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;
        private readonly UserManager<UserEntity> _userManager;

        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator, UserManager<UserEntity> userManager)
        {
            _dbContext = dbContext;
            _validator = validator;
            _userManager = userManager;
        }
        
        public async Task<Result<QueryUser>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new Result<QueryUser>(null, error: validationResult.ToString());
            
            UserEntity user =  new UserEntity
            {
                Email= request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                UserName= request.Username
            };

            try
            {
                var createdUser = await _userManager.CreateAsync(user, request.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                        return new Result<QueryUser>(
                        new (){
                            Id = user.Id,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            Username = user.UserName,
                            Role = UserEntityRole.User
                        });
                    
                    // Em caso de erro ao associar a role ao utilizador 
                    return new Result<QueryUser>(null, error: roleResult.Errors.FirstOrDefault().Description.ToString());
                }
                // Em caso de erro ao criar o utilizador
                return new Result<QueryUser>(null, error: createdUser.Errors.FirstOrDefault().Description.ToString());
            }
            catch (Exception e)
            {
                return new Result<QueryUser>(null, error: e.StackTrace);
            }
        }
    }
}
public class RegisterEndpoint: ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("register", async ([FromBody] RegisterUserCommand data, ISender sender) =>
        {
            var command = data.Adapt<Register.Command>();
            
            var result = await sender.Send(command);
            if (result.Error.IsSome)
            {
                var error = result.Error.OrElse("Some error occurred, no message was left");
                return Results.Problem(detail: error, statusCode: 500);
            }

            var content = result.Content.OrElseThrow();
            var controllerResult = content.Adapt<QueryUser>();
            return Results.Ok(controllerResult);
        }).IncludeInOpenApi();
    }
}

