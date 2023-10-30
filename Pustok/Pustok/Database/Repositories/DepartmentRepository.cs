using Npgsql;
using Pustok.Database.DomainModels;
using System.Collections.Generic;
using System;

namespace Pustok.Database.Repositories;

public class DepartmentRepository : IDisposable
{
    private readonly NpgsqlConnection _npgsqlConnection;

    public DepartmentRepository()
    {
        _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.CONNECTION_STRING);
        _npgsqlConnection.Open();
    }

    public List<Department> GetAll()
    {
        var selectQuery = "SELECT * FROM \"Departments\" ORDER BY name";

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        List<Department> departments = new List<Department>();

        while (dataReader.Read())
        {
            Department department = new Department
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"])
            };

            departments.Add(department);
        }

        return departments;
    }

    public Department GetById(int id)
    {
        using NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM \"Departments\" WHERE id={id}", _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        Department deparment = null;

        while (dataReader.Read())
        {
            deparment = new Department
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
            };
        }

        return deparment;
    }

    public void Dispose()
    {
        _npgsqlConnection.Dispose();
    }
}
