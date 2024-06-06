using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BelleVillePrototype.ApiService.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelleVillePrototype.ApiService.Entities;

[Table("Imovel")]
[EntityTypeConfiguration(typeof(PropertyConfiguration))]
public class ImovelEntity: BaseEntityInterface
{
    [Key]
    public ImovelId Id { get; set; } = ImovelId.Empty();
    public string Morada { get; set; } = string.Empty;
    public string? Localidade { get; set; }
    public string? CodigoPostal { get; set; }
    public ImovelType Tipo { get; set; } 
    public ImovelClassifier Classificacao { get; set; }
    
    public List<ChaveEntity> Chaves { get; set; }
    public bool IsDeleted { get; set; }
}

public enum ImovelType
{
    Moradia,
    Apartamento,
    Terreno,
    Loja,
    Armazem
}

public enum ImovelClassifier
{
    Venda,
    Aluguer
}

public class PropertyConfiguration: IEntityTypeConfiguration<ImovelEntity>
{
    public void Configure(EntityTypeBuilder<ImovelEntity> builder)
    {
        builder
            .Property(b => b.Classificacao)
            .IsRequired();
        
        builder.Property(b => b.Id)
            .HasConversion(new PropertyIdValueConverter())
            .ValueGeneratedOnAdd();
        
        builder.Property(b => b.Tipo)
            .IsRequired();

        builder.Property(b => b.Classificacao)
            .IsRequired();
        
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        
        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}
public readonly record struct ImovelId(Guid Value): IComparable<ImovelId>
{
    public static implicit operator Guid(ImovelId imovelId) => imovelId.Value;

    public int CompareTo(ImovelId other) => Value.CompareTo(other.Value);
    public static ImovelId Empty() => new(Guid.Empty);
    public static ImovelId NewPostId => new ImovelId(Guid.NewGuid());
}

public class PropertyIdValueConverter : ValueConverter<ImovelId, Guid>
{
    public PropertyIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(
            id => id.Value,
            value => new ImovelId(value),
            mappingHints
        ) { }
}
