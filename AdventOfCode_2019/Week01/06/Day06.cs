using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    partial class Day06 : Day00
    {
        public Day06(IServiceProvider serviceProvider, ILogger<Day06> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = new[] { "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            foreach (var orbit in inputs)
            {
                var parts = orbit.Split(')');
                var left = parts[0];
                var rigtht = parts[1];
            }

            return "N/A";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var range = inputs.Select(int.Parse).ToList();
            var start = range[0];
            var end = range[1];
            var count = 0;

            var allValuesIncrease = new IncreasingRule();
            var containsRepeatedValue = new SingleRepeatedValueRule();
            for (var password = start; password <= end; password++)
            {
                if (allValuesIncrease.Check(password) && containsRepeatedValue.Check(password))
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }

    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public ICollection<Node<T>> Left = new Collection<Node<T>>();

        public ICollection<Node<T>> Right = new Collection<Node<T>>();

        public T Value;
    }

    public class OrbitMap<T>
    {
        private Dictionary<T, Node<T>> Index = new Dictionary<T, Node<T>>();

        public Node<T> Root;

        public void AddOrbit(T source, T orbiter)
        {
            if (Root == null)
            {
                Root = new Node<T>(source);
                Index.Add(source, Root);
            }

            if (Index.ContainsKey(source) && Index.ContainsKey(orbiter))
            {
                throw new NotImplementedException("Both source and orbiter already exist as nodes");
            }

            if (Index.ContainsKey(orbiter))
            {
                throw new NotImplementedException("Orbiter already exists as a node.");
            }

            if (Index.ContainsKey(source))
            {
                var orbiterNode = new Node<T>(orbiter);
                orbiterNode.Left.Add(Index[source]);
                Index[source].Right.Add(orbiterNode);
                Index.Add(orbiterNode.Value, orbiterNode);
            }
        }
    }
}
