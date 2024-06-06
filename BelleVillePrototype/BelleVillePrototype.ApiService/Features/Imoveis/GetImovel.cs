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

namespace BelleVillePrototype.ApiService.Features.Imoveis;

[Controller]
public class GetImovel: ControllerBase
{
    [HttpGet("/imoveis")]
    public async Task<IActionResult> HandleAsync([FromQuery] Guid id, [FromServices] ISender sender)
    {
        var result = ModelState.Validate();
        var shouldNotReturn = result.Match<IActionResult?>(
            item => null,
            error => BadRequest(error.Message));

        if (shouldNotReturn is not null)
        {
            return shouldNotReturn;
        }

        var request = new GetImovelHandler.Command()
        {
            Id = id
        };
        var response = await sender.Send(request);

        return response.Match<IActionResult>(
            item => Ok(item),
            error => BadRequest(error.Message));
    }
}

public static class GetImovelHandler 
{
    public class Command: IRequest<Result<ResultImovel>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<ResultImovel>>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Result<ResultImovel>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var found = await _dbContext.Imoveis.FindAsync(new ImovelId(request.Id), cancellationToken);
                if (found is null)
                {
                    return new Result<ResultImovel>(new ValidationException("Imóvel não encontrado"));
                }
                
                return new Result<ResultImovel>(found.Adapt<ResultImovel>());
            }
            catch (Exception e)
            {
                return new Result<ResultImovel>(e);
            }
        }
    }
}
