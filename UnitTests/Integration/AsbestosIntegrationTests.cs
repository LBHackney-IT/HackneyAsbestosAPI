using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Integration
{
    public class AsbestosIntegrationTests
    {
        [Fact]
        public async Task return_500_when_internal_server_error()
        {
            // TODO
            Assert.True(false);
        }

        // Reference 00000000 returns no results
        [Fact]
        public async Task return_400_if_request_is_successful_but_no_results()
        {
            // TODO
            Assert.True(false);
        }
    }
}
