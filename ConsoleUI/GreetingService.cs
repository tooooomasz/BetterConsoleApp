using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleUI
{
    public class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService> _log;
        private readonly IConfiguration _config;

        public GreetingService(ILogger<GreetingService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            var loopTimes = _config.GetValue<int>("LoopTimes");
            for (int i = 0; i < loopTimes; i++)
            {
                _log.LogInformation("Run number {runNumber}", i);
            }
        }
    }
}
