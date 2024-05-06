using Microsoft.AspNetCore.Identity;

namespace BelleVillePrototype.ApiService.Shared.Tokens;

public interface ITokenService<UserKeyType> where UserKeyType: struct, IEquatable<UserKeyType>
{
    public string GenerateToken(IdentityUser<UserKeyType> user, uint expirationMinutes = 30);
}