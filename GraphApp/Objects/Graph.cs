using GraphApp.Extentions;
using GraphApp.GraphMatrixFactories;

namespace GraphApp.Objects
{
    public class Graph
    {
        private readonly List<List<int>> _matrix;
        public Graph(MatrixFactory factory)
        {
            _matrix = factory.GetMatrix();
        }

        public Graph(List<List<int>> matrix)
        {
            _matrix = matrix;
        }

        public int VertexCount() => _matrix.Count;

        public int Weight(int v1, int v2)
        {
            if (IsEdge(v1, v2)) return _matrix[v1][v2];
            else throw new Exception($"Ребро {v1}-{v2} не существует!");
        }

        public bool IsEdge(int v1, int v2)
        {
            if (v1 > _matrix.Count || v2 > _matrix.Count || v1 < 0 || v2 < 0)
                throw new Exception($"Обращение к несуществующей вершине! Обращение к {v1}, {v2}");

            return _matrix[v1][v2] != 0;
        }

        public List<List<int>> AdjacencyMatrix() => _matrix.Select(l => l.ToList()).ToList();

        public void PrintMatrix()
        {
            foreach (var l in _matrix)
            {
                foreach (var n in l)
                {
                    if (n == -1) Console.Write('-');
                    else Console.Write(n);
                    Console.Write('\t');
                }
                Console.Write('\n');
            }
        }

        public IEnumerable<int> AdjacencyList(int v) => _matrix.AdjacencyList(v);
       
        public IEnumerable<(int, int)> ListOfEdges()
        {
            var edgesSet = new HashSet<(int, int)>();

            for (int i = 0; i < _matrix.Count; i++)
            {
                for (int j = 0; j < _matrix[i].Count; j++)
                {
                    if (_matrix[i][j] > 0) edgesSet.Add((i, j));
                }
            }

            return edgesSet;
        }

        public IEnumerable<(int, int)> ListOfEdges(int v)
        {
            if (v < 0 || v > _matrix.Count) throw new Exception("Обращение к несуществующей вершине!");

            for (int i = 0; i < _matrix[v].Count; i++)
            {
                if (_matrix[v][i] > 0) yield return (v, i);
            }
        }

        public bool IsDirected()
        {
            for (int i = 0; i < _matrix.Count; i++)
            {
                for (int j = i; j < _matrix.Count; j++)
                {
                    if (_matrix[i][j] != _matrix[j][i]) return true;
                }
            }

            return false;
        }

        public List<List<int>> GetCorrelatedMatrix()
        {
            var result = AdjacencyMatrix();

            if (!IsDirected()) return result;

            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    if(IsEdge(i, j))
                        result[j][i] = result[i][j];
                }
            }

            return result;
        }

    }
}
