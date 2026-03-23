using ERP_System.Application.Features.Suppliers.Commands.CreateSupplier;
using ERP_System.Application.Features.Suppliers.Queries.GetAllSppliers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SuppliersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
           => Ok(await _mediator.Send(new GetAllSuppliersQuery()));

        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] CreateSupplierCommand command)
        {
            var result = await _mediator.Send(command);
            return Created($"/api/v1/supplier/{result.Data!.Id}", result);
        }
    } 
}
