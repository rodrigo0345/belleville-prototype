using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using BelleVillePrototype.ApiService.OptionType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BelleVillePrototype.ApiService.Posts;


[Table("Posts")]
[EntityTypeConfiguration(typeof(PostEntityConfiguration))]
public class PostModel 
{
    [Key]
    public PostId Id { get; set; } = PostId.Empty();
    public string Title { get; set; } = string.Empty;
    public string? Author { get; set; }
}

public class PostEntityConfiguration: IEntityTypeConfiguration<PostModel>
{
    public void Configure(EntityTypeBuilder<PostModel> builder)
    {
        builder
            .Property(b => b.Title)
            .IsRequired();
        
        builder.Property(b => b.Id)
            .HasConversion(new PostIdValueConverter());
    }
}
public readonly record struct PostId(Guid Value): IComparable<PostId>
{
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
