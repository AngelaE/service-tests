using BookApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Store
{
  public interface IBookStore
  {
    Task AddBook(Book book);
    Task<IEnumerable<Book>> GetBooks();
    Task<Book> GetBook(int id);
  }
}
