
namespace BelleVillePrototype.ApiService.Contracts.PostContract;

[Json(typeof(StringEnumConverter))]
public enum Order
{
  [EnumMember(Value = "ASC")]
    ASC = 0,

  [EnumMember(Value = "DESC")]
    DESC = 1
}

public class QueryPosts
{
    public Guid? Id { get; set; }

    public string OrderBy { get; set; } = "id";
    public Order Order { get; set; } = Order.ASC;

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Title { get; set; } = String.Empty;
    public string? Author { get; set; } = String.Empty;
}
