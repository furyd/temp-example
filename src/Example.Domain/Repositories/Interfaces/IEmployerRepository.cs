using Example.Domain.Repositories.Models;
using Example.Domain.Shared.Interfaces;

namespace Example.Domain.Repositories.Interfaces;

public interface IEmployerRepository : ICreate<Guid, EmployerModel>
{
}