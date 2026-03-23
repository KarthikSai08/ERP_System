using ERP_System.Application.Features.Orders.Commands.CreateOrder;
using ERP_System.Application.Features.Orders.Commands.UpdateOrder;
using ERP_System.Application.Features.Orders.Queries.GetAllOrders;
using ERP_System.Application.Features.Orders.Queries.GetSalesReport;
using ERP_System.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator  _mediator;
        public OrdersController(IMediator mediator) =>   _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllOrdersQuery()));

        [HttpPost]
        public async Task<IActionResult> Create(  [FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Created($"/api/v1/order/{result.Data!.Id}", result);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id,   [FromBody] OrderStatus status)
            => Ok(await _mediator.Send(new UpdateOrderStatusCommand(id, status)));

        [HttpGet("reports/sales")]
        public async Task<IActionResult> SalesReport(
          [FromQuery] DateTime from,   [FromQuery] DateTime to)
            => Ok(await _mediator.Send(new GetSalesReportQuery(from, to)));
    
    }
}
