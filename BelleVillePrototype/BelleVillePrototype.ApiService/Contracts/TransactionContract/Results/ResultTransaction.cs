using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ImovelContract.Results;

public class ResultTransaction
{
    public Guid Id { get; set; } 
    public bool IsActive { get; set; } = true;
    public DateTime Data { get; set; }
    
    public TransactionType Tipo { get; set; }
    
    public Guid ChaveId { get; set; }
    
    public Guid? UserId { get; set; }
    
    public string? Phone { get; set; } = string.Empty;
    
    public bool IsDeleted { get; set; }
}