using System.Data;
using Dapper;
using Example.Domain.Repositories.Implementation.DTOs;
using Example.Domain.Repositories.Interfaces;
using Example.Domain.Repositories.Models;
using Example.Domain.Settings;
using Example.Domain.Shared.Interfaces;
using Example.Domain.Shared.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Example.Domain.Repositories.Implementation;

public class SqlServerFieldRepository : IFieldRepository
{
    private readonly SqlServerSettings _settings;

    public SqlServerFieldRepository(IOptions<SqlServerSettings> settings)
    {
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task<Guid> Create(FieldModel item)
    {
        const string storedProcedure = "CreateField";

        await using var connection = new SqlConnection(_settings.ConnectionString);
        await connection.OpenAsync();

        return await connection.ExecuteScalarAsync<Guid>(storedProcedure, item, commandType: CommandType.StoredProcedure);
    }

    public async Task<ReadOnlyFieldModel?> Retrieve(Guid key)
    {
        const string storedProcedure = "RetrieveField";

        await using var connection = new SqlConnection(_settings.ConnectionString);
        await connection.OpenAsync();

        return await connection.QueryFirstAsync<ReadOnlyFieldModel>(storedProcedure, new { id = key }, commandType: CommandType.StoredProcedure);
    }

    public async Task Update(Guid key, FieldModel item)
    {
        const string storedProcedure = "UpdateField";

        await using var connection = new SqlConnection(_settings.ConnectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(storedProcedure, new { id = key, name = item.Text }, commandType: CommandType.StoredProcedure);
    }

    public async Task Delete(Guid key)
    {
        const string storedProcedure = "DeleteField";

        await using var connection = new SqlConnection(_settings.ConnectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(storedProcedure, new { id = key }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IReadOnlyCollection<ReadOnlyFieldModel>> List()
    {
        const string storedProcedure = "ListFields";

        await using var connection = new SqlConnection(_settings.ConnectionString);
        await connection.OpenAsync();

        return (await connection.QueryAsync<ReadOnlyFieldModel>(storedProcedure, commandType: CommandType.StoredProcedure)).ToList().AsReadOnly();
    }

    public async Task<IPager<ReadOnlyFieldModel>> List(PagedEmployerFilter filter)
    {
        const string storedProcedure = "ListFieldsFilteredByEmployer";

        await using var connection = new SqlConnection(_settings.ConnectionString);
        await connection.OpenAsync();

        var fields = (await connection.QueryAsync<FieldDTO>(storedProcedure, filter,
            commandType: CommandType.StoredProcedure)).ToList();

        return new Pager<ReadOnlyFieldModel>(filter.Page, filter.PageSize, Transform(fields));
    }

    private static ICollection<ReadOnlyFieldModel> Transform(IEnumerable<FieldDTO> fields)
    {
        var distinctFields = new Dictionary<Guid, ReadOnlyFieldModel>();

        foreach (var fieldDto in fields)
        {
            if (!distinctFields.TryGetValue(fieldDto.FieldId, out var distinctField))
            {
                distinctField = new ReadOnlyFieldModel(fieldDto.FieldId, fieldDto.FieldText, fieldDto.FieldType);
                distinctFields.Add(fieldDto.FieldId, distinctField);
            }

            if (string.IsNullOrWhiteSpace(fieldDto.Text))
            {
                continue;
            }

            if (distinctField.Values.ContainsKey(fieldDto.Text))
            {
                continue;
            }
                
            distinctField.Values.Add(fieldDto.Text, fieldDto.Value);
        }

        return distinctFields.Select(item => item.Value).ToList();
    }
}