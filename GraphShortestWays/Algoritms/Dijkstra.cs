using GraphApp.Objects;
using System;

namespace GraphShortestWays.Algoritms
{
    public class Dijkstra
    {
        private readonly Graph _graph;
        public Dijkstra(Graph graph)
        {
            _graph = graph;
        }

        public IEnumerable<int> FindShortestWay(int start, int end, out int distance)
        {
            var d = new int[_graph.VertexCount()];
            for(int i = 0; i < d.Length; i++) { d[i] = int.MaxValue; }
            d[start] = 0;
            var visited = new bool[_graph.VertexCount()];
            var parents = new int[_graph.VertexCount()];

            while (!visited[end])
            {
                // выбор необработанной вершины с минимальной пометкой distance
                var md = int.MaxValue;
                var v = -1;
                for (int i = 0; i < _graph.VertexCount(); i++) 
                {
                    if (!visited[i] && d[i] < md)
                    {
                        md = d[i];
                        v = i;
                    }
                }

                if (md == int.MaxValue)
                {
                    distance = 0;
                    return new List<int>();
                }

                visited[v] = true;

                var neighbors = _graph.AdjacencyList(v);
                foreach (var n in neighbors)
                {
                    var nd = d[v] + _graph.Weight(v, n);
                    if (nd < d[n])
                    {
                        d[n] = nd;
                        parents[n] = v;
                    }
                }
            }

            distance = d[end];
            return GetRoute(parents, start, end);
        }

        private IEnumerable<int> GetRoute(int[] parents, int start, int end)
        {
            var route = new List<int>();
            for (int v = end; v != start; v = parents[v])
            {
                route.Add(v);
            }

            route.Reverse();
            return route;
        }

        private int GetMinDistanceVertex(bool[] used, int[] d)
        {
            int min = int.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < d.Length; i++)
            {
                if (!used[i] & min > d[i])
                {
                    min = d[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}
