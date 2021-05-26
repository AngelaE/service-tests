using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

// Credits go to https://www.meziantou.net/how-to-get-asp-net-core-logs-in-the-output-of-xunit-tests.htm

namespace IntegrationTests.Logging
{
  internal sealed class XUnitLoggerProvider : ILoggerProvider
  {
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly LoggerExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();

    public XUnitLoggerProvider(ITestOutputHelper testOutputHelper)
    {
      _testOutputHelper = testOutputHelper;
    }

    public ILogger CreateLogger(string categoryName)
    {
      return new XUnitLogger(_testOutputHelper, _scopeProvider, categoryName);
    }

    public void Dispose()
    {
    }
  }
}
