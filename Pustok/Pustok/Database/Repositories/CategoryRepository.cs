using Npgsql;
using Pustok.Database.DomainModels;
using System.Collections.Generic;
using System;
using Pustok.Database.Abstracts;

namespace Pustok.Database.Repositories;

public class CategoryRepository : BaseRepository<Category>, IDisposable
{
    private readonly NpgsqlConnection _npgsqlConnection;

    public CategoryRepository()
    {
        _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.CONNECTION_STRING);
        _npgsqlConnection.Open();
    }

    public override List<Category> GetAll()
    {
        var selectQuery = "SELECT * FROM categories ORDER BY name";

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        List<Category> categories = new List<Category>();

        while (dataReader.Read())
        {
            Category product = new Category
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
            };

            categories.Add(product);
        }

        return categories;
    }

    public override Category GetById(int id)
    {
        using NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM categories WHERE id={id}", _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        Category category = null;

        while (dataReader.Read())
        {
            category = new Category
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
            };
        }

        return category;
    }

    public void Dispose()
    {
        _npgsqlConnection.Dispose();
    }

    public override void Insert(Category data)
    {
        throw new NotImplementedException();
    }

    public override void Update(Category data)
    {
        throw new NotImplementedException();
    }

    public override void RemoveById(int id)
    {
        throw new NotImplementedException();
    }
}
