using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Posts;
using BelleVillePrototype.ApiService.Shared.Result;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelleVillePrototype.ApiService.Features.Posts;

public static class CreatePost
{
    public class Command : IRequest<Result<Guid>>
    {
        public string Title { get; set; } = String.Empty;
        public string? Author { get; set; } 
    }
    
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

        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        
        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new Result<Guid>(Guid.Empty, error: validationResult.ToString());
            }
            var post = new PostModel { Title = request.Title, Author = request.Author };
            
            try
            {
                await _dbContext.Posts.AddAsync(post, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new Result<Guid>(content: post.Id.Value);
            }
            catch (Exception e)
            {
                return new Result<Guid>(Guid.Empty, error: e.Message);
            }
        }
    }
}

[Route("api/posts")]
public class CreatePostController : ControllerBase
{
    public struct ControllerResult
    {
        public Guid id { get; set; } 
    }
    
    [HttpPost("random")]
    public async Task<ActionResult<ControllerResult>> CreatePostAsync([FromBody] CreatePost.Command command, [FromServices] IMediator mediator)
    {
        var postId = await mediator.Send(command);
        if (postId.Error.IsSome)
        {
            var error = postId.Error.OrElse("Some error occurred, no message was left");
            return BadRequest(error);
        }
        
        var content = postId.Content.OrElseThrow();
        return Ok(new ControllerResult
        {
            id = content
        });
    }
} 
