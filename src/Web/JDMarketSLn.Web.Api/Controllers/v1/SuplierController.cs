using JdMarketSln.Application.Features.Suppliers.Commands.CreateSuplier;
using JdMarketSln.Application.Features.Suppliers.Commands.DeleteSuplier;
using JdMarketSln.Application.Features.Suppliers.Commands.UpdateSuplier;
using JdMarketSln.Application.Features.Suppliers.Queries.GetAllSupliersPaginated;
using JdMarketSln.Application.Features.Suppliers.Queries.GetSuplierById;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JDMarketSLn.Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SuplierController : BaseApiController
    {

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSuplierPaginatedRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllSuplierPaginatedQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetSuplierByIdQuery() { Id = id }));
        }

        [HttpPost]
        //[Authorize]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create(CreateSuplierCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Create), response);
        }

        [HttpPut]
        //[Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(Guid id, UpdateSuplierCommand command)
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteSuplierCommand { Id = id });

            return Ok(response);
        }
    }
}
