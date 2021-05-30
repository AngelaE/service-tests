using FluentAssertions;
using IntegrationTests;
using IntegrationTests.Clients;
using IntegrationTests.Clients.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Type = IntegrationTests.Clients.Models.Type;

namespace BookApi.IntegrationTests
{
  public class BookControllerTests : IClassFixture<DefaultTestWebApplicationFactory<Startup>>
  {
    private readonly DefaultTestWebApplicationFactory<Startup> _factory;
    private ITestOutputHelper _outputHelper;

    public BookControllerTests(DefaultTestWebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper)
    {
      _factory = factory;
      _outputHelper = outputHelper;
      _factory.TestOutputHelper = outputHelper;
    }

    [Fact]
    public async Task Get_Books_returns_two_books()
    {
      var client = _factory.CreateClient();
      var bookApiClient = new BookApiClient(client, false);
      var expectedBooks = new List<Book> {
        new Book{Id = 1},
        new Book{Id = 2}
      };

      // Act
      var books = await bookApiClient.Books.GetAllAsync();

      // Assert
      books.Should().NotBeNull();
     
      foreach(var expectedBook in expectedBooks)
      {
        var returnedBook = books.FirstOrDefault(b => b.Id == expectedBook.Id);
        returnedBook.Should().NotBeNull($"Book {expectedBook.Id} was expected in the returned list of books");
        // Any additional checks
        // Should the test break if the returned book contains additional fields? 
      }
      
      books.Count.Should().Be(2);
    }

    [Fact]
    public void Get_Books_returns_404_if_not_found()
    {
      var client = _factory.CreateClient();
      var bookApiClient = new BookApiClient(client, false);

      // Act
      Func<Task> requestBook = async () => await bookApiClient.Books.GetAsync(-1);

      // Assert
      requestBook.Should().Throw<HttpOperationException>()
          .Where(e => e.Response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }


    [Fact(Skip = "Creating a book at the wrong time will cause test failures. This test would need to go into a separate fixture")]
    public async Task Book_can_be_created()
    {
      var client = _factory.CreateClient();
      var bookApiClient = new BookApiClient(client, false);

      // Act
      var book = await bookApiClient.Books.StoreBookAsync(new Book { Id = 3, Title = "test title", Author = "author"});

      // Assert
      book.Should().NotBeNull();
    }

    [Fact]
    public async Task Create_book_throws_BadRequest_for_string_id()
    {
      var client = _factory.CreateClient();
      var bookString = JsonSerializer.Serialize(new Book { Id = 3, Title = "test title", Author = "author"});
      bookString = bookString.Replace("3", "invalid");

      var content = new StringContent(bookString,
          Encoding.UTF8,
          "application/json");

      // Act
      var response = await client.PostAsync("Books", content);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      _outputHelper.WriteLine($"Book sent: {bookString}");
      _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

  }
}
