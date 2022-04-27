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
        public async Task<IActionResult> Get([FromQuery] GetAllSuplierPaginatedRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllSuplierPaginatedQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetSuplierByIdQuery() { Id = id }));
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateSuplierCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Post), response);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateSuplierCommand command)
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
            var response = await Mediator.Send(new DeleteSuplierCommand { Id = id });

            return Ok(response);
        }
    }
}
