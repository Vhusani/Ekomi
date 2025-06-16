using MySqlConnector;
using System.Data;

namespace WebAPI.Data
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnectionFactory(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public IDbConnection GetThemLive()
        {
            var connectionString = _configuration.GetConnectionString("Live");
            return new MySqlConnection(connectionString);
        }

    }
}
