using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2019.Week01
{
    public class OrbitMap<T>
    {
        public Node<T> Root;

        public readonly Dictionary<T, Node<T>> Nodes = new Dictionary<T, Node<T>>();

        public OrbitMap(IEnumerable<(T Source, T Orbiter)> inputs)
        {
            foreach (var (Source, Orbiter) in inputs)
            {
                AddOrbit(Source, Orbiter);
            }
        }

        public void AddOrbit(T source, T orbiter)
        {
            if (Root == null)
            {
                Root = new Node<T>(source);
                Nodes.Add(source, Root);
            }

            if (!Nodes.ContainsKey(source) && !Nodes.ContainsKey(orbiter))
            {
                // both source and orbit are new.
                var sourceNode = new Node<T>(source);
                var orbiterNode = new Node<T>(orbiter);

                sourceNode.Right.Add(orbiterNode);
                orbiterNode.Left = sourceNode;

                Nodes.Add(sourceNode.Value, sourceNode);
                Nodes.Add(orbiterNode.Value, orbiterNode);
            }
            else if (Nodes.ContainsKey(source) && Nodes.ContainsKey(orbiter))
            {
                // both keys already exist.
                var orbiterNode = Nodes[orbiter];
                var sourceNode = Nodes[source];

                orbiterNode.Left = sourceNode;
                sourceNode.Right.Add(orbiterNode);
            }
            else if (Nodes.ContainsKey(source))
            {
                // 'source' key exists but 'orbiter' key doesn't.
                var orbiterNode = new Node<T>(orbiter);
                var sourceNode = Nodes[source];

                orbiterNode.Left = sourceNode;
                sourceNode.Right.Add(orbiterNode);
                Nodes.Add(orbiterNode.Value, orbiterNode);
            }
            else
            {
                // 'orbiter' key exists but 'source' key doesn't.
                var sourceNode = new Node<T>(source);
                var orbiterNode = Nodes[orbiter];

                orbiterNode.Left = sourceNode;
                sourceNode.Right.Add(orbiterNode);
                Nodes.Add(sourceNode.Value, sourceNode);
            }
        }

        internal int CountDirectAndIndirectOrbits()
        {
            var sum = 0;
            foreach (var node in Nodes.Values)
            {
                if (node.Left == null)
                {
                    continue;
                }

                var next = node.Left;
                do
                {
                    sum++;
                    next = next.Left;
                } while (next != null);
            }

            return sum;
        }

        internal int CountOribitalTransfersRequired(T source, T destination)
        {
            var pathFromSource = new List<T>();
            var pathFromDestination = new List<T>();

            var sourceNode = Nodes[source];
            var destinationNode = Nodes[destination];

            var node = sourceNode;
            while(node.Left != null)
            {
                pathFromSource.Add(node.Left.Value);
                node = node.Left;
            }

            node = destinationNode;
            while (node.Left != null)
            {
                pathFromDestination.Add(node.Left.Value);
                node = node.Left;
            }

            var orbitalTransfers = pathFromDestination.Except(pathFromSource)
                .Union(pathFromSource.Except(pathFromDestination)).Count();

            return orbitalTransfers;
        }
    }
}
