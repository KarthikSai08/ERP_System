using ERP_System.Application.Features.Warehouses.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WarehousesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("add-warehouse")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWarehouseCommand cmd, CancellationToken ct)
        {
            var result = await _mediator.Send(cmd, ct);
            return Ok(result);
        }
    }
}
