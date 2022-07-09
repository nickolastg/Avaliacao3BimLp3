namespace Avaliacao3BimLp3.Database;
using Microsoft.Data.Sqlite;
using Dapper;

class DatabaseSetup
{
    private readonly DatabaseConfig _databaseConfig;
    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
        CreateStudentTable();
    }

    private void CreateStudentTable()
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Students(
                Registration varchar (100) not null primary key,
                Name varchar(100) not null,
                City varchar(100) not null,
                Former boolean not null
            );
        ");
    }
}