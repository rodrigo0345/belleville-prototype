using BelleVillePrototype.ApiService.Contracts.PostContract;

namespace BelleVillePrototype.ApiService.Contracts;

public interface QueryInterface
{
    public string? OrderBy { get; set; } 
    public Order? Order { get; set; }
    
    public int? PageSize { get; set; }
    public int? Page { get; set; } 
    
    public string? FilterTimeBy { get; set; }
    public DateTime? FilterStartDate { get; set; }
    public DateTime? FilterEndDate { get; set; }
}