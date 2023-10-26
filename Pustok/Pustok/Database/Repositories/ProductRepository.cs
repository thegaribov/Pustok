using Npgsql;
using Pustok.Database.DomainModels;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;

namespace Pustok.Database.Repositories;

public class ProductRepository : IDisposable
{
    private readonly NpgsqlConnection _npgsqlConnection;

    public ProductRepository()
    {
        _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.CONNECTION_STRING);
        _npgsqlConnection.Open();
    }

    public List<Product> GetAll()
    {
        var selectQuery = "SELECT * FROM products ORDER BY name";

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        List<Product> products = new List<Product>();

        while (dataReader.Read())
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
                CategoryId = dataReader["categoryid"] as int?,
                Price = Convert.ToInt32(dataReader["price"]),
                Rating = Convert.ToInt32(dataReader["rating"]),
            };

            products.Add(product);
        }

        return products;
    }

    public List<Product> GetAllWithCategories()
    {
        var selectQuery = "SELECT \r\n\tp.\"id\" productId,\r\n\tp.\"name\" productName,\r\n\tp.\"price\" productPrice,\r\n\tp.\"rating\" productRating,\r\n\tc.\"id\" categoryId,\r\n\tc.\"name\" categoryName\r\nFROM products p\r\nLEFT JOIN categories c ON p.\"categoryid\"=c.\"id\"\r\nORDER BY p.name";

        using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        List<Product> products = new List<Product>();

        while (dataReader.Read())
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dataReader["productId"]),
                Name = Convert.ToString(dataReader["productName"]),
                CategoryId = dataReader["categoryId"] as int?,
                Price = Convert.ToInt32(dataReader["productPrice"]),
                Rating = Convert.ToInt32(dataReader["productRating"]),
                Category = dataReader["categoryId"] is int
                    ? new Category(Convert.ToInt32(dataReader["categoryId"]), Convert.ToString(dataReader["categoryName"]))
                    : null
            };

            products.Add(product);
        }

        return products;
    }

    public Product GetById(int id)
    {
        using NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM products WHERE id={id}", _npgsqlConnection);
        using NpgsqlDataReader dataReader = command.ExecuteReader();

        Product product = null;

        while (dataReader.Read())
        {
            product = new Product
            {
                Id = Convert.ToInt32(dataReader["id"]),
                Name = Convert.ToString(dataReader["name"]),
                CategoryId = dataReader["categoryid"] as int?,
                Price = Convert.ToInt32(dataReader["price"]),
                Rating = Convert.ToInt32(dataReader["rating"]),
            };
        }

        return product;
    }


    public void Insert(Product product)
    {
        string updateQuery =
            "INSERT INTO products(name, price, rating, categoryid)" +
            $"VALUES ('{product.Name}', {product.Price}, {product.Rating}, {product.CategoryId})";

        using NpgsqlCommand command = new NpgsqlCommand(updateQuery, _npgsqlConnection);
        command.ExecuteNonQuery();
    }

    public void Update(Product product)
    {
        var query =
                $"UPDATE products " +
                $"SET name='{product.Name}', price={product.Price}, rating={product.Rating}, categoryid={product.CategoryId} " +
                $"WHERE id={product.Id}";

        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }

    public void RemoveById(int id)
    {
        var query = $"DELETE FROM products WHERE id={id}";

        using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
        updateCommand.ExecuteNonQuery();
    }

    public void Dispose()
    {
        _npgsqlConnection.Dispose();
    }
}
