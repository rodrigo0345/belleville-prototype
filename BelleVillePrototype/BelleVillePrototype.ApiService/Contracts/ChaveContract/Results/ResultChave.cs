using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ImovelContract.Results;

public class ResultChave
{
    public Guid? Id { get; set; }
    public string? Codigo { get; set; }
    public Guid? ImovelId { get; set; }

    public bool IsDeleted { get; set; } = false;
}