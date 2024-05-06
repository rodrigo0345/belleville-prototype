using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.UserContract;

public class QueryUser
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    
    public string Username { get; set; } = string.Empty;
    public UserEntityRole Role { get; set; } = UserEntityRole.User; 
}