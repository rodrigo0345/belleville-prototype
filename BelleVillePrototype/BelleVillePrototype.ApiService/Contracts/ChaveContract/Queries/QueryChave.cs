using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Contracts.PostContract;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ChaveContract.Queries;

public class QueryChave : QueryInterface
{
    public Guid? Id { get; set; }
    public string? Codigo { get; set; }
    public Guid? ImovelId { get; set; } 
    public bool IsDeleted { get; set; } = false;
    
    public string? OrderBy { get; set; } = "id";
    public Order? Order { get; set; } = PostContract.Order.ASC;
    public int? PageSize { get; set; } = 20;
    public int? Page { get; set; } = 0;

    public string? FilterTimeBy { get; set; } = null;

    public DateTime? FilterStartDate { get; set; } = null;
    public DateTime? FilterEndDate { get; set; } = null;
}