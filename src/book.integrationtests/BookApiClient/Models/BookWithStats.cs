// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace IntegrationTests.Clients.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class BookWithStats
    {
        /// <summary>
        /// Initializes a new instance of the BookWithStats class.
        /// </summary>
        public BookWithStats()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the BookWithStats class.
        /// </summary>
        public BookWithStats(int id, string title, string author, int? copiesSold = default(int?))
        {
            Id = id;
            Title = title;
            Author = author;
            CopiesSold = copiesSold;
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

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "copiesSold")]
        public int? CopiesSold { get; set; }

    }
}
