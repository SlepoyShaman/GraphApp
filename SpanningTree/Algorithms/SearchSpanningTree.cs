using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanningTree.Algorithms
{
    public static class SearchSpanningTree
    {
        public static List<List<int>> Kruskal(List<List<int>> _matrix, out int summ)
        {
            int n = _matrix.Count;
            var sets = new int[n];
            int summa = 0;

            for (int i = 0; i < n; i++)
            {
                sets[i] = i;
            }

            // список ребер
            var edges = new List<Tuple<int, int, int>>();

            // матрицу смежности переводим в список ребер
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (_matrix[i][j] != 0)
                    {
                        edges.Add(new Tuple<int, int, int>(i, j, _matrix[i][j]));
                        
                    }
                }
            }

            // сортируем список ребер по весу
            edges.Sort((x, y) => x.Item3.CompareTo(y.Item3));

            var result = new List<List<int>>();

            // бегаем по отсортированному списку ребер и если обе вершины не связны, то добавляем ребро в список
            foreach (var edge in edges)
            {
                int set1 = sets[edge.Item1];
                int set2 = sets[edge.Item2];

                if (set1 != set2)
                {
                    result.Add(new List<int> { edge.Item1 + 1, edge.Item2 + 1, edge.Item3 });
                    summa += edge.Item3;

                    for (int i = 0; i < n; i++)
                    {
                        if (sets[i] == set2)
                        {
                            sets[i] = set1;
                        }
                    }
                }
                
            }

            summ = summa;
            return result;
        }

        public static List<List<int>> Prim(List<List<int>> _matrix, out int summ)
        {
            int summa = 0;
            // Создаем список, который будет содержать все ребра остовного дерева
            var mst = new List<List<int>>();

            // Создаем список для хранения вершин, которые уже включены в остовное дерево
            var visited = new HashSet<int>();

            // Для начала выбираем первую вершину графа в качестве стартовой
            visited.Add(0);

            // Пока не включим в остовное дерево все вершины графа
            while (visited.Count < _matrix.Count)
            {
                // Создаем переменные для хранения минимального веса и соответствующего ему ребра
                int minWeight = int.MaxValue;
                int u = -1;
                int v = -1;

                // Ищем ребро с минимальным весом, которое соединяет вершину из visited с вершиной, не входящей в visited
                foreach (int i in visited)
                {
                    for (int j = 0; j < _matrix[i].Count; j++)
                    {
                        if (!visited.Contains(j) && _matrix[i][j] != 0 && _matrix[i][j] < minWeight)
                        {
                            minWeight = _matrix[i][j];
                            u = i;
                            v = j;
                        }
                    }
                }

                // Добавляем найденное ребро в список остовного дерева
                mst.Add(new List<int>() { u+1, v+1, minWeight });
                summa += minWeight;

                // Добавляем новую вершину в visited
                visited.Add(v);
            }
            summ = summa;
            return mst;
        }


        public static List<List<int>> Boruvka(List<List<int>> _matrix, out int summ)
        {
            int summa = 0;
            int n = _matrix.Count;
            List<List<int>> mst = new List<List<int>>();
            List<int> components = Enumerable.Range(0, n).ToList();
            List<int> minEdge = Enumerable.Repeat(-1, n).ToList();
            List<int> minWeight = Enumerable.Repeat(int.MaxValue, n).ToList();

            int numComponents = n;
            while (numComponents > 1)
            {
                // Step 1: Find the minimum edge for each component
                for (int i = 0; i < n; i++)
                {
                    foreach (int j in Enumerable.Range(0, n).Where(x => _matrix[i][x] > 0 && components[i] != components[x]))
                    {
                        if (_matrix[i][j] < minWeight[components[i]])
                        {
                            minWeight[components[i]] = _matrix[i][j];
                            minEdge[components[i]] = j;
                        }
                    }
                }

                // Step 2: Find the minimum edge that connects two different components
                for (int i = 0; i < n; i++)
                {
                    if (minEdge[i] != -1)
                    {
                        int j = minEdge[i];
                        int temp = i;
                        if (_matrix[i][j] == 0)
                        {
                            int minim = int.MaxValue;
                            int index = 0;
                            for (int k = 0; k < components.Count; k++)
                            {
                                if (components[k] == i)
                                {
                                    if (_matrix[k][j] != 0)
                                    {
                                        if (_matrix[k][j] < minim)
                                        {
                                            minim = _matrix[k][j];
                                            index = k;
                                        }
                                    }
                                        
                                }
                            }
                            i = index;
                        }
                        if (components[i] != components[j])
                        {
                            mst.Add(new List<int> { i+1, j+1, _matrix[i][j]});
                            summa += _matrix[i][j];
                            int oldComponent = components[j];
                            for (int k = 0; k < n; k++)
                            {
                                if (components[k] == oldComponent)
                                {
                                    components[k] = components[i];
                                }
                            }
                            numComponents--;
                        }
                        minEdge[i] = -1;
                        minWeight[i] = int.MaxValue;
                        i = temp;
                    }
                }
            }
            summ = summa;
            return mst;
        }
        







    }
}
