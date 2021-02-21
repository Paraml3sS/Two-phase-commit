using System.Data;

namespace FlyCompany.Application
{
    public interface IDbConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}