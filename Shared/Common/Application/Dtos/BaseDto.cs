using System;

namespace salesdesk_api.Shared.Common.Application.Dtos
{
    public class BaseEntityDto
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedByFullUser { get; set; }
        public string? UpdatedByFullUser { get; set; }
        public string? DeletedByFullUser { get; set; }
    }

    public class BaseHeaderEntityDto : BaseEntityDto
    {
        public string Year { get; set; } = string.Empty;
        public DateTime? CompletionDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

    public abstract class BaseCreateDto
    {
    }

    public abstract class BaseUpdateDto
    {
    }
}
