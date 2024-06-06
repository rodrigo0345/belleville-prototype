using BelleVillePrototype.ApiService.Contracts.ImovelContract.Commands;
using BelleVillePrototype.ApiService.Contracts.ImovelContract.Results;
using BelleVillePrototype.ApiService.Contracts.UserContract;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Shared.ModelState;
using BelleVillePrototype.ApiService.Shared.Tokens;
using FluentValidation;
using LanguageExt.Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BelleVillePrototype.ApiService.Features.Chaves;

[ApiController]
public class GetChave: ControllerBase
{
    [HttpGet("/chaves/{id:Guid}")]
    public async Task<IActionResult> HandleAsync([FromRoute] Guid id, [FromServices] ISender sender)
    {
        var result = ModelState.Validate();
        var shouldNotReturn = result.Match<IActionResult?>(
            item => null,
            error => BadRequest(error.Message));

        if (shouldNotReturn is not null)
        {
            return shouldNotReturn;
        }

        var request = new GetChaveHandler.Command()
        {
            Id = id
        };
        var response = await sender.Send(request);

        return response.Match<IActionResult>(
            item => Ok(item),
            error => BadRequest(error.Message));
    }
}

public static class GetChaveHandler 
{
    public class Command: IRequest<Result<ResultChave>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<ResultChave>>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Result<ResultChave>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var found = await _dbContext.Chaves.FindAsync(new ChaveId(request.Id), cancellationToken);
                if (found is null)
                {
                    return new Result<ResultChave>(new ValidationException("Chave não encontrada"));
                }
                
                return new Result<ResultChave>(found.Adapt<ResultChave>());
            }
            catch (Exception e)
            {
                return new Result<ResultChave>(e);
            }
        }
    }
}
