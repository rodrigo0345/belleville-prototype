namespace BelleVillePrototype.ApiService.Contracts.UserContract;

public class LoginResult: QueryUser
{
    public string Token { get; set; } = string.Empty;
}