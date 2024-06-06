using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ImovelContract.Commands;

public class CreateImovelCommand
{
    [Required(ErrorMessage = "A Morada é obrigatória")]
    public string Morada { get; set; }
    
    public string? Localidade { get; set; }
    
    public string? CodigoPostal { get; set; }
    
    [Required(ErrorMessage = "O Tipo de imóvel é obrigatório")]
    public ImovelType Tipo { get; set; } 
    
    [Required(ErrorMessage = "A Classificação do imóvel é obrigatória")]
    public ImovelClassifier Classificacao { get; set; }
}