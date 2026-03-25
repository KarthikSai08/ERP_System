using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Application.Features.Products.Commands.DeleteProduct;
using ERP_System.Application.Features.Products.Commands.UpdateProduct;
using ERP_System.Application.Features.Products.Queries.GetAllProducts;
using ERP_System.Application.Features.Products.Queries.GetProductById;
using ERP_System.Application.Features.Products.Queries.SearchProducts;
using ERP_System.Application.Interfaces;
using ERP_System.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICacheService _cache;

        public ProductsController(IMediator mediator, ICacheService cache)
        {
            _mediator = mediator;
            _cache = cache;
        }


        [HttpGet("all-products")]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllProductsQuery query, CancellationToken ct)
        {
            var version = await _cache.GetAsync<int>("products:version", ct);
            if (version == 0) version = 1;

            query = query with { Version = version };

            return Ok(await _mediator.Send(query)); 
        
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] int? categoryId, [FromQuery] decimal? maxPrice)
            => Ok(await _mediator.Send(new SearchProductsQuery(name, categoryId, maxPrice)));

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken )
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return CreatedAtRoute("GetProductById", new { id = result.Data!.Id }, result);
        }

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
    }
}
