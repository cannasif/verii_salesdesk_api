using System;

namespace salesdesk_api.Shared.Domain.Entities.Common
{
    public abstract class BaseHeaderEntity : BaseEntity
    {
        public string Year { get; set; } = DateTime.UtcNow.Year.ToString();

        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
