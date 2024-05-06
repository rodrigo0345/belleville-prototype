namespace BelleVillePrototype.ApiService.Contracts;

public class CreatePostRequest
{
    public string Title { get; set; } = String.Empty;
    public string? Author { get; set; }  
}