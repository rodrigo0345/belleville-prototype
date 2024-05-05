using BelleVillePrototype.ApiService.Contracts;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Shared.Result;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new Result<PostEntity>(null, error: validationResult.ToString());
            }
            
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (post is null)
                {
                    return new Result<PostEntity>(null, error: "Post not found");
                }
                return new Result<PostEntity>(post);
            }
            catch (Exception e)
            {
                return new Result<PostEntity>(null, error: e.Message);
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
            if (result.Error.IsSome)
            {
                var error = result.Error.OrElse("Some error occurred, no message was left");
                return Results.Problem(detail: error, statusCode: 500);
            }

            var content = result.Content.OrElseThrow();
            return Results.Ok(new GetPost.ControllerResult(content.Id, content.Title, content.Author));
        }).IncludeInOpenApi();
    }
}

