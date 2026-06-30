namespace salesdesk_api.Modules.System.Domain.Entities
{
    public class DocumentFieldLabel : BaseEntity
    {
        public string DocumentType { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string FieldKey { get; set; } = string.Empty;
        public string DefaultLabel { get; set; } = string.Empty;
        public string? CustomLabel { get; set; }
        public string? HelpText { get; set; }
        public string? Placeholder { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
