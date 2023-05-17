using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JDMarketSLn.WebApi.Filters;

namespace JDMarketSLn.WebApi.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
