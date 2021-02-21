using System;
using System.Threading.Tasks;
using Dapper;
using FlyCompany.Application;

namespace FlyCompany.Application
{
    public class DistributedTransactionHandler
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        
        public DistributedTransactionHandler(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }
        
        private async Task<int> Handle(string sql, object param = null)
        {
            using var connection = dbConnectionFactory.Connection;
            connection.Open();
            var result = await connection.ExecuteAsync(sql, param);
            connection.Close();
            return result;
        }
        
        public async Task<int> HandlePrepare(Guid transactionId, string sql, object param)
            => await this.Handle("Begin;" + sql + $"PREPARE TRANSACTION '{transactionId}';", param);
        
        public async Task<int> HandleRollback(Guid transactionId)
            => await this.Handle($"ROLLBACK PREPARED '{transactionId}';");
        
        public async Task<int> HandleCommit(Guid transactionId)
            => await this.Handle($"COMMIT PREPARED '{transactionId}';");
    }
}