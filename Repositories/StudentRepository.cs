namespace Avaliacao3BimLp3.Repositories;

using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Dapper;

class StudentRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public StudentRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public Student Save(Student student)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Students VALUES(@Registration, @Name, @City, @Former);", student);

        return student;
    }

    public void Delete(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Students WHERE registration = @Registration;", new { Registration = registration });
    }

    public void MarkAsFormed(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Students SET Former = true WHERE registration = @Registration;", new { Registration = registration });
    }

    public List<Student> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var students = connection.Query<Student>("SELECT * FROM Students").ToList();

        return students;
    }

    public List<Student> GetAllFormed() 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var students = connection.Query<Student>("SELECT * FROM Students WHERE Former = true;").ToList();

        return students;
    }

    public List<Student> GetAllStudentByCity(string city)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.Query<Student>("SELECT * FROM Students WHERE City LIKE @City;", new { @City = city + "%" }).ToList();

        return result;
    }

    public List<Student> GetAllByCities(string[] cities)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.Query<Student>("SELECT * FROM Students WHERE city IN @Cities;", new { Cities = cities }).ToList();

        return result;
    }

    public List<CountStudentGroupByAttribute> CountByCities()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.Query<CountStudentGroupByAttribute>("SELECT City as AttributeName, COUNT (City) as StudentNumber FROM Students GROUP BY City;").ToList();

        return result;
    }

    public List<CountStudentGroupByAttribute> CountByFormed()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.Query<CountStudentGroupByAttribute>("SELECT Former as AttributeName, COUNT (Former) as StudentNumber FROM Students GROUP BY Former;").ToList();

        return result;
    }

    public bool ExistsById(string registration)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar("SELECT COUNT(registration) FROM Students WHERE registration = @Registration", new { Registration = registration });

        return Convert.ToBoolean(result);
    }
}