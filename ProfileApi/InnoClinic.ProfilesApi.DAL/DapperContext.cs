using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace InnoClinic.Prof.DataAccess;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("InnoClinicProfile");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}