using Bogus;
using Example.Domain.Repositories.Implementation;
using Example.Domain.Repositories.Models;
using Example.Domain.Settings;
using Example.Domain.Shared.Models;
using Microsoft.Extensions.Options;

namespace Example.TestDataLoader.Fields;

public static class FieldData
{
    public static async Task<ICollection<Guid>> LoadData(Guid employer, Guid fieldType, int number, IOptions<SqlServerSettings> settings)
    {
        var ids = new List<Guid>(number);

        var repository = new SqlServerFieldRepository(settings);

        var generator = new Faker<FieldModel>()
                .RuleFor(option => option.Text, faker => faker.Lorem.Word())
                .RuleFor(option => option.Employer, employer)
                .RuleFor(option => option.FieldType, fieldType)
            ;

        foreach (var fieldModel in generator.Generate(number))
        {
            ids.Add(await repository.Create(fieldModel));
        }

        return ids;
    }

    public static async Task Test(IOptions<SqlServerSettings> settings, Guid employer)
    {
        var repository = new SqlServerFieldRepository(settings);

        await repository.List(new PagedEmployerFilter(1, 10, employer));
    }
}