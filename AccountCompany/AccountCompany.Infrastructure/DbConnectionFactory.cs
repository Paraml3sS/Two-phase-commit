using System.Data;
using AccountCompany.Application;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AccountCompany.Infrastructure
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly string connectionString;
        
        public DbConnectionFactory(IConfiguration configuration)
        {
            // connectionString = "host=postgresql-account;port=5432;database=postgresdb;username=user;password=closeyoureyes;";
            connectionString = configuration["ConnectionString"];
        }
        
        public IDbConnection Connection => new NpgsqlConnection(connectionString);
    }
}