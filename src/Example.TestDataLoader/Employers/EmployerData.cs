using Bogus;
using Example.Domain.Repositories.Implementation;
using Example.Domain.Repositories.Models;
using Example.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Example.TestDataLoader.Employers;

public static class EmployerData
{
    public static async Task<ICollection<Guid>> Load(int number, IOptions<SqlServerSettings> settings)
    {
        var ids = new List<Guid>(number);

        var repository = new SqlServerEmployerRepository(settings);

        var generator = new Faker<EmployerModel>()
            .RuleFor(employer => employer.Name, faker => faker.Company.CompanyName());

        foreach (var employerModel in generator.Generate(number))
        {
            ids.Add(await repository.Create(employerModel));
        }

        return ids;
    }
}