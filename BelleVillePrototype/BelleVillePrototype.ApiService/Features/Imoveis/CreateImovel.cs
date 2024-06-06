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
public class CreateImovel: ControllerBase
{
    [HttpPost("/imoveis")]
    public async Task<IActionResult> HandleAsync([FromBody] CreateImovelCommand command, [FromServices] ISender sender)
    {
        
        var result = ModelState.Validate();
        var shouldNotReturn = result.Match<IActionResult?>(
            item => null,
            error => BadRequest(error.Message));

        if (shouldNotReturn is not null)
        {
            return shouldNotReturn;
        }

        var request = command.Adapt<CreateNewImovel.Command>();
        var response = await sender.Send(request);

        return response.Match<IActionResult>(
            item => Ok(item),
            error => BadRequest(error.Message));
    }
}

public static class CreateNewImovel 
{
    public class Command : CreateImovelCommand, IRequest<Result<ResultImovel>>
    {
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
            
            ImovelEntity entity =  new ImovelEntity() 
            {
                Morada = request.Morada,
                Localidade = request.Localidade,
                CodigoPostal = request.CodigoPostal,
                Tipo = request.Tipo,
                Classificacao = request.Classificacao
            };

            try
            {
                await _dbContext.Imoveis.AddAsync(entity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new ResultImovel()
                {
                    Id = entity.Id,
                    Morada = entity.Morada,
                    Localidade = entity.Localidade,
                    CodigoPostal = entity.CodigoPostal,
                    Tipo = entity.Tipo,
                    Classificacao = entity.Classificacao
                };
            }
            catch (Exception e)
            {
                return new Result<ResultImovel>(e);
            }
        }
    }
}
