using BelleVillePrototype.ApiService.Contracts.ImovelContract.Commands;
using BelleVillePrototype.ApiService.Contracts.ImovelContract.Results;
using BelleVillePrototype.ApiService.Contracts.TransactionContract.Commands;
using BelleVillePrototype.ApiService.Contracts.UserContract;
using BelleVillePrototype.ApiService.Entities;
using BelleVillePrototype.ApiService.Infrastructure;
using BelleVillePrototype.ApiService.Shared.ModelState;
using BelleVillePrototype.ApiService.Shared.Tokens;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace BelleVillePrototype.ApiService.Features.Chaves;

[ApiController]
public class CreateTransaction: ControllerBase
{
    [HttpPost("/transactions")]
    public async Task<IActionResult> HandleAsync([FromBody] CreateTransactionCommand command, [FromServices] ISender sender)
    {
        var result = ModelState.Validate();
        var shouldNotReturn = result.Match<IActionResult?>(
            item => null,
            error => BadRequest(error.Message));

        if (shouldNotReturn is not null)
        {
            return shouldNotReturn;
        }

        var request = command.Adapt<CreateNewTransaction.Command>();
        var response = await sender.Send(request);

        return response.Match<IActionResult>(
            item => Ok(item),
            error => BadRequest(error.Message));
    }
}

public static class CreateNewTransaction
{
    public class Command : CreateTransactionCommand, IRequest<Result<ResultTransaction>>
    {
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
            
            TransactionEntity entity =  new TransactionEntity() 
            {
                UserId = request.UserId,
                ChaveId = new ChaveId(request.ChaveId),
                Data = request.Data,
                Tipo = request.Tipo,
                Phone = request.Phone,
                IsActive = true
            };

            // caso a transação seja interna, não é necessário o telefone
            // caso a transação seja externa, não é necessário o UserId que não irá haver nenhum utilizador associado,
            // mas é necessário deixar o telefone do funcionário
            if (request.Tipo == TransactionType.Interno)
            {
                request.Phone = null;
            } else if (request.Tipo == TransactionType.Externo)
            {
                request.UserId = null;
                if(request.Phone is null) return new Result<ResultTransaction>(new Exception("O Telefone é obrigatório para transações externas."));
            }

            try
            {
                // check if the chave exists
                var findChave= _dbContext.Chaves.FindAsync(entity.ChaveId, cancellationToken);

                ValueTask<UserEntity>? findUser = default;
                if(entity.Tipo == TransactionType.Externo)
                {
                    // check if the user exists
                    findUser = _dbContext.Users.FindAsync(entity.UserId, cancellationToken);
                }
                
                await Task.WhenAll(findChave.AsTask(), findUser.AsTask());

                if (await findChave is null)
                {
                    return new Result<ResultTransaction>(new Exception("A Chave selecionada não existe."));
                }
                
                if(entity.Tipo == TransactionType.Externo && await findUser.AsTask() is null)
                {
                    return new Result<ResultTransaction>(new Exception("O Utilizador selecionado não existe."));
                }
                
                // é preciso desativar a ultima transacao da chave
                using var transaction = _dbContext.Database.BeginTransaction();
                
                var lastActiveTransactions = _dbContext.Transactions.Where(
                    tr  => tr.IsActive == true && tr.ChaveId == entity.ChaveId);
                
                await lastActiveTransactions.ForEachAsync(tr => tr.IsActive = false, cancellationToken);
                
                await _dbContext.Transactions.AddAsync(entity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                // commit the transaction
                await transaction.CommitAsync(cancellationToken);
                
                return new ResultTransaction()
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    ChaveId = entity.ChaveId,
                    Data = entity.Data,
                    Tipo = entity.Tipo,
                    Phone = entity.Phone,
                    IsActive = entity.IsActive
                };
            }
            catch (Exception e)
            {
                return new Result<ResultTransaction>(e);
            }
        }
    }
}
