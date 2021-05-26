using BookApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Store
{
  public class InMemoryBookStore : IBookStore
  {
    private List<Book> _books = new() { 
      new Book { Id = 1, Author = "Douglas Adams", Title = "Hitch Hikers Guide to the Galaxy", Type = CoverType.eBook},
      new Book { Id = 2, Author = "J.K. Rowling", Title = "Harry Potter", Type = CoverType.Hardcover},
    };

    public Task AddBook(Book book)
    {
      _books.Add(book);
      return Task.CompletedTask;
    }

    public Task<Book> GetBook(int id)
    {
      return Task.FromResult(_books.FirstOrDefault(b => b.Id == id));
    }

    public Task<IEnumerable<Book>> GetBooks()
    {
      return Task.FromResult<IEnumerable<Book>>(_books);
    }
  }
}
