using Npgsql;
using Pustok.Contracts;
using Pustok.Database.DomainModels;
using System;

namespace Pustok.Database.Repositories;

public class MigrationHistoryRepository : IDisposable
{
    private readonly NpgsqlConnection _npgsqlConnection;

    public MigrationHistoryRepository()
    {
        _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.CONNECTION_STRING);
        _npgsqlConnection.Open();
    }

    public MigrationHistory GetByName(string name)
    {
        using NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM \"MigrationHistories\" WHERE name='{name}'", _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        MigrationHistory migrationHistory = null;

        while (dataReader.Read())
        {
            migrationHistory = new MigrationHistory
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
                Description = Convert.ToString(dataReader["description"])
            };
        }

        return migrationHistory;
    }

    public void Insert(MigrationHistory data)
    {
        string updateQuery =
            "INSERT INTO \"MigrationHistories\"(name, description)" +
            $"VALUES ('{data.Name}', '{data.Description}')";

        using NpgsqlCommand command = new NpgsqlCommand(updateQuery, _npgsqlConnection);
        command.ExecuteNonQuery();
    }

    public void Dispose()
    {
        _npgsqlConnection.Dispose();
    }
}
