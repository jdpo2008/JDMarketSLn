using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JDMarketSLn.Application.Common.Enums;
using JDMarketSLn.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts;

namespace JDMarketSLn.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class WeatherForecastController : ApiBaseController
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}