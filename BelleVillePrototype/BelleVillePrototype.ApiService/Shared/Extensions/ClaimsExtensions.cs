using System.Security.Claims;
using BelleVillePrototype.ApiService.OptionType;
using Google.Protobuf.WellKnownTypes;

namespace BelleVillePrototype.ApiService.Shared.Extensions;

public static class ClaimsExtensions
{
    public static Option<string> GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        string? claim = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        return string.IsNullOrWhiteSpace(claim) ? Option<string>.None() : Option<string>.Some(claim);
    }
}