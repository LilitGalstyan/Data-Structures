using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    #region Vertex
    class Vertex<T>
    {
        public Vertex(T value, params Vertex<T>[] parameters) : this(value, (IEnumerable<Vertex<T>>)parameters) { }

        public Vertex(T value, IEnumerable<Vertex<T>> neighbors = null)
        {
            Value = value;
            Neighbors = neighbors?.ToList() ?? new List<Vertex<T>>();
            IsVisited = false;
        }

        public T Value { get; }
        public List<Vertex<T>> Neighbors { get; }
        public bool IsVisited { get; set; }
        public int NeighborsCount => Neighbors.Count;

        public void AddEdge(Vertex<T> vertex)
        {
            Neighbors.Add(vertex);
        }

        public void AddEdges(params Vertex<T>[] newNeighbors)
        {
            Neighbors.AddRange(newNeighbors);
        }

        public void AddEdges(IEnumerable<Vertex<T>> newNeighbors)
        {
            Neighbors.AddRange(newNeighbors);
        }

        public void RemoveEdge(Vertex<T> vertex)
        {
            Neighbors.Remove(vertex);
        }

        public override string ToString()
        {
            return Neighbors.Aggregate(new StringBuilder($"{Value}: "), (sb, n) => sb.Append($"{n.Value} ")).ToString();
        }
    }
    #endregion

    #region Graph
    class Graph<T>
    {
        public Graph(params Vertex<T>[] initialNodes) : this((IEnumerable<Vertex<T>>)initialNodes) { }

        public Graph(IEnumerable<Vertex<T>> initialNodes = null)
        {
            Vertices = initialNodes?.ToList() ?? new List<Vertex<T>>();
        }

        public List<Vertex<T>> Vertices { get; }

        public int Size => Vertices.Count;

        private void AddToList(Vertex<T> vertex)
        {
            if (!Vertices.Contains(vertex))
            {
                Vertices.Add(vertex);
            }
        }

        private void AddNeighbor(Vertex<T> first, Vertex<T> second)
        {
            if (!first.Neighbors.Contains(second))
            {
                first.AddEdge(second);
            }
        }

        private void AddNeighbors(Vertex<T> first, Vertex<T> second)
        {
            AddNeighbor(first, second);
            //AddNeighbor(second, first);
        }

        public void AddPair(Vertex<T> first, Vertex<T> second)
        {
            AddToList(first);
            AddToList(second);
            AddNeighbors(first, second);
        }

        private void UnvisitAll()
        {
            foreach (var vertex in Vertices)
            {
                vertex.IsVisited = false;
            }
        }

        public void DepthFirstSearch(Vertex<T> root, Action<string> writer)
        {
            UnvisitAll();
            DepthFirstSearchImplementation(root, writer);
        }

        private void DepthFirstSearchImplementation(Vertex<T> root, Action<string> writer)
        {
            if (!root.IsVisited)
            {
                writer($"{root.Value} ");
                root.IsVisited = true;

                foreach (Vertex<T> neighbor in root.Neighbors)
                {
                    DepthFirstSearchImplementation(neighbor, writer);
                }
            }
        }
        public void BreadFirstSearch(Vertex<T> root, Action<string> writer)
        {
            UnvisitAll();
            BreadFirstSearchImplementation(root, writer);
        }

        private void BreadFirstSearchImplementation(Vertex<T> root, Action<string> writer)
        {
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            root.IsVisited = true;

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Vertex<T> current = queue.Dequeue();

                foreach (Vertex<T> neighbor in current.Neighbors)
                {
                    if (!neighbor.IsVisited)
                    {
                        writer($"{neighbor.Value} ");
                        neighbor.IsVisited = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        public bool IsCycleInGraph(Graph<Vertex<T>> graph)
        {
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            graph.Vertices[0].IsVisited = true;
            stack.Push(Vertices[0]);
            bool IsUnvisitedVertexFound = false;
            while (stack.Count() != 0)
            {
                Vertex<T> x = stack.Peek();
                foreach (var item in x.Neighbors)
                {
                    if (item.IsVisited)
                    {
                        item.IsVisited = true;
                        stack.Push(item);
                        IsUnvisitedVertexFound = true;
                        break;
                    }

                    else
                    {
                        if (stack.Contains(item))
                        {
                            foreach (var s in stack)
                            {
                                Console.WriteLine($"{s.Value} ");
                            }

                            return true;
                        }
                    }
                }
                if (!IsUnvisitedVertexFound) stack.Pop();
                IsUnvisitedVertexFound = false;
            }
            return false;
        }
    }
    #endregion
}
