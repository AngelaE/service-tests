using BookApi.Models;
using BookApi.OpenApi;
using BookApi.Store;
using BookStats.Autorest;
using BookStats.Autorest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Consumes(contentType: ContentTypes.APPLICATION_JSON)]
  [Produces(contentType: ContentTypes.APPLICATION_JSON)]
  public class BooksController : ControllerBase
  {
    private IBookStore _bookStore;
    private IBookStatsClient _bookStats;

    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookStore bookStore, IBookStatsClient bookStats, ILogger<BooksController> logger)
    {
      _bookStore = bookStore;
      _bookStats = bookStats;
      _logger = logger;
    }

    /// <summary>
    /// Get the list of all books
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [SwaggerOperation(OperationId = "GetAll")]
    public async Task<IEnumerable<Book>> Get()
    {
      return await _bookStore.GetBooks();
    }

    [HttpGet]
    [Route("{bookId:int}")]
    [ProducesResponseType(typeof(BookWithStats), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int bookId)
    {
      var book = await _bookStore.GetBook(bookId);
      if (book == null) return NotFound();

      BookStatsModel stats = null;
      try
      {
        stats = _bookStats.GetBookStats(bookId);
      }
      catch (Exception) { }

      return Ok(new BookWithStats {
        Id = book.Id,
        Author = book.Author,
        Title = book.Title,
        Type = book.Type,
        CopiesSold = stats?.CopiesSold,
      });
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    public async Task<IActionResult> StoreBook(Book book)
    {
      if (book == null) return BadRequest();

      await _bookStore.AddBook(book);

      return Ok(book);
    }
  }
}
