namespace Example.Domain.Shared.Interfaces;

public interface IDelete<in TKey>
{
    Task Delete(TKey key);
}