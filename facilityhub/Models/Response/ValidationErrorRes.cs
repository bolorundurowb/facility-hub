using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FacilityHub.Models.Response;

public class ValidationErrorRes
{
    public Dictionary<string, string?> Errors { get; }

    public ValidationErrorRes(ModelStateDictionary modelState)
    {
        Errors = modelState.Where(x => x.Value != null && x.Value.Errors.Any())
            .Select(x => new
            {
                x.Key,
                Error = x.Value?.Errors.First()
            })
            .ToDictionary(x => x.Key, y => y.Error?.ErrorMessage.Replace("'", string.Empty));
    }
}
