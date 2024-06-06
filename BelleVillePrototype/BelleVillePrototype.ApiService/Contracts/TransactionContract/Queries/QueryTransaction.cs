using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Contracts.PostContract;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ChaveContract.Queries;

public class QueryTransaction : QueryInterface
{
    public TransactionId? Id { get; set; } = TransactionId.Empty();
    public bool? IsActive { get; set; } = true;
    public DateTime? Data { get; set; }
    public TransactionType? Tipo { get; set; }
    public ChaveId? ChaveId { get; set; }
    public Guid? UserId { get; set; }
    
    // apenas deve ser utilizado quando a transacao está em tipo externo
    public string? Phone { get; set; } = string.Empty;
    
    public string? OrderBy { get; set; } = "id";
    public Order? Order { get; set; } = PostContract.Order.ASC;
    public int? PageSize { get; set; } = 20;
    public int? Page { get; set; } = 0;

    public string? FilterTimeBy { get; set; } = null;

    public DateTime? FilterStartDate { get; set; } = null;
    public DateTime? FilterEndDate { get; set; } = null;
}