using BelleVillePrototype.ApiService.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BelleVillePrototype.ApiService.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelleVillePrototype.ApiService.Entities;

[Table("Transaction")]
[EntityTypeConfiguration(typeof(TransactionConfiguration))]

public class TransactionEntity: BaseEntityInterface
{
    [Key]
    public TransactionId Id { get; set; } = TransactionId.Empty();
    public bool IsActive { get; set; }
    public DateTime Data { get; set; }
    public TransactionType Tipo { get; set; }
    
    public ChaveId ChaveId { get; set; }
    public ChaveEntity Chave { get; set; }
    
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }
    
    // apenas deve ser utilizado quando a chave está em tipo externo
    public string? Phone { get; set; } = string.Empty;
    
    // comum a todas as entidades
    public bool IsDeleted { get; set; }
}

public enum TransactionType
{
    Externo,
    Interno 
}

public class TransactionConfiguration: IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.Property(b => b.Id)
            .HasConversion(new TransactionIdValueConverter())
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Data)
            .IsRequired();
        
        builder.Property(b => b.Tipo)
            .IsRequired();
        
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);

        builder.Property(b => b.ChaveId).HasConversion(new ChaveIdValueConverter());

        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}
public readonly record struct TransactionId(Guid Value): IComparable<TransactionId>
{
    public static implicit operator Guid(TransactionId id) => id.Value;

    public int CompareTo(TransactionId other) => Value.CompareTo(other.Value);
    public static TransactionId Empty() => new(Guid.Empty);
    public static TransactionId NewTransactionId => new TransactionId(Guid.NewGuid());
}

public class TransactionIdValueConverter : ValueConverter<TransactionId, Guid>
{
    public TransactionIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(
            id => id.Value,
            value => new TransactionId(value),
            mappingHints
        ) { }
}
