using Npgsql;
using Pustok.Contracts;
using Pustok.Database.DomainModels;
using Pustok.Database.Repositories;
using System;
using System.Collections.Generic;

namespace Pustok.Services;


public partial class DatabaseService : IDisposable
{
    private delegate void QueryExecuter();

    private readonly NpgsqlConnection _npgsqlConnection;
    private readonly MigrationHistoryRepository _migrationHistoryRepository;


    private readonly List<QueryExecuter> _queryExecuters = new List<QueryExecuter>();
    //Investigate

    public DatabaseService()
    {
        _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.CONNECTION_STRING);
        _npgsqlConnection.Open();

        RegisterQueryExecuters();

        _migrationHistoryRepository = new MigrationHistoryRepository();
    }

    public void InitializeTables()
    {
        CreateMigrationHistoriesTable();

        UpdateDatabase();
    }
    private void UpdateDatabase()
    {
        foreach (var queryExecuter in _queryExecuters)
        {
            var migrationHistory = _migrationHistoryRepository.GetByName(queryExecuter.Method.Name);
            if (migrationHistory != null)
            {
                continue;
            }

            queryExecuter();

            _migrationHistoryRepository.Insert(new MigrationHistory
            {
                Name = queryExecuter.Method.Name,
                Description = queryExecuter.Method.Name,
            });
        }
    }

    public void Dispose()
    {
        _npgsqlConnection.Dispose();
        _migrationHistoryRepository.Dispose();
    }
}

public partial class DatabaseService
{
    private void RegisterQueryExecuters()
    {
        _queryExecuters.Add(CreateDepartmentsTable);
        _queryExecuters.Add(CreateEmployeesTable);
        _queryExecuters.Add(AlterDepartmentsTableAddDescriptionColumn);
    }

    private void CreateMigrationHistoriesTable()
    {
        var query = $"CREATE TABLE IF NOT EXISTS \"MigrationHistories\"(\r\n\t\"id\" SERIAL PRIMARY KEY,\r\n\t\"name\" TEXT NOT NULL,\r\n\t\"description\" TEXT NOT NULL\r\n)";

        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }
    private void CreateDepartmentsTable()
    {
        var query = $"CREATE TABLE IF NOT EXISTS \"Departments\" (\r\n\t\"id\" SERIAL PRIMARY KEY,\r\n\t\"name\" TEXT NOT NULL\r\n)";

        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }
    private void CreateEmployeesTable()
    {
        var query2 = $"CREATE TABLE IF NOT EXISTS \"Employees\"(\r\n\t\"id\" SERIAL PRIMARY KEY,\r\n\t\"code\" TEXT UNIQUE NOT NULL,\r\n\t\"name\" TEXT NOT NULL,\r\n\t\"surname\" TEXT NOT NULL,\r\n\t\"fathername\" TEXT NOT NULL,\r\n\t\"pin\" TEXT UNIQUE NOT NULL,\r\n\t\"email\" TEXT UNIQUE NOT NULL,\r\n\t\"IsDeleted\" BOOL DEFAULT FALSE,\r\n\t\"departmentId\" INT REFERENCES \"Departments\"(\"id\")\r\n)";

        using NpgsqlCommand updateCommand2 = new NpgsqlCommand(query2, _npgsqlConnection);
        updateCommand2.ExecuteNonQuery();
    }
    private void AlterDepartmentsTableAddDescriptionColumn()
    {
        var query2 = $"ALTER TABLE \"Departments\"\r\nADD COLUMN \"Description\" TEXT";

        using NpgsqlCommand updateCommand2 = new NpgsqlCommand(query2, _npgsqlConnection);
        updateCommand2.ExecuteNonQuery();
    }

}
