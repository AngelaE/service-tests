using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace BookStats.Api.OpenApi
{
  public class AdditionalPropertiesDocumentFilter : IDocumentFilter
  {
    public void Apply(OpenApiDocument openApiDoc, DocumentFilterContext context)
    {
      foreach (var schema in context.SchemaRepository.Schemas.Where(schema => schema.Value.AdditionalProperties == null))
      {
        schema.Value.AdditionalPropertiesAllowed = true;
      }
    }
  }
}
