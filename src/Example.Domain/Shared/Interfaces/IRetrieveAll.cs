namespace Example.Domain.Shared.Interfaces;

public interface IRetrieveAll<TItem>
{
    Task<IReadOnlyCollection<TItem>> List();
}