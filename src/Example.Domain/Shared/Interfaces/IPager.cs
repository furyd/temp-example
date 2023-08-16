namespace Example.Domain.Shared.Interfaces;

public interface IPager<T>
{
    int Page { get; }

    int PageSize { get; }

    int ActualPageSize { get; }

    ICollection<T> Records { get; }
}