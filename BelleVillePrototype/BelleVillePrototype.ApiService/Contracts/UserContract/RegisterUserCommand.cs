using System.ComponentModel.DataAnnotations;

namespace BelleVillePrototype.ApiService.Contracts.UserContract;

public class RegisterUserCommand
{
    [EmailAddress(ErrorMessage = "Email inválido")]
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [Phone(ErrorMessage = "Número de telefone inválido")]
    public string Phone { get; set; }
    
    [Required]
    public string Username { get; set; }
}