using System;
using System.Collections;
using System.Collections.Generic;
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

            var current = root;
            var tree = new Node<Reactant>(root.Output);


            // traverse teh tree till every node ends in ORE

            var todo = new Stack<Node<Reactant>>();

        PARSE_INPUT:
            foreach (var input in current.Input)
            {
                var node = new Node<Reactant>(input);
                tree.Right.Add(node);

                var candidate = reactions.SingleOrDefault(x => x.Output.Equals(input));
                if (candidate != null)
                {
                    foreach(var )
                    todo.Push(candidate.Input);
                }
            }

            var search = current.Input.FirstOrDefault();


            if (current != null)
                goto PARSE_INPUT;

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

        public class Reactant : IEquatable<Reactant>
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

                return count % Value;
            }

            public override string ToString()
            {
                return $"{Value} {Symbol}";
            }

            public bool Equals(Reactant other)
            {
                if (other == null)
                {
                    return false;
                }

                return other.Symbol == Symbol;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Reactant);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Symbol);
            }
        }
    }
}
