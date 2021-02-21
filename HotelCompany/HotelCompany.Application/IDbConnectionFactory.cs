using System.Data;

namespace HotelCompany.Application
{
    public interface IDbConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}