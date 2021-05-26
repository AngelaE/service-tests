using BookApi.Configuration;
using IntegrationTests.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using WireMock.Server;
using Xunit.Abstractions;

namespace IntegrationTests
{
  public class DefaultTestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
  {
    public ITestOutputHelper TestOutputHelper { get; set; }
    public WireMockServer BookStats { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      BookStats = WireMockServer.Start();
      var discovery = new TestDiscovery(BookStats);

      // Register the xUnit logger
      builder.ConfigureLogging(loggingBuilder =>
      {
        loggingBuilder.AddProvider(new XUnitLoggerProvider(TestOutputHelper));
      });
      builder.ConfigureServices(services =>
      {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IServiceDiscovery));
        services.Remove(descriptor);

        services.AddSingleton<IServiceDiscovery>(new TestDiscovery(BookStats));
      });
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      BookStats.Stop();
    }
  }
}
