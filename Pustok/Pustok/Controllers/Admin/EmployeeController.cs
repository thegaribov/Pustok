using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using Pustok.Database.DomainModels;
using Pustok.Database.Repositories;
using Pustok.Services;
using Pustok.ViewModels;
using Pustok.ViewModels.Employee;
using Pustok.ViewModels.Product;

namespace Pustok.Controllers.Admin;


[Route("admin/employees")]
public class EmployeeController : Controller
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly DepartmentRepository _departmentRepository;
    private readonly ILogger<ProductController> _logger;
    private readonly EmployeeService _employeeService;

    public EmployeeController()
    {
        _employeeRepository = new EmployeeRepository();
        _departmentRepository = new DepartmentRepository();
        _employeeService = new EmployeeService();

        var factory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        _logger = factory.CreateLogger<ProductController>();
    }


    #region Products

    [HttpGet] //admin/employees
    public IActionResult Employees()
    {
        return View("Views/Admin/Employee/Employees.cshtml", _employeeRepository.GetAllNotDeleted());
    }

    #endregion

    #region Add

    [HttpGet("add")]
    public IActionResult Add()
    {
        var model = new EmployeeViewModel
        {
            Departments = _departmentRepository.GetAll()
        };

        return View("Views/Admin/Employee/EmployeeAdd.cshtml", model);
    }

    [HttpPost("add")]
    public IActionResult Add(EmployeeViewModel model)
    {
        if (!ModelState.IsValid)
            return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");

        var deparment = _departmentRepository.GetById(model.DepartmentId);
        if (deparment == null)
        {
            ModelState.AddModelError("DeparmentId", "Deparment doesn't exist");

            return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");
        }

        var employee = new Employee
        {
            Name = model.Name,
            Code = _employeeService.GenerateAndGetCode(),
            DepartmentId = deparment.Id,
            Email = model.Email,
            FatherName = model.FatherName,
            Pin = model.Pin,
            Surname = model.Surname,
        };

        try
        {
            _employeeRepository.Insert(employee);
        }
        catch (PostgresException e)
        {
            _logger.LogError(e, "Postgresql Exception");

            throw e;
        }

        return RedirectToAction("Employees");
    }

    private IActionResult PrepareValidationView(string viewName)
    {
        var deparments = _departmentRepository.GetAll();

        var responseViewModel = new EmployeeViewModel
        {
            Departments = deparments
        };

        return View(viewName, responseViewModel);
    }

    #endregion

    #region Delete

    [HttpGet("delete")]
    public IActionResult Delete(string code)
    {
        Employee employee = _employeeRepository.GetByCode(code);
        if (employee == null)
        {
            return NotFound();
        }

        employee.IsDeleted = true;

        _employeeRepository.Update(employee);

        return RedirectToAction("Employees");
    }

    #endregion
}
