namespace Example.Domain.Shared.Interfaces;

public interface IUpdate<in TKey, in TItem>
{
    Task Update(TKey key, TItem item);
}