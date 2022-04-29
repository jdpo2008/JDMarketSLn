using JdMarketSln.Application.Features.Categories.Commands.CreateCategory;
using JdMarketSln.Application.Features.Categories.Commands.DeleteCategory;
using JdMarketSln.Application.Features.Categories.Commands.UpdateCategory;
using JdMarketSln.Application.Features.Categories.Queries.GetAllCategoryPaginated;
using JdMarketSln.Application.Features.Categories.Queries.GetCategoryById;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JDMarketSLn.Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryPaginatedRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllCategoryPaginatedQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery() { Id = id }));
        }

        [HttpPost]
        //[Authorize]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return CreatedAtAction(nameof(Create), response);
        }

        [HttpPut]
        //[Authorize]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command)
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
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return Ok(response);
        }

    }
}
