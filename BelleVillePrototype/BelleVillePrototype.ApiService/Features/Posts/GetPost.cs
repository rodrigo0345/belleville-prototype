using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Infrastructure;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using LanguageExt.Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class GetPost
{
    public class Command : IRequest<Result<PostEntity>>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
    
    public record ControllerResult(PostId Id, string Title, string? Author);

    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<PostEntity>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        
        public async Task<Result<PostEntity>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new Result<PostEntity>(new ValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage.ToString()));
            }
            
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (post is null)
                {
                    return new Result<PostEntity>(new Exception("Post não existe"));
                }
                return new Result<PostEntity>(post);
            }
            catch (Exception e)
            {
                return new Result<PostEntity>(e);
            }
        }
    }
}
public class GetPostEndpoint : ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("posts", async ([FromQuery] Guid id, ISender sender) =>
        {
            var command = new GetPost.Command()
            {
                Id = id
            };
            
            var result = await sender.Send(command);
            return result.Match<IResult>(
                entity => Results.Ok(entity.Adapt<GetPost.ControllerResult>()),
                error => Results.Problem(detail: error.Message, statusCode: 400));
        }).IncludeInOpenApi();
    }
}

