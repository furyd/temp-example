namespace Example.Domain.Shared.Interfaces;

public interface ICreate<TKey, in TItem>
{
    Task<TKey> Create(TItem item);
}