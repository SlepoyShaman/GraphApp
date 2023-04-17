using GraphApp.Objects;

namespace GraphApp.Algoritms
{
    public class GraphParametrs
    {
        private readonly List<List<int>> _distanсesMatrix;
        private readonly Graph _graph;
        private readonly bool _isGraphAdjancy;
        public GraphParametrs(Graph graph)
        {
            _distanсesMatrix = MakeDistancesMatrix(graph);
            _isGraphAdjancy = IsGraphAdjancy(_distanсesMatrix);
            _graph = graph;
        }

        public List<List<int>> GetDistancesMatrix() => _distanсesMatrix.Select(l => l.ToList()).ToList();

        public IEnumerable<(int, int)> VertexDegrees()
        {
            (int, int)[] result = new (int, int)[_distanсesMatrix.Count];

            var matrix = _graph.AdjacencyMatrix();

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix.Count; j++)
                {
                    if (matrix[i][j] > 0)
                    {
                        result[i].Item1 += 1;
                        result[j].Item2 += 1;
                    }
                }
            }

            return result;
        }

        public int GetRadius()
        {
            if (_graph.IsDirected() || !_isGraphAdjancy)
                return -1;
            else
                return _distanсesMatrix.Select(l => l.Max()).Min();
        }

        public int GetDiametr()
        {
            if (_graph.IsDirected() || !_isGraphAdjancy)
                return -1;
            else
                return _distanсesMatrix.Select(l => l.Max()).Max();
        }

        public IEnumerable<int> Centers()
        {
            if (_graph.IsDirected() || !_isGraphAdjancy) yield break;

            var maxDistances = _distanсesMatrix.Select(l => l.Max()).ToArray();

            for (int i = 0; i < maxDistances.Length; i++)
            {
                if (maxDistances[i] == GetRadius()) yield return i;
            }
        }

        public IEnumerable<int> Peripheral()
        {
            if (_graph.IsDirected() || !_isGraphAdjancy) yield break;

            var maxDistances = _distanсesMatrix.Select(l => l.Max()).ToArray();

            for (int i = 0; i < maxDistances.Length; i++)
            {
                if (maxDistances[i] == GetDiametr()) yield return i;
            }
        }

        private static bool IsGraphAdjancy(List<List<int>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix.Count; j++)
                {
                    if (matrix[i][j] == 0 & i != j) return false;
                }
            }

            return true;
        }
        private static List<List<int>> MakeDistancesMatrix(Graph graph)
        {
            var distanсesMatrix = graph.AdjacencyMatrix();
            for (int k = 0; k < distanсesMatrix.Count; k++)
            {
                for (int i = 0; i < distanсesMatrix.Count; i++)
                {
                    for (int j = 0; j < distanсesMatrix.Count; j++)
                    {
                        if (distanсesMatrix[i][k] != 0 & distanсesMatrix[k][j] != 0 & i != j)
                            distanсesMatrix[i][j] = Math.Min(distanсesMatrix[i][k] + distanсesMatrix[k][j],
                                    distanсesMatrix[i][j] == 0 ? Int32.MaxValue : distanсesMatrix[i][j]);
                    }
                }
            }

            return distanсesMatrix;
        }
    }
}
