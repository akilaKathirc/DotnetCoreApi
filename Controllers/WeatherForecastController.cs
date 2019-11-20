using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController(DataContext context)
        {
            _context = context;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext _context;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}
 [AllowAnonymous]
        [HttpGet("GetWheathers")]
       public  async Task<IActionResult> GetWheathers()
        {
            var response =await _context.Weathers.ToListAsync();
            return Ok(response);
        }
 [AllowAnonymous]
        [HttpGet("GetWheathers/{id}")]
        public async  Task<IActionResult> GetWheather(int id)
        {
            var response =await _context.Weathers.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(response);
        }


    }
}
