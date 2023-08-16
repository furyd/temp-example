using Example.Domain.Repositories.Models;

namespace Example.Domain.Repositories.Interfaces
{
    public interface IMultipleRepository
    {
        Task<Tuple<IEnumerable<EmployerModel>, IEnumerable<FieldTypeModel>>> Multiple();
    }
}
