using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> _forecasts = new List<WeatherForecast>
        {
            new WeatherForecast { Id = 1, Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" }
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // endpoint for get all weather forecast
        [HttpGet]
        public IEnumerable<WeatherForecast> GetAll()
        {
            return _forecasts;
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var forecast = _forecasts.FirstOrDefault(f => f.Id == id);

            if (forecast == null)
            {
                return NotFound();
            }

            _forecasts.Remove(forecast);

            return NoContent();

        }


    }
}
