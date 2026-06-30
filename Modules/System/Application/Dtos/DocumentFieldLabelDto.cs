using System.ComponentModel.DataAnnotations;

namespace salesdesk_api.Modules.System.Application.Dtos
{
    public class DocumentFieldLabelDto
    {
        public long Id { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string FieldKey { get; set; } = string.Empty;
        public string DefaultLabel { get; set; } = string.Empty;
        public string? CustomLabel { get; set; }
        public string EffectiveLabel { get; set; } = string.Empty;
        public string? HelpText { get; set; }
        public string? Placeholder { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateDocumentFieldLabelDto
    {
        [Required]
        [MaxLength(30)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Scope { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string FieldKey { get; set; } = string.Empty;

        [MaxLength(80)]
        public string? CustomLabel { get; set; }

        [MaxLength(500)]
        public string? HelpText { get; set; }

        [MaxLength(120)]
        public string? Placeholder { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateDocumentFieldLabelsRequest
    {
        public List<UpdateDocumentFieldLabelDto> Items { get; set; } = new();
    }
}
