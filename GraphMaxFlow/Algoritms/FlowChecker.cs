
using GraphApp.Extentions;
using GraphApp.Objects;
using System.Security.Cryptography.X509Certificates;

namespace GraphMaxFlow.Algoritms
{
    public class FlowChecker
    {
        private readonly Graph _graph;
        private readonly List<List<int>> _matrix;
        private readonly int _source;
        private readonly int _sink;
        public FlowChecker(Graph graph)
        {
            _graph = graph;
            _matrix = graph.AdjacencyMatrix();
            _source = FindSource(_matrix);
            _sink = FindSink(_matrix);

            if(_sink < 0 || _source < 0)
            {
                throw new Exception("There is no source or sink");
            }
        }

        public FlowChecker()
        {
            _source = 0;
            _sink = 5;
        }

        public int FindMaxFlow()
        {
            return 0;
        }

        public IEnumerable<(int v, int u, int weight)> FirstWayToSink(List<List<int>> matrix)
        {
            var result = new List<int>();
            var queue = new Queue<int>();
            var used = new bool[matrix.Count];
            var parents = new int[matrix.Count];

            queue.Enqueue(_source);
            used[_source] = true;
            parents[_source] = _source;

            bool isSinkFind = false;

            while (queue.Any())
            {
                int currentVertex = queue.Dequeue();

                var neighbors = matrix.AdjacencyList(currentVertex).Where(v => !used[v]);

                foreach(var n in neighbors)
                {
                    queue.Enqueue(n);
                    used[n] = true;
                    parents[n] = currentVertex;

                    if(n == _sink)
                    {
                        isSinkFind = true;
                        break;
                    }
                }

                if(isSinkFind) { break; }
            }

            int stepVertex = _sink;
            while (stepVertex != _source) 
            {
                result.Add(stepVertex);
                stepVertex = parents[stepVertex];
            }
            result.Add(_source);
            result.Reverse();

            var resultInEdges = new List<(int v, int u, int weight)>();

            for(int i = 0; i < result.Count - 1; i++)
            {
                resultInEdges.Add((v: result[i], u: result[i + 1], weight: matrix[result[i]][result[i+1]]));
            }

            return resultInEdges;
        }

        private static int FindSource(List<List<int>> matrix)
        {
            for(int j = 0; j < matrix.Count; j++)
            {
                bool isItSource = true;

                for(int i = 0; i < matrix.Count; i++)
                {
                    if (matrix[i][j] > 0)
                    {
                        isItSource = false;
                    }
                }

                if(isItSource) { return j; }
            }

            return -1;
        }

        private static int FindSink(List<List<int>> matrix)
        {
            for(int i = 0; i < matrix.Count; i++)
            {
                var edgesFrom = matrix[i].Where(n => n > 0);

                if (!edgesFrom.Any())
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
