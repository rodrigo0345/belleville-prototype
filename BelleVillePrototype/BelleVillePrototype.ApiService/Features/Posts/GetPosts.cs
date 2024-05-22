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
using BelleVillePrototype.ApiService.Contracts.PostContract;
using System;

public static class GetPosts
{
    public class Command : QueryPosts, IRequest<Result<IEnumerable<PostEntity>>>
    {

    }

    public class ControllerResult : QueryPosts;


    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
        }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<IEnumerable<PostEntity>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        private IQueryable<T> OrderBy<T>(IQueryable<T> query, Order order, Func<T, object> keySelector)
        {
            if (order == Order.ASC)
            {
                query = query.OrderBy(keySelector).AsQueryable();
            }
            else
            {
                query = query.OrderByDescending(keySelector).AsQueryable();
            }

            return query;
        }

        public async Task<Result<IEnumerable<PostEntity>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new Result<IEnumerable<PostEntity>>(new ValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage.ToString()));
            }

            try
            {
                var query = _dbContext.Posts.AsQueryable();


                if (!String.IsNullOrWhiteSpace(request.OrderBy))
                {

                    if (String.Compare(request.OrderBy, "Id", true) == 0)
                    {
                        query = this.OrderBy(query, request.Order, (el) => el.Id);
                    }
                    else if (String.Compare(request.OrderBy, "Title", true) == 0)
                    {
                        query = this.OrderBy(query, request.Order, (el) => el.Title);
                    }
                    else if (String.Compare(request.OrderBy, "Author", true) == 0)
                    {
                        query = this.OrderBy(query, request.Order, (el) => el.Author ?? el.Title);
                    }

                }

                query = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).AsQueryable();

                var posts = query.ToList();
                return posts ?? [];
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<PostEntity>>(e);
            }
        }
    }
}
public class GetPostsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

    }
}

[ApiController]
public class GetPostsController : ControllerBase
{
    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts([FromQuery] QueryPosts query, [FromServices] ISender sender, [FromServices] ILogger<GetPostsController> logger)
    {
        var command = query.Adapt<GetPosts.Command>();

        var result = await sender.Send(command);
        return result.Match<IActionResult>(
            entity => Ok(entity.Select(e => e.Adapt<GetPosts.ControllerResult>())),
            error =>
            {
                logger.LogError(error, "Error while getting posts");
                return BadRequest(error.Message);
            });
    }
}

