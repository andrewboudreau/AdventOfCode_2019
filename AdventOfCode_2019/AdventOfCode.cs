using System;
using AdventOfCode_2019.Week01;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class AdventOfCode
    {
        public static ILoggerFactory LogFactory;

        public static LogLevel LoggingLevel = LogLevel.Critical;

        public static void Main(string[] args)
        {
            using var scope = ConfigureServices().CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var logger = serviceProvider.GetRequiredService<ILogger<AdventOfCode>>();

            var day = serviceProvider.GetRequiredService<Day13>();

            logger.LogCritical($"Solution: {day.Solve()}");
            logger.LogCritical($"Solution Part 2: {day.Solve2()}");

            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices(ServiceCollection services)
        {
            var singleLineConsoleLoggerProvider = new SingleLineConsoleLogger();
            services
                .AddLogging(configure => configure.ClearProviders().AddProvider(singleLineConsoleLoggerProvider).SetMinimumLevel(LoggingLevel));

            var serviceProvider = services.BuildServiceProvider();
            LogFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = serviceProvider.GetRequiredService<ILogger<AdventOfCode>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddTransient<Day01>();
            services.AddTransient<Day02>();
            services.AddTransient<Day03>();
            services.AddTransient<Day04>();
            services.AddTransient<Day05>();
            services.AddTransient<Day06>();
            services.AddTransient<Day07>();
            services.AddTransient<Day08>();
            services.AddTransient<Day09>();
            services.AddTransient<Day10>();
            services.AddTransient<Day11>();
            services.AddTransient<Day12>();
            services.AddTransient<Day13>();
            ////services.AddTransient<Day14>();
            ////services.AddTransient<Day15>();
            ////services.AddTransient<Day16>();
            ////services.AddTransient<Day17>();
            ////services.AddTransient<Day18>();
            ////services.AddTransient<Day19>();

            services.AddTransient(sp => new IntCodeCpuClassic(sp.GetRequiredService<ILogger<IntCodeCpuClassic>>()));
            services.AddTransient(sp => new IntCodeCpuMemory(sp.GetRequiredService<ILogger<IntCodeCpuMemory>>()));
            services.AddTransient(sp => new IntCodeCpu(sp.GetRequiredService<ILogger<IntCodeCpu>>(), sp.GetRequiredService<IntCodeCpuMemory>()));

            return services.BuildServiceProvider();
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            return ConfigureServices(services);
        }
    }
}
