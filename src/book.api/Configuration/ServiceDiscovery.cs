using Microsoft.Extensions.Configuration;
using System;

namespace BookApi.Configuration
{
  public class ServiceDiscovery : IServiceDiscovery
  {
    private IConfiguration _configuration;

    public ServiceDiscovery(IConfiguration configuration)
    {
      _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
    }

    public Uri GetServiceUri(string name)
    {
      var uri = _configuration.GetConnectionString(name);
      // todo: error handling - what if we cannot find the service?
      return new Uri(uri);
    }
  }
}
