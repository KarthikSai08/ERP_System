using ERP_System.Application.Features.Inventory.Command.AddStock;
using ERP_System.Application.Features.Inventory.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InventoriesController(IMediator mediator) => _mediator = mediator;

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockAlerts()
        => Ok(await  _mediator.Send(new GetLowStockAlertQuery()));

        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock([FromBody] AddStockCommand command)
        => Ok(await  _mediator.Send(command));
    }


}
}
