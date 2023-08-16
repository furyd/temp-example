using Example.Domain.Settings;
using Example.TestDataLoader.Employers;
using Example.TestDataLoader.Fields;
using Example.TestDataLoader.FieldTypes;
using Example.TestDataLoader.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var configurationBuilder = new ConfigurationBuilder();

configurationBuilder.AddUserSecrets<Program>();

var configuration = configurationBuilder.Build();

var settings = new SqlServerSettings();

configuration.GetSection(nameof(SqlServerSettings)).Bind(settings);

var options = Options.Create(settings);

var employers = await EmployerData.Load(10, options);
var fieldTypes = await FieldTypeData.Load(options);

foreach (var employer in employers)
{
    foreach (var fieldType in fieldTypes)
    {
        var fields = await FieldData.LoadData(employer, fieldType.Value, 10, options);

        if (fieldType.Key.Equals("text", StringComparison.OrdinalIgnoreCase) ||
            fieldType.Key.Equals("textarea", StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }

        foreach (var field in fields)
        {
            await OptionsData.LoadData(field, 10, options);
        }
    }
}