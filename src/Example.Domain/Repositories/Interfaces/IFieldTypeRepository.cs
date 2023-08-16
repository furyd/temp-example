using Example.Domain.Repositories.Models;
using Example.Domain.Shared.Interfaces;

namespace Example.Domain.Repositories.Interfaces;

public interface IFieldTypeRepository : ICreate<Guid, FieldTypeModel>
{
}