using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Posts;
using BelleVillePrototype.ApiService.Shared.Result;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelleVillePrototype.ApiService.Features.Posts;

public static class GetPost
{
    public class Command : IRequest<Result<PostModel>>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }

    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<PostModel>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }
        
        public async Task<Result<PostModel>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new Result<PostModel>(null, error: validationResult.ToString());
            }
            
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (post is null)
                {
                    return new Result<PostModel>(null, error: "Post not found");
                }
                return new Result<PostModel>(post);
            }
            catch (Exception e)
            {
                return new Result<PostModel>(null, error: e.Message);
            }
        }
    }
}

public class GetPostController: ControllerBase
{
    
    public record ControllerResult(PostId Id, string Title, string? Author);
    
    [HttpGet("random")]
    public async Task<ActionResult<ControllerResult>> GetPost([FromBody] GetPost.Command command, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(command);
        if (result.Error.IsSome)
        {
            var error = result.Error.OrElse("Some error occurred, no message was left");
            return BadRequest(error);
        }

        var content = result.Content.OrElseThrow();
        return Ok(new ControllerResult(content.Id, content.Title, content.Author));
    }
} 
