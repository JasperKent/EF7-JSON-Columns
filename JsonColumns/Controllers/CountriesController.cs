using JsonColumns.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JsonColumns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly MapContext _context;

        public CountriesController(MapContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _context.Countries.ToArrayAsync();

            return Ok(results);
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var results = await _context.Countries.Where (c => c.Name == name).ToArrayAsync();

            return Ok(results);
        }

        [HttpGet("ByColour/{colour}")]
        public async Task<IActionResult> GetByColour(string colour)
        {
            var results = await _context.Countries.Where(c => c.Shape.Colour == colour).ToArrayAsync();

            return Ok(results);
        }


        [HttpGet("AboveLatitude/{latitude:double}")]
        public async Task<IActionResult> AboveLatitude(double latitude)
        {
            var latParam = new SqlParameter("latitude", latitude);

            var results = _context.Countries.FromSql($"""
                            SELECT DISTINCT [Id]
                                ,[Name]
                                ,[Shape]
                            FROM [JsonColumns-EF7].[dbo].[Countries]
                            CROSS APPLY
                            OPENJSON ([Shape], '$.Coordinates') WITH (Latitude float '$.Latitude') lat
                            WHERE lat.Latitude > {latParam}
                            """
            );
            return Ok(await results.ToArrayAsync());
        }
    }
}
