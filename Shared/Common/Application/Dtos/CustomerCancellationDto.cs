using System.ComponentModel.DataAnnotations;

namespace salesdesk_api.Shared.Common.Application.Dtos
{
    public class CustomerCancellationDto
    {
        [MaxLength(500)]
        public string? Reason { get; set; }
    }
}
