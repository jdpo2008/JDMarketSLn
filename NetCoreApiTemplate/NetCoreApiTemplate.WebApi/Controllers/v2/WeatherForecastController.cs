using Microsoft.AspNetCore.Mvc;
using NetCoreApiTemplate.Application.Features.WeatherForecasts.Queries.GetWeatherForecasts;

namespace NetCoreApiTemplate.WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class WeatherForecastController : ApiBaseController
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}