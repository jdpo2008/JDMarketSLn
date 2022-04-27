using JdMarketSln.Application.Features.Products.Commands.CreateProduct;
using JdMarketSln.Application.Features.Products.Commands.DeleteProduct;
using JdMarketSln.Application.Features.Products.Commands.UpdateProduct;
using JdMarketSln.Application.Features.Products.Queries.GetAllProducts;
using JdMarketSln.Application.Features.Products.Queries.GetAllProductsPaginated;
using JdMarketSln.Application.Features.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JDMarketSLn.Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsPaginatedRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllProductsPaginatedQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery() { Id = id }));
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Post), response);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok(response);
        }

    }
}
