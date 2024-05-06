using BelleVillePrototype.ApiService.Entities;
using Microsoft.AspNetCore.Identity;

namespace BelleVillePrototype.ApiService.Shared.Tokens;

public interface ITokenService 
{
    public Task<string> GenerateToken(UserEntity user, uint expirationMinutes = 30);
}