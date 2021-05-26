using System;

namespace BookApi.Configuration
{
  public interface IServiceDiscovery
  {
    public Uri GetServiceUri(string name);
  }
}
