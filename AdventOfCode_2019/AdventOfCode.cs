using System;
using AdventOfCode_2019.Week01;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class AdventOfCode
    {
        public static ILoggerFactory LogFactory;

        public static void Main(string[] args)
        {
            using var scope = ConfigureServices().CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var logger = serviceProvider.GetRequiredService<ILogger<AdventOfCode>>();
            try
            {
                var day = serviceProvider.GetRequiredService<Day04>();
                logger.LogInformation($"Solution: {day.Solve()}");
                logger.LogInformation($"Solution Part 2: {day.Solve2()}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Information));

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<AdventOfCode>>();
            services.AddSingleton(typeof(ILogger), logger);
            LogFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            services.AddTransient<Day01>();
            services.AddTransient<Day02>();
            services.AddTransient<Day03>();
            services.AddTransient<Day04>();
            services.AddTransient(sp => new IntCodeCpu(sp.GetRequiredService<ILogger<IntCodeCpu>>()));

            return services.BuildServiceProvider();
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            return ConfigureServices(services);
        }
    }
}
