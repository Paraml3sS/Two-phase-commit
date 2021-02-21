using System.Data;
using HotelCompany.Application;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HotelCompany.Infrastructure
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly string connectionString;
        
        public DbConnectionFactory(IConfiguration configuration)
        {
            // connectionString = "host=postgresql-hotel;port=5432;database=postgresdb;username=user;password=closeyoureyes;";
            connectionString = configuration["ConnectionString"];
        }
        
        public IDbConnection Connection => new NpgsqlConnection(connectionString);
    }

}