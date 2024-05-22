
namespace BelleVillePrototype.ApiService.Contracts.PostContract;
using System;
using System.ComponentModel.DataAnnotations;

public enum Order
{
    ASC = 0,
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
