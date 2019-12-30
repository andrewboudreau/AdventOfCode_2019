using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AdventOfCode_2019.Week01;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day14 : Day00
    {
        public Day14(IServiceProvider serviceProvider, ILogger<Day13> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = new string[]
            {
                "10 ORE => 10 A",
                "1 ORE => 1 B",
                "7 A, 1 B => 1 C",
                "7 A, 1 C => 1 D",
                "7 A, 1 D => 1 E",
                "7 A, 1 E => 1 FUEL"
            };

            DirectInput = null;
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var reactions = inputs.Select(x => new Reaction(x)).ToList();
            var root = reactions.Single(x => x.Output.Symbol == "FUEL");

            var reactants = new Dictionary<Reactant, List<Reactant>>();
            //var node = new Node<Reactant>();

            foreach (var reaction in reactions)
            {
                if (!reactants.ContainsKey(reaction.Output))
                {
                    reactants.Add(reaction.Output, new List<Reactant>());
                }
            }

            var reactionsFromOreOnly = reactions
                .Where(x => x.Input.Any(x => x.Equals("ORE")))
                .ToList();

            var level = reactions
                .Where(x => x.Input.Any(y => reactionsFromOreOnly.Any(r => r.Output.Equals(y))))
                .ToList();

            //foreach (var reactant in reactants)
            //{
            //    if (reactant.Value.Last().Symbol == "ORE")
            //    {
            //        Console.WriteLine("done");
            //    }
            //}
            // traverse teh tree till every node ends in ORE

            // var todo = new Stack<Node<Reactant>>();
            return "";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return $"NA";
        }

        public class Reaction
        {
            public Reaction(string reaction)
            {
                var parts = reaction.Split(" => ");

                var inputs = parts[0].Split(",");
                Input = inputs.Select(ToReactant).ToList();

                var output = parts[1];
                Output = ToReactant(output);
            }

            public List<Reactant> Input { get; private set; }

            public Reactant Output { get; private set; }

            public override string ToString()
            {
                return $"{string.Join(", ", Input)} => {Output}";
            }

            private Reactant ToReactant(string valueSymbol)
            {
                var parts = valueSymbol.Trim().Split(" ");
                var value = int.Parse(parts[0].Trim());
                var symbol = parts[1].Trim();

                return new Reactant(symbol, value);
            }
        }

        public class Reactant : IEquatable<Reactant>, IEquatable<string>
        {
            public Reactant(string symbol, int value)
            {
                Symbol = symbol;
                Value = value;
            }

            public string Symbol { get; private set; }

            public int Value { get; private set; }

            public int HowManyNeededFor(int count)
            {
                if (Value <= count)
                {
                    return 1;
                }

                return (int)Math.Ceiling(count / (double)Value);
            }

            public override string ToString()
            {
                return $"{Value} {Symbol}";
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Reactant);
            }

            public bool Equals(Reactant other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                if (other is null)
                {
                    return false;
                }

                return Symbol == other.Symbol;
            }

            public bool Equals([AllowNull] string other)
            {
                return Symbol == other;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Symbol);
            }

            public static bool operator ==(Reactant obj1, Reactant obj2)
            {
                if (ReferenceEquals(obj1, obj2))
                {
                    return true;
                }

                if (obj1 is null)
                {
                    return false;
                }

                return obj1.Equals(obj2);
            }

            public static bool operator !=(Reactant obj1, Reactant obj2)
            {
                return !(obj1 == obj2);
            }

            public static bool operator ==(Reactant reactant, string symbol)
            {
                if (reactant is null)
                {
                    return false;
                }

                if (symbol is null)
                {
                    return false;
                }

                return reactant.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase);
            }

            public static bool operator !=(Reactant reactant, string symbol)
            {
                return !(reactant == symbol);
            }
        }
    }
}
