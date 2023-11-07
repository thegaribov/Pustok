using Pustok.Database;
using Pustok.Services.Abstract;
using System;
using System.Linq;

namespace Pustok.Services;

public class EmployeeServiceImp : IEmployeeService
{
    private readonly PustokDbContext _dbContext;

    public EmployeeServiceImp(PustokDbContext pustokDbContext)
    {
        _dbContext = pustokDbContext;
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

        } while (DoesCodeExist(code));

        return code;
    }

    private bool DoesCodeExist(string code)
    {
        return _dbContext.Employees.Any(e => e.Code == code);
    }
}
