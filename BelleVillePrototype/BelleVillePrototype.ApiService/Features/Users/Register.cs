using BelleVillePrototype.ApiService.Contracts.UserContract;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Shared.Tokens;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using LanguageExt.Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

public static class Register 
{
    public class Command : IRequest<Result<RegisterResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        
        public string Username { get; set; }
    }

    public class ControllerResult : RegisterResult
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

    internal sealed class Handler : IRequestHandler<Command, Result<RegisterResult>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;

        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator, UserManager<UserEntity> userManager, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _validator = validator;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        
        public async Task<Result<RegisterResult>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return new Result<RegisterResult>(new ValidationException(validationResult.ToString()));
            if (!request.Email.Contains('@'))
                return new Result<RegisterResult>(new ValidationException("Email inválido"));
            
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
                    var roleResult = await _userManager.AddToRoleAsync(user, UserEntityRole.User.ToString());
                    if (roleResult.Succeeded)
                        return new Result<RegisterResult>(
                            new RegisterResult() {
                                Id = user.Id,
                                Email = user.Email,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Phone = user.Phone,
                                Username = user.UserName,
                                Role = UserEntityRole.User.ToString(),
                                Token = await _tokenService.GenerateToken(user)
                            }
                            );
                    
                    // Em caso de erro ao associar a role ao utilizador 
                    return new Result<RegisterResult>(new Exception(roleResult.Errors.FirstOrDefault()?.Description
                        .ToString()));
                }
                // Em caso de erro ao criar o utilizador
                return new Result<RegisterResult>(new Exception(createdUser.Errors.FirstOrDefault()?.Description
                    .ToString()));
            }
            catch (Exception e)
            {
                return new Result<RegisterResult>(e);
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
            return result.Match<IResult>(
                item => Results.Ok(item.Adapt<Register.ControllerResult>()),
                error => Results.Problem(detail: error.Message, statusCode: 400)
            );
        }).IncludeInOpenApi();
    }
}

