﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Services;
using Pustok.ViewModels.Employee;
using System.Linq;

namespace Pustok.Controllers.Admin;


[Route("admin/employees")]
public class EmployeeController : Controller
{
    private readonly PustokDbContext _dbContext;
    private readonly ILogger<ProductController> _logger;
    private readonly EmployeeService _employeeService;

    public EmployeeController()
    {
        _dbContext = new PustokDbContext();
        _employeeService = new EmployeeService();

        var factory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        _logger = factory.CreateLogger<ProductController>();
    }


    #region Employees

    [HttpGet] //admin/employees
    public IActionResult Employees()
    {
        //decorator pattern
        var employeesQuery = _dbContext.Employees
            .Where(e => !e.IsDeleted)
            .OrderBy(e => e.Name)
            .AsQueryable();

        var sql = employeesQuery.ToQueryString();


        return View("Views/Admin/Employee/Employees.cshtml", employeesQuery.ToList());
    }

    #endregion

    #region Add

    [HttpGet("add")]
    public IActionResult Add()
    {
        var model = new EmployeeViewModel
        {
            Departments = _dbContext.Departments.ToList()
        };

        return View("Views/Admin/Employee/EmployeeAdd.cshtml", model);
    }

    [HttpPost("add")]
    public IActionResult Add(EmployeeViewModel model)
    {
        if (!ModelState.IsValid)
            return PrepareValidationView("Views/Admin/Product/ProductAdd.cshtml");

        var query = _dbContext.Departments
                .Where(d => d.Id == model.DepartmentId)
                .AsQueryable()
                .ToQueryString();


        var deparment = _dbContext.Departments.FirstOrDefault(d => d.Id == model.DepartmentId);
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
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges(); //unit of work pattern => transaction
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
        var deparments = _dbContext.Departments.ToList();

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
        Employee employee = _dbContext.Employees.FirstOrDefault(e => e.Code == code);
        if (employee == null)
        {
            return NotFound();
        }

        employee.IsDeleted = true;

        _dbContext.Employees.Update(employee);
        _dbContext.SaveChanges();

        return RedirectToAction("Employees");
    }

    #endregion

    protected override void Dispose(bool disposing)
    {
        _dbContext.Dispose();
        _employeeService.Dispose();

        base.Dispose(disposing);
    }
}