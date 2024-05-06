using BelleVillePrototype.ApiService.Contracts;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Features.Posts;
using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Shared.Result;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelleVillePrototype.ApiService.Features.Posts
{
    public static class CreatePost
    {
        public class Command : IRequest<Result<Guid>>
        {
            public string Title { get; set; } = String.Empty;
            public string? Author { get; set; } 
        }

        public record ControllerResult(Guid Id);
        
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty();
            }
        }

        internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
        {
            private readonly ApplicationDbContext _dbContext;
            private readonly IValidator<Command> _validator;
            private readonly ILogger _logger;

            public Handler(ApplicationDbContext dbContext, IValidator<Command> validator, ILogger logger)
            {
                _dbContext = dbContext;
                _validator = validator;
                _logger = logger;
            }
            
            public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return new Result<Guid>(Guid.Empty, error: validationResult.ToString());
                }
                var post = new PostEntity { Title = request.Title, Author = request.Author };
                
                try
                {
                    _dbContext.Posts.Add(post);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("Post created: {PostId}", post.Id);
                    return new Result<Guid>(content: post.Id.Value);
                }
                catch (Exception e)
                {
                    return new Result<Guid>(Guid.Empty, error: e.Message);
                }
            }
        }
    }
}
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("posts", async (CreatePostRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreatePost.Command>();
            
            var result = await sender.Send(command);
            if (result.Error.IsSome)
            {
                var error = result.Error.OrElse("Some error occurred, no message was left");
                return Results.Problem(detail: error, statusCode: 500);
            }

            var content = result.Content.OrElseThrow();
            return Results.Ok(new CreatePost.ControllerResult(content));
        }).IncludeInOpenApi().RequireAuthorization("Admin");
    }
}
