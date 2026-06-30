using System.Collections.Generic;
using salesdesk_api.Infrastructure.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace salesdesk_api.Shared.Common.Application
{
    public class Filter
    {
        public string Column { get; set; } = string.Empty;
        public string Operator { get; set; } = "Equals";
        public string Value { get; set; } = string.Empty;
    }

    [ModelBinder(BinderType = typeof(PagedRequestModelBinder))]
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? SortBy { get; set; } = "Id";
        public string? SortDirection { get; set; } = "desc";
        public List<Filter>? Filters { get; set; } = new();
        /// <summary>
        /// "and" veya "or" — filtrelerin nasıl birleştirileceğini belirler. Varsayılan: "and"
        /// </summary>
        public string FilterLogic { get; set; } = "and";
    }
}
