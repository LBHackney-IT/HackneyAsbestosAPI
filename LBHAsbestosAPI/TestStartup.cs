using System;
using Microsoft.Extensions.Configuration;

namespace LBHAsbestosAPI
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration env) : base(env)
        {
            TestStatus.IsRunningIntegrationTests = true;
        }
    }
}
