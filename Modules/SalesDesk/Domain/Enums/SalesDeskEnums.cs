namespace salesdesk_api.Modules.SalesDesk.Domain.Enums;

public enum SalesDeskCustomerKind
{
    Customer = 1,
    Supplier = 2,
    Both = 3
}

public enum SalesDeskPotentialStatus
{
    Waiting = 1,
    Found = 2,
    Suspicious = 3,
    Strong = 4,
    Converted = 5,
    NotFound = 6
}

public enum SalesDeskDocumentStatus
{
    Draft = 1,
    Pending = 2,
    Approved = 3,
    ConvertedToOrder = 4,
    ToBeIssued = 5,
    Issued = 6,
    Cancelled = 7
}

public enum SalesDeskPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public enum SalesDeskTaskStatus
{
    Open = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}

public enum SalesDeskVisitStatus
{
    Planned = 1,
    Done = 2,
    Cancelled = 3
}

public enum SalesDeskFixedAssetStatus
{
    Active = 1,
    InMaintenance = 2,
    Scrapped = 3
}

public enum SalesDeskRecurringPaymentType
{
    Expense = 1,
    Income = 2
}
