using System.Data;

namespace AccountCompany.Application
{
    public interface IDbConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}