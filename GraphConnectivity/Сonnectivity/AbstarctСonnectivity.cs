using GraphApp.Extentions;
using GraphApp.Objects;

namespace GraphConnectivity.Сonnectivity
{
    public abstract class AbstarctСonnectivity
    {
        protected readonly List<List<int>> _graphMatrix;
        public AbstarctСonnectivity(Graph graph)
        {
            _graphMatrix = graph.AdjacencyMatrix();
        }

        protected static bool IsMatrixСonnected(List<List<int>> matrix)
        {
            var vertexInOneComponent = BFS(0, matrix);

            if (vertexInOneComponent.Count() == matrix.Count)
                return true;

            return false;
        }

        protected static IEnumerable<IEnumerable<int>> GetСonnectivityComponentsByMatrix(List<List<int>> matrix)
        {
            var result = new List<IEnumerable<int>>();
            var allVertex = new List<int>();
            for (int i = 0; i < matrix.Count; i++) { allVertex.Add(i); }

            if (IsMatrixСonnected(matrix))
            {
                result.Add(allVertex);
                return result;
            }

            while (allVertex.Any())
            {
                var vertexInOneComponent = BFS(allVertex.First(), matrix);

                result.Add(vertexInOneComponent);

                foreach (var v in vertexInOneComponent)
                    allVertex.Remove(v);
            }

            return result;

        }

        protected static IEnumerable<int> BFS(int vertex, List<List<int>> matrix)
        {
            var result = new List<int>();
            var queue = new Queue<int>();

            queue.Enqueue(vertex);
            result.Add(vertex);
            while (queue.Any())
            {
                int currentVertex = queue.Dequeue();

                var neighbors = matrix.AdjacencyList(currentVertex).Where(v => !result.Contains(v));

                foreach (var v in neighbors)
                {
                    queue.Enqueue(v);
                    result.Add(v);
                }
            }

            return result;
        }

        protected static IEnumerable<int> DFSWithCounters(int vertex,ref int counter, int[] counters, List<List<int>> matrix)
        {
            var vertexes = new List<int>();
            var stack = new Stack<int>();

            stack.Push(vertex);
            while (stack.Any())
            {
                int currentVertex = stack.Pop();

                if (counters[currentVertex] == 0) vertexes.Add(currentVertex);
                counters[currentVertex] = ++counter;

                var neigbors = matrix.AdjacencyList(currentVertex).Where(v => counters[v] == 0);
                foreach (var v in neigbors) 
                { 
                    stack.Push(currentVertex);
                    stack.Push(v);
                    break;
                }
            }

            return vertexes;
        }
    }
}
