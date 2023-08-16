namespace Example.Domain.Shared.Interfaces;

public interface IRetrieveFiltered<TItem, in TFilter>
{
    Task<IPager<TItem>> List(TFilter filter);
}