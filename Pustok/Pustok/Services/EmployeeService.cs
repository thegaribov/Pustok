using Pustok.Database.Repositories;
using System;

namespace Pustok.Services;

public class EmployeeService : IDisposable
{
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeService()
    {
        _employeeRepository = new EmployeeRepository();
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

        } while (_employeeRepository.GetByCode(code) != null);

        return code;
    }

    public void Dispose()
    {
        _employeeRepository.Dispose();
    }
}
