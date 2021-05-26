using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
  public enum CoverType
  {
    Hardcover,
    Paperback,
    eBook
  }

  public class Book
  {
    [Required]
    public int Id { get; init; }

    [Required]
    public string Title { get; init; }

    [Required]
    public string Author { get; init; }

    public string Summary { get; init; }

    [Required]
    public CoverType Type { get; init; }
  }

  public class BookWithStats
  {
    [Required]
    public int Id { get; init; }

    [Required]
    public string Title { get; init; }

    [Required]
    public string Author { get; init; }

    public string Summary { get; init; }

    [Required]
    public CoverType Type { get; init; }

    public int? CopiesSold { get; init; }
  }
}
