using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using salesdesk_api.Shared.Host.WebApi.Telemetry;

namespace salesdesk_api.Infrastructure.Filters
{
    public class ValidationFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly ILocalizationService _localizationService;

        public ValidationFilterAttribute(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors.Select(error =>
                        string.IsNullOrWhiteSpace(x.Key)
                            ? error.ErrorMessage
                            : $"{x.Key}: {error.ErrorMessage}"))
                    .ToList();

                var response = ApiResponse<object>.ErrorResult(
                    _localizationService.GetLocalizedString("General.RequiredFieldsNotFilled"),
                    _localizationService.GetLocalizedString("General.RequiredFieldsNotFilled"),
                    StatusCodes.Status400BadRequest,
                    errorCode: ApiErrorCodes.ValidationFailed);

                response.Errors = errors;
                RequestTelemetryContext.SetErrorCode(context.HttpContext, ApiErrorCodes.ValidationFailed);
                RequestTelemetryContext.SetFailureReason(context.HttpContext, "ModelStateInvalid");
                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}
