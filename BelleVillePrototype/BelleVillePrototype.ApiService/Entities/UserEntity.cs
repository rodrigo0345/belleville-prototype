using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BelleVillePrototype.ApiService.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelleVillePrototype.ApiService.Entities;

[EntityTypeConfiguration(typeof(UserEntityConfiguration))]
public class UserEntity: IdentityUser<Guid>, BaseEntityInterface
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
    public List<TransactionEntity> Transactions { get; set; }

    public bool IsDeleted { get; set; }
}

public class UserEntityConfiguration: IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(b => b.FirstName)
            .IsRequired();

        builder.Property(b => b.LastName)
            .IsRequired();

        builder.Property(b => b.Email)
            .IsRequired();

        builder.Property(b => b.Phone)
            .IsRequired();
        
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        
        builder.HasQueryFilter(b => !b.IsDeleted);
    }
}

public enum UserEntityRole
{
    Admin,
    User
}
