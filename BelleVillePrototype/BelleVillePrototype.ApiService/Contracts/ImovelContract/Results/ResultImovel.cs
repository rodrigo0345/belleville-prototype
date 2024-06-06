using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.ImovelContract.Results;

public class ResultImovel
{
    public Guid? Id { get; set; }
    public string Morada { get; set; }
    public string? Localidade { get; set; }
    public string? CodigoPostal { get; set; }
    public ImovelType Tipo { get; set; }
    public ImovelClassifier Classificacao { get; set; }
}