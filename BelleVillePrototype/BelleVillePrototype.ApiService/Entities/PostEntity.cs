using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelleVillePrototype.ApiService.Entities;


[Table("Posts")]
[EntityTypeConfiguration(typeof(PostEntityConfiguration))]
public class PostEntity
{
    [Key]
    public PostId Id { get; set; } = PostId.Empty();
    public string Title { get; set; } = string.Empty;
    public string? Author { get; set; }
}

public class PostEntityConfiguration: IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder
            .Property(b => b.Title)
            .IsRequired();
        
        builder.Property(b => b.Id)
            .HasConversion(new PostIdValueConverter())
            .ValueGeneratedOnAdd();
    }
}
public readonly record struct PostId(Guid Value): IComparable<PostId>
{
    public static implicit operator Guid(PostId postId) => postId.Value;
    public int CompareTo(PostId other) => Value.CompareTo(other.Value);
    public static PostId Empty() => new(Guid.Empty);
    public static PostId NewPostId => new PostId(Guid.NewGuid());
}

public class PostIdValueConverter : ValueConverter<PostId, Guid>
{
    public PostIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(
            id => id.Value,
            value => new PostId(value),
            mappingHints
        ) { }
}