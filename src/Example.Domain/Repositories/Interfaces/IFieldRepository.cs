using Example.Domain.Repositories.Models;
using Example.Domain.Shared.Interfaces;
using Example.Domain.Shared.Models;
using FieldModel = Example.Domain.Repositories.Models.FieldModel;

namespace Example.Domain.Repositories.Interfaces;

public interface IFieldRepository : 
    ICreate<Guid, FieldModel>, 
    IRetrieve<Guid, ReadOnlyFieldModel?>, 
    IUpdate<Guid, FieldModel>, 
    IDelete<Guid>,
    IRetrieveAll<ReadOnlyFieldModel>,
    IRetrieveFiltered<ReadOnlyFieldModel, PagedEmployerFilter>
{
}