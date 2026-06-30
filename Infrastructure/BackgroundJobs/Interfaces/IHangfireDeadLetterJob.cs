using Hangfire;

namespace Infrastructure.BackgroundJobs.Interfaces
{
    public interface IHangfireDeadLetterJob
    {
        [JobDisplayName("Başarısız job arşivleme")]
        Task ProcessAsync(HangfireDeadLetterPayload payload);
    }
}
