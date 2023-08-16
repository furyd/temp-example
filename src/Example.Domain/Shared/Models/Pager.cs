using Example.Domain.Shared.Interfaces;

namespace Example.Domain.Shared.Models;

public readonly record struct Pager<T>(int Page, int PageSize, ICollection<T> Records) : IPager<T>
{
    public int ActualPageSize => Records.Count;
}