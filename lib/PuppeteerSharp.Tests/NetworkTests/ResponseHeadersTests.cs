using System.Threading.Tasks;
using PuppeteerSharp.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace PuppeteerSharp.Tests.NetworkTests
{
    [Collection(TestConstants.TestFixtureCollectionName)]
    public class ResponseHeadersTests : PuppeteerPageBaseTest
    {
        public ResponseHeadersTests(ITestOutputHelper output) : base(output)
        {
        }

        [PuppeteerTest("network.spec.ts", "Request.headers", "should work")]
        [Fact(Timeout = TestConstants.DefaultTestTimeout)]
        public async Task ShouldWork()
        {
            Server.SetRoute("/empty.html", (context) =>
            {
                context.Response.Headers["foo"] = "bar";
                return Task.CompletedTask;
            });

            var response = await Page.GoToAsync(TestConstants.EmptyPage);
            Assert.Contains("bar", response.Headers["foo"]);
        }
    }
}
