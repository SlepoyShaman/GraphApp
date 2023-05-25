using DistanceFromGivenVertex.Algorithms;
using GraphApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceFromAllVertexes.Algorithms
{
    public static class Johnson
    {
        public static int[][] AlgJohnson(List<List<int>> _matrix)
        {
            int[][] distances = new int[_matrix.Count][];
            for (int i = 0; i < _matrix.Count; i++)
            {
                distances[i] = new int[_matrix.Count];
            }

            int[] h = new int[_matrix.Count + 1];

            for (int i = 0; i < h.Length; i++) h[i] = int.MaxValue - 5000;
            h[h.Length - 1] = 0;

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

            for (int i = 0; i < _matrix.Count; i++)
            {
                edges.Add(new Tuple<int, int, int>(_matrix.Count, i, 0));
            }

            for (int i = 0; i < _matrix.Count; i++)
            {
                foreach (var e in edges)
                {
                    h[e.Item2] = Math.Min(h[e.Item2], h[e.Item1] + e.Item3);
                }
            }

            foreach (var e in edges)
            {
                if (h[e.Item2] > h[e.Item1] + e.Item3)
                {
                    return new int[][] { };
                }
            }

            for (int i = 0; i < _matrix.Count; i++)
            {
                for (int j = 0; j < _matrix.Count; j++)
                {
                    if (_matrix[i][j] != 0)
                        _matrix[i][j] = _matrix[i][j] + h[j] - h[i];
                }
            }

            // расчитываем расстояние по алгоритму Дейкстры
            for (int i = 0; i < _matrix.Count; i++)
            {
                distances[i] = SearchDistancesFromGivenVertex.Dijkstra(i,_matrix);
            }

            for (int i = 0; i < _matrix.Count; i++)
            {
                for (int j = 0; j < _matrix.Count; j++)
                {
                    distances[i][j] = distances[i][j] + h[i] - h[j];
                }
            }

            return distances;

        }
    }
}
