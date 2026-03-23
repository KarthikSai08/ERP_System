using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Application.Features.Products.Commands.DeleteProduct;
using ERP_System.Application.Features.Products.Commands.UpdateProduct;
using ERP_System.Application.Features.Products.Queries.GetAllProducts;
using ERP_System.Application.Features.Products.Queries.SearchProducts;
using ERP_System.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(cmd), new { id = result.Data!.Id }, result);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllAsync() => Ok(await _mediator.Send(new GetAllProductsQuery()));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand cmd)
        {
            var result = await _mediator.Send(cmd with { ProductId = id });
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] int? categoryId, [FromQuery] decimal? maxPrice)
            => Ok(await _mediator.Send(new SearchProductsQuery(name, categoryId, maxPrice)));
    }
}
