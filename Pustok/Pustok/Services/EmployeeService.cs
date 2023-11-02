using Pustok.Database;
using System;
using System.Linq;

namespace Pustok.Services;

public class EmployeeService : IDisposable
{
    private readonly PustokDbContext _dbContext;

    public EmployeeService()
    {
        _dbContext = new PustokDbContext();
    }

    public string GenerateAndGetCode()
    {
        var random = new Random();
        string numberPart;
        string code;

        do
        {
            numberPart = random.Next(1, 100000).ToString();
            code = $"E{numberPart.PadLeft(5, '0')}";

        } while (_dbContext.Employees.Any(e => e.Code == code));

        return code;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
