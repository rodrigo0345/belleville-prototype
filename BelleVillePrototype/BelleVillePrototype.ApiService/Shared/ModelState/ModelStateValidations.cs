using System.Runtime.InteropServices.JavaScript;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BelleVillePrototype.ApiService.Shared.ModelState;

public static class ModelStateValidations
{
    public static Result<string> Validate(this ModelStateDictionary model)
    {
        if (model.IsValid)
        {
            return new Result<string>("Model is valid");
        }

        var error = model.Values.FirstOrDefault()?.Errors.FirstOrDefault();
        return new Result<string>(new Exception(error?.ErrorMessage ?? "Model is invalid"));
    }
}