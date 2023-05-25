using GraphApp.Extentions;
using GraphApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceFromGivenVertex.Algorithms
{
    public static class SearchDistancesFromGivenVertex
    {
        public static int[] Dijkstra(int startVertex, List<List<int>> _matrix)
        {
            int[] distances = new int[_matrix.Count]; // array of distances from startVertex
            for (int i = 0; i < distances.Length; i++) distances[i] = int.MaxValue;

            bool[] visited = new bool[_matrix.Count]; // visited vertices
            for (int i = 0; i < _matrix.Count; i++) visited[i] = false;
           
            int k = 0; // number of visited vertices
            distances[startVertex] = 0;

            // number of visits for each vertex
            int[] count = new int[_matrix.Count];
            for (int i = 0; i < _matrix.Count; i++) count[i] = 0;
            

            while (k != _matrix.Count)
            {
                // choose an unprocessed vertex with the minimum distance label
                int md = int.MaxValue;
                int v = -1;

                for (int i = 0; i < _matrix.Count; i++)
                {
                    if (!visited[i] && distances[i] < md)
                    {
                        md = distances[i];
                        v = i;
                    }
                }

                if (v == -1) break;

                // mark the vertex as processed
                visited[v] = true;
                k++;
                count[v]++;

                // update labels for neighbors of v
                var neigh = _matrix.AdjacencyList(v);
                foreach (int n in neigh)
                {
                    // new distance
                    int nd = distances[v] + _matrix[v][n];

                    if (nd < distances[n])
                    {
                        distances[n] = nd;

                        // Ford's modification
                        if (visited[n])
                        {
                            visited[n] = false;
                            k--;
                        }
                    }
                }

                // if the vertex has been visited n times, there is a cycle in the graph

                foreach (int c in count)
                {
                    if (c == _matrix.Count)
                    {
                        return new int[] { };
                    }
                }
            }

            return distances;
        }

        public static int[] BellmanFord(int startVertex, List<List<int>> _matrix)
        {
            int[] distances = new int[_matrix.Count];
            for (int i = 0; i < distances.Length; i++) distances[i] = int.MaxValue - 5000;

            distances[startVertex] = 0;

            // список ребер
            var edges = new List<Tuple<int, int, int>>();

            // матрицу смежности переводим в список ребер
            for (int i = 0; i < _matrix.Count; i++)   
            {
                for (int j = 0; j < _matrix.Count; j++)
                {
                    if (_matrix[i][j] != 0)
                    {
                        edges.Add(new Tuple<int, int, int>(i, j, _matrix[i][j]));
                    }
                }
            }

            for (int i = 0; i < _matrix.Count - 1; i++)
            {
                foreach (var e in edges)
                {
                    distances[e.Item2] = Math.Min(distances[e.Item2], distances[e.Item1] + e.Item3);
                }
            }

            foreach (var e in edges)
            {
                if (distances[e.Item2] > distances[e.Item1] + e.Item3)
                {
                    return new int[] { };
                }
            }

            return distances;

        }

        public static int[] Levit(int startVertex, List<List<int>> _matrix)
        {
            int[] distances = new int[_matrix.Count];
            for (int i = 0; i < distances.Length; i++) distances[i] = int.MaxValue - 5000;

            distances[startVertex] = 0;

            // 2 - unprocessed, 1 - processing, 0 - processed
            int[] belong = new int[_matrix.Count];
            for (int i = 0; i < belong.Length; i++) belong[i] = 2;

            belong[startVertex] = 1;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startVertex);
            Queue<int> urgentQueue = new Queue<int>();

            while (queue.Count != 0 || urgentQueue.Count != 0)
            {
                int u = urgentQueue.TryDequeue(out int urgent) ? urgent : queue.Dequeue();

                var neigh = _matrix.AdjacencyList(u);

                foreach (int v in neigh)
                {
                    if (belong[v] == 2)
                    {
                        distances[v] = Math.Min(distances[v], distances[u] + _matrix[u][v]);
                        belong[v] = 1;
                        queue.Enqueue(v);
                    }

                    if (belong[v] == 1)
                    {
                        distances[v] = Math.Min(distances[v], distances[u] + _matrix[u][v]);
                    }

                    if (belong[v] == 0 && distances[v] > distances[u] + _matrix[u][v])
                    {
                        distances[v] = distances[u] + _matrix[u][v];
                        belong[v] = 1;
                        urgentQueue.Enqueue(v);
                    }
                }
                belong[u] = 0;

                foreach (int d in distances)
                {
                    if (d < 0)
                    {
                        return new int[] { };
                    }
                }
            }

            return distances;

        }

    }
}
