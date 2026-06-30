using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.System.Application.Services;

namespace salesdesk_api.Modules.System.Api
{
    [ApiController]
    [Route("api/hangfire")]
    [Authorize]
    public class HangfireController : ControllerBase
    {
        private readonly IHangfireMonitoringService _hangfireMonitoringService;
        private readonly ILocalizationService _localizationService;

        public HangfireController(
            IHangfireMonitoringService hangfireMonitoringService,
            ILocalizationService localizationService)
        {
            _hangfireMonitoringService = hangfireMonitoringService;
            _localizationService = localizationService;
        }

        [HttpGet("recurring-jobs")]
        public async Task<IActionResult> GetRecurringJobs()
        {
            return Ok(await _hangfireMonitoringService.GetRecurringJobsAsync().ConfigureAwait(false));
        }

        [HttpPost("recurring-jobs/{jobId}/trigger")]
        public async Task<IActionResult> TriggerRecurringJob([FromRoute] string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                return BadRequest(new { Message = _localizationService.GetLocalizedString("HangfireController.JobIdRequired") });
            }

            var result = await _hangfireMonitoringService.TriggerRecurringJobAsync(jobId).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound(new { Message = _localizationService.GetLocalizedString("HangfireController.RecurringJobNotFound") });
            }

            return Ok(result);
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            return Ok(await _hangfireMonitoringService.GetStatsAsync().ConfigureAwait(false));
        }

        [HttpGet("failed")]
        public Task<IActionResult> GetFailed([FromQuery] int from = 0, [FromQuery] int count = 20)
        {
            // Backward-compatible endpoint: artık sadece RII_JOB_FAILURE_LOG'dan okunur.
            return GetFailuresFromDb(from, count);
        }

        [HttpGet("failures-from-db")]
        public async Task<IActionResult> GetFailuresFromDb([FromQuery] int from = 0, [FromQuery] int count = 50)
        {
            return Ok(await _hangfireMonitoringService.GetFailuresAsync(from, count).ConfigureAwait(false));
        }

        [HttpGet("successes-from-db")]
        public async Task<IActionResult> GetSuccessesFromDb([FromQuery] int from = 0, [FromQuery] int count = 50)
        {
            return Ok(await _hangfireMonitoringService.GetSuccessesAsync(from, count).ConfigureAwait(false));
        }

        [HttpGet("dead-letter")]
        public async Task<IActionResult> GetDeadLetter([FromQuery] int from = 0, [FromQuery] int count = 20)
        {
            return Ok(await _hangfireMonitoringService.GetDeadLetterAsync(from, count).ConfigureAwait(false));
        }
    }
}
