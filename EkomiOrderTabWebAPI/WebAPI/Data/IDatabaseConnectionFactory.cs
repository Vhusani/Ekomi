using System.Data;

namespace WebAPI.Data
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetThemLive();
    }
}
