namespace BelleVillePrototype.ApiService.Contracts.UserContract;

public class LoginUserCommand
{
    public string Email { get; set; }
    public string Password { get; set; }
}