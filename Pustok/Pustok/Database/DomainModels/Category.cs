﻿namespace Pustok.Database.DomainModels;

public class Category
{
    public Category()
    {
    }

    public Category(int ıd, string name)
    {
        Id = ıd;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}