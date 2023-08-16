using Example.Domain.Queries.Models;
using Example.Domain.Shared.Interfaces;
using Example.Domain.Shared.Models;

namespace Example.Domain.Queries.Interfaces;

public interface IFieldQueries : IRetrieveFiltered<FieldModel, PagedEmployerFilter>
{
}