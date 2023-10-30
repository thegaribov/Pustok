using Npgsql;
using Pustok.Database.DomainModels;
using System.Collections.Generic;
using System;

namespace Pustok.Database.Abstracts;

public abstract class BaseRepository<T>
    where T : IEntity
{
    public abstract List<T> GetAll();
    public abstract T GetById(int id);
    public abstract void Insert(T data);
    public abstract void Update(T data);
    public abstract void RemoveById(int id);
}
