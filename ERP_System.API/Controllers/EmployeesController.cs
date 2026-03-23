using ERP_System.Application.Features.Employees.Commands.CreateEmployee;
using ERP_System.Application.Features.Employees.Queries.GetAllEmployees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator  _mediator;
    public EmployeesController(IMediator mediator) =>  _mediator = mediator;

     [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] string? department)
        => Ok(await  _mediator.Send(new GetAllEmployeesQuery(department)));

     [HttpPost]
        public async Task<IActionResult> Create( [FromBody] CreateEmployeeCommand command)
        {
            var result = await  _mediator.Send(command);
            return Created($"/api/v1/employee/{result.Data!.Id}", result);
        }
    }
}
