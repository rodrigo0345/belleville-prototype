namespace BelleVillePrototype.ApiService.Contracts.UserContract;

public class RegisterUserCommand
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
}