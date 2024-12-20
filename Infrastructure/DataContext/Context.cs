using System.Data.Common;
using Npgsql;

namespace Infrastructure.DataContext;

public class Context:IContext
{
    private const string ConnectionString = "Server=localhost; Port = 5432; Database = test; User Id = postgres; Password = 1234;";


    public DbConnection Connection()
    {
        return new NpgsqlConnection(ConnectionString);
    }
}

public interface IContext
{
    public DbConnection Connection();
}