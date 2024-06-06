using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Contracts.PostContract;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ImovelContract.Queries;

public class QueryImovel : QueryInterface
{
    public Guid? Id { get; set; }
    
    [Required(ErrorMessage = "A Morada é obrigatória")]
    public string Morada { get; set; }

    public string? Localidade { get; set; }

    public string? CodigoPostal { get; set; }

    [Required(ErrorMessage = "O Tipo de imóvel é obrigatório")]
    public ImovelType Tipo { get; set; }

    [Required(ErrorMessage = "A Classificação do imóvel é obrigatória")]
    public ImovelClassifier Classificacao { get; set; }

    public string? OrderBy { get; set; } = "id";
    public Order? Order { get; set; } = PostContract.Order.ASC;
    public int? PageSize { get; set; } = 20;
    public int? Page { get; set; } = 0;

    public string? FilterTimeBy { get; set; } = null;

    public DateTime? FilterStartDate { get; set; } = null;
    public DateTime? FilterEndDate { get; set; } = null;
}