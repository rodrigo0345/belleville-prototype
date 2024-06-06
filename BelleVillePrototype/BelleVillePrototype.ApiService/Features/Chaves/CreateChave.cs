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
public class CreateChave: ControllerBase
{
    [HttpPost("/chaves")]
    public async Task<IActionResult> HandleAsync([FromBody] CreateChaveCommand command, [FromServices] ISender sender)
    {
        var result = ModelState.Validate();
        var shouldNotReturn = result.Match<IActionResult?>(
            item => null,
            error => BadRequest(error.Message));

        if (shouldNotReturn is not null)
        {
            return shouldNotReturn;
        }

        var request = command.Adapt<CreateNewChave.Command>();
        var response = await sender.Send(request);

        return response.Match<IActionResult>(
            item => Ok(item),
            error => BadRequest(error.Message));
    }
}

public static class CreateNewChave
{
    public class Command : CreateChaveCommand, IRequest<Result<ResultChave>>
    {
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
            
            ChaveEntity entity =  new ChaveEntity() 
            {
                ImovelId = new ImovelId(request.ImovelId),
                Codigo = request.Codigo!,
            };

            try
            {
                // check if the imovel exists
                var found = await _dbContext.Imoveis.FindAsync(entity.ImovelId, cancellationToken);
                if (found is null)
                {
                    return new Result<ResultChave>(new Exception("O Imovel selecionado não existe."));
                }
                
                await _dbContext.Chaves.AddAsync(entity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return new ResultChave()
                {
                    Id = entity.Id,
                    Codigo = entity.Codigo,
                    ImovelId = entity.ImovelId,
                };
            }
            catch (Exception e)
            {
                return new Result<ResultChave>(e);
            }
        }
    }
}
