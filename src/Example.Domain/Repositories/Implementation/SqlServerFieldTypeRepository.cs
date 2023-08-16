using Example.Domain.Repositories.Interfaces;
using Example.Domain.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using Example.Domain.Repositories.Models;

namespace Example.Domain.Repositories.Implementation;

public class SqlServerFieldTypeRepository : IFieldTypeRepository
{
    private readonly SqlServerSettings _settings;

    public SqlServerFieldTypeRepository(IOptions<SqlServerSettings> settings)
    {
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task<Guid> Create(FieldTypeModel item)
    {
        const string storedProcedure = "CreateFieldType";

        await using var connection = new SqlConnection(_settings.ConnectionString);
        await connection.OpenAsync();

        return await connection.ExecuteScalarAsync<Guid>(storedProcedure, item, commandType: CommandType.StoredProcedure);
    }
}