// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace IntegrationTests.Clients.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Book
    {
        /// <summary>
        /// Initializes a new instance of the Book class.
        /// </summary>
        public Book()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Book class.
        /// </summary>
        public Book(int id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

    }
}
