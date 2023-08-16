using Example.Domain.Repositories.Implementation;
using Example.Domain.Repositories.Models;
using Example.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Example.TestDataLoader.FieldTypes;

public static class FieldTypeData
{
    private static readonly IEnumerable<string> Types = new[] { "text", "textarea", "select", "radiolist", "checkboxlist" };

    public static async Task<ICollection<KeyValuePair<string, Guid>>> Load(IOptions<SqlServerSettings> settings)
    {
        var ids = new List<KeyValuePair<string, Guid>>(Types.Count());

        var repository = new SqlServerFieldTypeRepository(settings);

        foreach (var type in Types)
        {
            ids.Add(new KeyValuePair<string,Guid>(type, await repository.Create(new FieldTypeModel{ Name = type })));
        }

        return ids;
    }
}