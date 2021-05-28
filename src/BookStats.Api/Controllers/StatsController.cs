using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BookStats.Api.Controllers
{
  public class Stats
  {
    public int BookId { get; init; }
    public int CopiesSold { get; init; }
  }

  public static class ContentTypes
  {
    public const string APPLICATION_JSON = "application/json";
  }

  [ApiController]
  [Route("[controller]")]
  [Consumes(contentType: ContentTypes.APPLICATION_JSON)]
  [Produces(contentType: ContentTypes.APPLICATION_JSON)]
  public class StatsController : ControllerBase
  {
    private static readonly Stats[] _stats = new[]
    {
            new Stats{ BookId = 1, CopiesSold = 21453},
            new Stats{ BookId = 2, CopiesSold = 2851},
        };

    private readonly ILogger<StatsController> _logger;

    public StatsController(ILogger<StatsController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    [Route("{bookId:int}")]
    [ProducesResponseType(typeof(Stats), StatusCodes.Status200OK)]
    public IActionResult Get(int bookId)
    {
      var stats = _stats.FirstOrDefault(s => s.BookId == bookId);
      if (stats == null) return NotFound();

      return Ok(stats);
    }
  }
}
