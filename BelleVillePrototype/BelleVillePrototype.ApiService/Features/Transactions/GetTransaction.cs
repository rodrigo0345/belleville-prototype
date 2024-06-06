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
public class GetTransaction: ControllerBase
{
    [HttpGet("/transactions/{id:Guid}")]
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

        var request = new GetTransactionHandler.Command()
        {
            Id = id
        };
        var response = await sender.Send(request);

        return response.Match<IActionResult>(
            item => Ok(item),
            error => BadRequest(error.Message));
    }
}

public static class GetTransactionHandler 
{
    public class Command: IRequest<Result<ResultTransaction>>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Command, Result<ResultTransaction>>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Result<ResultTransaction>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var found = await _dbContext.Transactions.FindAsync(new TransactionId(request.Id), cancellationToken);
                if (found is null)
                {
                    return new Result<ResultTransaction>(new ValidationException("Transação não encontrada"));
                }
                
                return new Result<ResultTransaction>(found.Adapt<ResultTransaction>());
            }
            catch (Exception e)
            {
                return new Result<ResultTransaction>(e);
            }
        }
    }
}
