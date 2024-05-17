using BelleVillePrototype.ApiService.Contracts;
using BelleVillePrototype.ApiService.Contracts.UserContract;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Shared.Tokens;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using LanguageExt.Common;
namespace BelleVillePrototype.ApiService.Features.Users;

public static class Login 
{
    public class Command : IRequest<Result<LoginResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ControllerResult : LoginResult { }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é um parâmetro obrigatório");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password é um parâmetro obrigatório");
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<LoginResult>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;

        public Handler(
            ApplicationDbContext dbContext, 
            IValidator<Command> validator, 
            UserManager<UserEntity> userManager, 
            SignInManager<UserEntity> signInManager, 
            ITokenService tokenService)
        {
            _dbContext = dbContext;
            _validator = validator;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        
        public async Task<Result<LoginResult>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new Result<LoginResult>(new ValidationException(validationResult.ToString()));
            if (!request.Email.Contains('@'))
                return new Result<LoginResult>(new ValidationException("Email inválido."));
            
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return new Result<LoginResult>(new Exception("Utilizador ou password incorretos"));
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return new Result<LoginResult>(new Exception("Utilizador ou password incorretos"));
            
            var token = await _tokenService.GenerateToken(user);
            
            var role = await _userManager.GetRolesAsync(user);
            UserEntityRole roleConverted = Enum.Parse<UserEntityRole>(role.FirstOrDefault());
            
            return new Result<LoginResult>(new LoginResult()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Username = user.UserName,
                Token = token,
                Role = roleConverted.ToString(),
            });
        }
    }
}
public class RegisterEndpoint: ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("login", async ([FromBody] LoginUserCommand data, ISender sender) =>
        {
            var command = data.Adapt<Login.Command>();
            
            var result = await sender.Send(command);
            return result.Match<IResult>(
                controllerResult =>
                {
                    var content = controllerResult;
                    var c = content.Adapt<Login.ControllerResult>();
                    return Results.Ok(c);
                },
               error => Results.Problem(detail: error.Message, statusCode: 400));
        }).IncludeInOpenApi();
    }
}

