using JdMarketSln.Application.Features.Products.Commands.CreateProduct;
using JdMarketSln.Application.Features.Products.Commands.DeleteProduct;
using JdMarketSln.Application.Features.Products.Commands.UpdateProduct;
using JdMarketSln.Application.Features.Products.Queries.GetAllProducts;
using JdMarketSln.Application.Features.Products.Queries.GetAllProductsPaginated;
using JdMarketSln.Application.Features.Products.Queries.GetProductById;
using JdMarketSln.Application.Features.Products.Queries.GetProductsByIdSuplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JDMarketSLn.Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductsPaginatedRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllProductsPaginatedQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery() { Id = id }));
        }

        [HttpGet("{suplierId}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProductsBySuplierId(Guid suplierId)
        {
            return Ok(await Mediator.Send(new GetProductsByIdSuplierQuery() { SuplierId = suplierId }));
        }

        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Create), response);
        }

        [HttpPut]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok(response);
        }

    }
}
