using Bogus;
using Example.Domain.Repositories.Implementation;
using Example.Domain.Repositories.Models;
using Example.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Example.TestDataLoader.Options;

public static class OptionsData
{
    public static async Task LoadData(Guid field, int number, IOptions<SqlServerSettings> settings)
    {
        var repository = new SqlServerOptionRepository(settings);

        var generator = new Faker<OptionModel>()
            .RuleFor(option => option.Text, faker => faker.Lorem.Word())
            .RuleFor(option => option.Value, (_, model) => model.Text)
            .RuleFor(option => option.Field, field)
            ;

        foreach (var optionModel in generator.Generate(number))
        {
            await repository.Create(optionModel);
        }
    }
}