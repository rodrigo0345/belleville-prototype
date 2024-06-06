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
using Microsoft.EntityFrameworkCore;

namespace BelleVillePrototype.ApiService.Features.Imoveis;

[Controller]
public class DeleteImovel: ControllerBase
{
    [HttpDelete("/imoveis")]
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

        var request = new DeleteImovelHandler.Command()
        {
            Id = id
        };
        var response = await sender.Send(request);

        return response.Match<IActionResult>(
            item => Ok(item),
            error => BadRequest(error.Message));
    }
}

public static class DeleteImovelHandler 
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
                    return new Result<ResultImovel>(new ValidationException("Imóvel não existe"));
                }
                
                // é preciso apagar todas as chaves associadas
                var chaves = await _dbContext.Chaves.Where(chave => chave.ImovelId == found.Id).ToListAsync(cancellationToken);
                _dbContext.Chaves.RemoveRange(chaves);
                
                _dbContext.Imoveis.Remove(found);
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                // não sei pq hei de retornar isto mas aqui prontos, nem me chateio muito
                return new Result<ResultImovel>(found.Adapt<ResultImovel>());
            }
            catch (Exception e)
            {
                return new Result<ResultImovel>(e);
            }
        }
    }
}
