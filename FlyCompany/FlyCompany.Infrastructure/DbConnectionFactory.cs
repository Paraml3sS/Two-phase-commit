using System.Data;
using FlyCompany.Application;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace FlyCompany.Infrastructure
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly string connectionString;
        
        public DbConnectionFactory(IConfiguration configuration)
        {
            // connectionString = "host=postgresql-fly;port=5432;database=postgresdb;username=user;password=closeyoureyes;";
            connectionString = configuration["ConnectionString"];
        }
        
        public IDbConnection Connection => new NpgsqlConnection(connectionString);
    }

}