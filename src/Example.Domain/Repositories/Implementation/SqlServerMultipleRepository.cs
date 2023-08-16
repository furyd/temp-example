using Dapper;
using Example.Domain.Repositories.Interfaces;
using Example.Domain.Repositories.Models;
using Example.Domain.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Example.Domain.Repositories.Implementation
{
    public class SqlServerMultipleRepository : IMultipleRepository
    {
        private readonly SqlServerSettings _settings;

        public SqlServerMultipleRepository(IOptions<SqlServerSettings> settings)
        {
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<Tuple<IEnumerable<EmployerModel>, IEnumerable<FieldTypeModel>>> Multiple()
        {
            const string storedProcedure = "ListEmployersAndFieldTypes";

            await using var connection = new SqlConnection(_settings.ConnectionString);
            await connection.OpenAsync();
            
            using var multiple = await connection.QueryMultipleAsync(storedProcedure, commandType: System.Data.CommandType.StoredProcedure);

            return new Tuple<IEnumerable<EmployerModel>, IEnumerable<FieldTypeModel>>(await multiple.ReadAsync<EmployerModel>(), await multiple.ReadAsync<FieldTypeModel>());
        }
    }
}
