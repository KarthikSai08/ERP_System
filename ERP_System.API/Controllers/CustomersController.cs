using ERP_System.Application.Features.Customers.Commands;
using ERP_System.Application.Features.Customers.Queries.GetAllCustomers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator  _mediator;
    public CustomersController(IMediator mediator) => _mediator = mediator;

     [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetAllCustomersQuery()));

     [HttpPost]
        public async Task<IActionResult> Create( [FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Created($"/api/v1/customer/{result.Data!.Id}", result);
        }
    }
}
}
