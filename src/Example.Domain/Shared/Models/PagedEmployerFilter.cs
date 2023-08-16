namespace Example.Domain.Shared.Models;

public readonly record struct PagedEmployerFilter(int Page, int PageSize, Guid Employer);