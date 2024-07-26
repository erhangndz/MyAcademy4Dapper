using System.Data.SqlClient;
using System.Data;

namespace MyAcademy4Dapper.Context
{
    public class BigDataContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public BigDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BigDataConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
