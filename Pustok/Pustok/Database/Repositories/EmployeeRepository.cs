using Npgsql;
using Pustok.Database.Abstracts;
using Pustok.Database.DomainModels;
using System;
using System.Collections.Generic;

namespace Pustok.Database.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IDisposable
{
    private readonly NpgsqlConnection _npgsqlConnection;

    public EmployeeRepository()
    {
        _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.CONNECTION_STRING);
        _npgsqlConnection.Open();
    }

    public override List<Employee> GetAll()
    {
        var selectQuery = "SELECT * FROM \"Employees\" ORDER BY name";

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        List<Employee> employees = new List<Employee>();

        while (dataReader.Read())
        {
            Employee employee = new Employee
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Code = Convert.ToString(dataReader["code"]),
                Name = Convert.ToString(dataReader["name"]),
                Surname = Convert.ToString(dataReader["surname"]),
                FatherName = Convert.ToString(dataReader["fathername"]),
                Pin = Convert.ToString(dataReader["pin"]),
                Email = Convert.ToString(dataReader["email"]),
                DepartmentId = Convert.ToInt32(dataReader["departmentId"]),
            };

            employees.Add(employee);
        }

        return employees;
    }

    public override Employee GetById(int id)
    {
        using NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM employees WHERE id={id}", _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        Employee employee = null;

        while (dataReader.Read())
        {
            employee = new Employee
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Code = Convert.ToString(dataReader["code"]),
                Name = Convert.ToString(dataReader["name"]),
                Surname = Convert.ToString(dataReader["surname"]),
                FatherName = Convert.ToString(dataReader["fathername"]),
                Pin = Convert.ToString(dataReader["pin"]),
                Email = Convert.ToString(dataReader["email"]),
                DepartmentId = Convert.ToInt32(dataReader["deparmentId"]),
            };
        }

        return employee;
    }

    public override void Insert(Employee data)
    {
        string updateQuery =
            "INSERT INTO Employees(code, name, surname, fatherName, pin, email, departmentId)" +
            $"VALUES ('{data.Code}', '{data.Name}', '{data.Surname}', '{data.FatherName}', '{data.Pin}', '{data.Email}', {data.DepartmentId})";

        using NpgsqlCommand command = new NpgsqlCommand(updateQuery, _npgsqlConnection);
        command.ExecuteNonQuery();
    }

    public override void Update(Employee data)
    {
        var query =
            $"UPDATE Employees " +
            $"SET " +
            $"  code='{data.Code}'," +
            $"  name='{data.Name}'," +
            $"  surname='{data.Surname}'," +
            $"  fatherName='{data.FatherName}'," +
            $"  pin='{data.Pin}'," +
            $"  email='{data.Email}' " +
            $"WHERE id={data.Id}";

        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }

    public override void RemoveById(int id)
    {
        var query = $"DELETE FROM Employees WHERE id={id}";

        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }

    public void Dispose()
    {
        _npgsqlConnection.Dispose();
    }
}
