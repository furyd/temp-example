namespace Example.Domain.Shared.Interfaces;

public interface IRetrieve<in TKey, TItem>
{
    Task<TItem> Retrieve(TKey key);
}