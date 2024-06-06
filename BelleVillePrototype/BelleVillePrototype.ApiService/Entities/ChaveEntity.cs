using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BelleVillePrototype.ApiService.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelleVillePrototype.ApiService.Entities;

[Table("Chave")]
[EntityTypeConfiguration(typeof(ChaveConfiguration))]

public class ChaveEntity: BaseEntityInterface
{
    [Key]
    public ChaveId Id { get; set; } = ChaveId.Empty();
    public string Codigo { get; set; } = string.Empty;
    
    // serve apenas para navegação mais acessível
    public ImovelId ImovelId { get; set; }
    public ImovelEntity Imovel { get; set; }
    
    // comum a todas as entidades
    public bool IsDeleted { get; set; }
}

public class ChaveConfiguration: IEntityTypeConfiguration<ChaveEntity>
{
    public void Configure(EntityTypeBuilder<ChaveEntity> builder)
    {
        builder
            .Property(b => b.Codigo)
            .HasMaxLength(6)
            .IsRequired();
        
        builder.Property(b => b.Id)
            .HasConversion(new ChaveIdValueConverter())
            .ValueGeneratedOnAdd();

        builder.Property(b => b.ImovelId)
            .HasConversion(new PropertyIdValueConverter());

        builder.Property(b => b.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}
public readonly record struct ChaveId(Guid Value): IComparable<ChaveId>
{
    public static implicit operator Guid(ChaveId id) => id.Value;

    public int CompareTo(ChaveId other) => Value.CompareTo(other.Value);
    public static ChaveId Empty() => new(Guid.Empty);
    public static ChaveId NewChaveId => new ChaveId(Guid.NewGuid());
}

public class ChaveIdValueConverter : ValueConverter<ChaveId, Guid>
{
    public ChaveIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(
            id => id.Value,
            value => new ChaveId(value),
            mappingHints
        ) { }
}
