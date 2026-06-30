using salesdesk_api.Modules.Identity.Application.Services;
using salesdesk_api.Modules.System.Application.Dtos;
using salesdesk_api.Modules.System.Application.Services;
using salesdesk_api.Shared.Common.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Modules.System.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentFieldLabelsController : ControllerBase
    {
        private readonly IDocumentFieldLabelService _documentFieldLabelService;
        private readonly IUserService _userService;

        public DocumentFieldLabelsController(
            IDocumentFieldLabelService documentFieldLabelService,
            IUserService userService)
        {
            _documentFieldLabelService = documentFieldLabelService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<DocumentFieldLabelDto>>>> Get(
            [FromQuery] string? documentType,
            [FromQuery] string? scope)
        {
            var response = await _documentFieldLabelService.GetAsync(documentType, scope).ConfigureAwait(false);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<List<DocumentFieldLabelDto>>>> Update(
            [FromBody] UpdateDocumentFieldLabelsRequest request)
        {
            return await UpdateInternal(request).ConfigureAwait(false);
        }

        [HttpPost("update")]
        public async Task<ActionResult<ApiResponse<List<DocumentFieldLabelDto>>>> UpdateViaPost(
            [FromBody] UpdateDocumentFieldLabelsRequest request)
        {
            return await UpdateInternal(request).ConfigureAwait(false);
        }

        private async Task<ActionResult<ApiResponse<List<DocumentFieldLabelDto>>>> UpdateInternal(
            UpdateDocumentFieldLabelsRequest request)
        {
            var currentUserResponse = await _userService.GetCurrentUserIdAsync().ConfigureAwait(false);
            if (!currentUserResponse.Success)
            {
                var unauth = ApiResponse<List<DocumentFieldLabelDto>>.ErrorResult(
                    currentUserResponse.Message,
                    currentUserResponse.Message,
                    StatusCodes.Status401Unauthorized);

                return StatusCode(unauth.StatusCode, unauth);
            }

            var response = await _documentFieldLabelService.UpdateAsync(request, currentUserResponse.Data).ConfigureAwait(false);
            return StatusCode(response.StatusCode, response);
        }
    }
}
