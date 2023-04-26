using GraphApp.Extentions;
using GraphApp.Objects;

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
            var parents = new int[_graph.VertexCount()];
            var used = new bool[_graph.VertexCount()];
            var currentVertex = start;
            parents[currentVertex] = start;
            d[currentVertex] = 0; 

            while(used.Contains(false))
            {
                var neigbors = _graph.AdjacencyList(currentVertex);

                foreach(var n in neigbors)
                {
                    int cost = d[currentVertex] + _graph.Weight(currentVertex, n);
                    if(cost < d[n])
                    {
                        d[n] = cost;
                        parents[n] = currentVertex;
                    }
                }

                used[currentVertex] = true;

                currentVertex = GetMinDistanceVertex(used, d);
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

            for(int i = 0; i < d.Length; i++)
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
