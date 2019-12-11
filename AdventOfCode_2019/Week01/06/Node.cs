using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AdventOfCode_2019.Week01
{
    /// <summary>
    /// A node with 0 or 1 node on the left, and many nodes on the right.
    /// </summary>
    /// <typeparam name="T">Node contents</typeparam>
    public class Node<T>
    {
        private Node<T> left;

        public Node(T value)
        {
            Value = value;
        }

        public Node<T> Left
        {
            get
            {
                return left;
            }

            set
            {
                if (left != null)
                {
                    throw new InvalidOperationException($"Cannot set Left when a value is already set. Tried to set value '{Value}' but was '{left}'");
                }

                left = value;
            }
        }

        public ICollection<Node<T>> Right = new Collection<Node<T>>();

        public T Value;

        public override string ToString()
        {
            return string.Join(',', Right.Select(x => x.Value));
        }
    }
}
