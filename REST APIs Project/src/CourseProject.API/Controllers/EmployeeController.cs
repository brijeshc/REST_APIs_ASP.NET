﻿using CourseProject.Common.Dtos.Employee;
using CourseProject.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controllers;
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase 
{
    private IEmployeeService EmployeeService { get; }
    public EmployeeController(IEmployeeService employeeService)
    {
        EmployeeService = employeeService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateEmployee(EmployeeCreate employeeCreate)
    {
        var id = await EmployeeService.CreateEmployeeAsync(employeeCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateEmployee(EmployeeUpdate employeeUpdate)
    {
        await EmployeeService.UpdateEmployeeAsync(employeeUpdate);
        return Ok(employeeUpdate);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteEmployee(EmployeeDelete employeeDelete)
    {
        await EmployeeService.DeleteEmployeeAsync(employeeDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await EmployeeService.GetEmployeeAsync(id);
        return Ok(employee);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilter employeeFilter)
    {
        var employees = await EmployeeService.GetEmployeesAsync(employeeFilter);
        return Ok(employees);
    }
    
}
