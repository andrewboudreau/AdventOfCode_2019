using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode_2019
{
    public abstract class Day00
    {
        private const string DefaultFileName = "input.dat";
        protected readonly ILogger logger;

        public Day00(IServiceProvider serviceProvider, ILogger<Day00> logger)
        {
            ServiceProvider = serviceProvider;
            this.logger = logger;
        }

        public int Day => int.Parse(GetType().Name.ToLowerInvariant().Replace("day", ""));

        public virtual string InputFile => $@"{Day}_{DefaultFileName}";

        public IServiceProvider ServiceProvider { get; }

        public IEnumerable<string> DirectInput { get; protected set; }

        public virtual IEnumerable<string> ReadInput()
        {
            if (DirectInput != null)
            {
                logger.LogInformation($"Using direct input.");
                if (DirectInput == null)
                {
                    throw new InvalidOperationException($"No input file or direct input found for {this}");
                }

                foreach (var line in DirectInput)
                {
                    yield return line;
                }
            }
            else
            {
                var inputFile = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), InputFile, new EnumerationOptions() { RecurseSubdirectories = true }).FirstOrDefault();
                logger.LogInformation($"Using input file {inputFile}");
                using var file = new StreamReader(inputFile);
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    yield return line;
                }

            }
        }

        public virtual string Solve()
        {
            return Solve(ReadInput());
        }

        protected abstract string Solve(IEnumerable<string> inputs);

        public virtual string Solve2()
        {
            return Solve2(ReadInput());
        }

        protected virtual string Solve2(IEnumerable<string> inputs)
        {
            return "N/A";
        }
    }
}
