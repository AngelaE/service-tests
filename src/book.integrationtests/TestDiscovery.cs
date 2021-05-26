using BookApi.Configuration;
using System;
using WireMock.Server;

namespace IntegrationTests
{
  public class TestDiscovery : IServiceDiscovery
  {
    private WireMockServer _server;

    public TestDiscovery(WireMockServer server)
    {
      _server = server;
    }

    public Uri GetServiceUri(string name)
    {
      var url = $"http://localhost:{_server.Ports[0]}";
      return new Uri(url);
    }
  }
}
