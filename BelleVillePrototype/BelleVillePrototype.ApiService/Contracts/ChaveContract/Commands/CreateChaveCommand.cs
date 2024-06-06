using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ImovelContract.Commands;

public class CreateChaveCommand
{
    [MinLength(6, ErrorMessage = "O Código da Chave deve ter 6 caracteres")]
    [MaxLength(6, ErrorMessage = "O Código da Chave deve ter 6 caracteres")]
    public string? Codigo { get; set; }

    [Required(ErrorMessage = "O Id do Imóvel é obrigatório")]
    public Guid ImovelId { get; set; }
    
    public bool IsDeleted { get; set; } = false;
}