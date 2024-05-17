using BelleVillePrototype.ApiService.Contracts;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Features.Posts;
using BelleVillePrototype.ApiService.Infrastructure;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using LanguageExt.Common;
using Mapster;
using MediatR;

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
                    return new Result<Guid>(new ValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage.ToString()));
                }
                var post = new PostEntity { Title = request.Title, Author = request.Author };
                
                try
                {
                    _dbContext.Posts.Add(post);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("Post created: {PostId}", post.Id);
                    return new Result<Guid>(post.Id.Value);
                }
                catch (Exception e)
                {
                    return new Result<Guid>(e);
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
            return result.Match<IResult>(
                item => Results.Ok(new CreatePost.ControllerResult(item)),
                error => Results.Problem(detail: error.Message, statusCode: 400)
            );
        }).IncludeInOpenApi().RequireAuthorization("Admin");
    }
}
