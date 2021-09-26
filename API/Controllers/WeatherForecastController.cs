using API.DTOs.WeatherForecast;
using API.Services.WeatherForecasts;
using API.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Weather Forecast Controller
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public WeatherForecastController(WeatherForecastService service)
        {
            _service = service;
        }

        /// <summary>
        /// Swagger comment
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Response for the request</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetWeatherForecastResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public ActionResult Get([FromQuery] GetWeatherForecastRequest request)
        {
            var data = _service.GetByDate(request);
            if (data == null)
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, "Not Result found"));
            else
                return Ok(data);
        }

        /// <summary>
        /// Swagger comment Authorize
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Response for the request</returns>
        [HttpGet("GetAuthorize")]
        [Authorize]
        [ProducesResponseType(typeof(GetWeatherForecastResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public ActionResult GetAuthorize([FromQuery] GetWeatherForecastRequest request)
        {
            return Get(request);
        }

        /// <summary>
        /// Swagger comment Admin
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Response for the request</returns>
        [HttpGet("GetAdmin")]
        [Authorize("admin")]
        [ProducesResponseType(typeof(GetWeatherForecastResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public ActionResult GetAdmin([FromQuery] GetWeatherForecastRequest request)
        {
            return Get(request);
        }
    }
}