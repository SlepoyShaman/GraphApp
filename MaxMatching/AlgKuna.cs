using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxMatching
{
    public class Algcuna
    {
        private int leftSize; // Размер левой доли графа
        private int rightSize; // Размер правой доли графа
        private List<List<int>> matrix; // Матрица смежности графа
        private int[] matchL; // Массив для хранения пар вершин
        private int[] matchR; // Массив для хранения пар вершин
        private int[] dist; // Массив для хранения расстояний

        public Algcuna(List<List<int>> adjacencyMatrix)
        {
            matrix = adjacencyMatrix;
            leftSize = adjacencyMatrix.Count;
            rightSize = adjacencyMatrix[0].Count;
            matchL = new int[leftSize];
            matchR = new int[rightSize];
            dist = new int[leftSize];
        }

        // Функция поиска увеличивающей цепи с помощью алгоритма Хопкрофта-Карпа
        private bool BFS()
        {
            Queue<int> queue = new Queue<int>();

            for (int u = 0; u < leftSize; u++)
            {
                if (matchL[u] == -1)
                {
                    dist[u] = 0;
                    queue.Enqueue(u);
                }
                else
                {
                    dist[u] = int.MaxValue;
                }
            }

            dist[-1] = int.MaxValue;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                if (dist[u] < dist[-1])
                {
                    for (int v = 0; v < rightSize; v++)
                    {
                        if (matrix[u][v] == 1)
                        {
                            if (dist[matchR[v]] == int.MaxValue)
                            {
                                dist[matchR[v]] = dist[u] + 1;
                                queue.Enqueue(matchR[v]);
                            }
                        }
                    }
                }
            }

            return (dist[-1] != int.MaxValue);
        }

        // Рекурсивная функция поиска увеличивающей цепи
        private bool DFS(int u)
        {
            if (u != -1)
            {
                for (int v = 0; v < rightSize; v++)
                {
                    if (matrix[u][v] == 1 && dist[matchR[v]] == dist[u] + 1)
                    {
                        if (DFS(matchR[v]))
                        {
                            matchL[u] = v;
                            matchR[v] = u;
                            return true;
                        }
                    }
                }

                dist[u] = int.MaxValue;
                return false;
            }

            return true;
        }

        // Функция поиска максимального паросочетания
        public List<Tuple<int, int>> FindMaxMatching()
        {
            for (int u = 0; u < leftSize; u++)
            {
                matchL[u] = -1;
            }

            for (int v = 0; v < rightSize; v++)
            {
                matchR[v] = -1;
            }

            int matchingCount = 0;

            while (BFS())
            {
                for (int u = 0; u < leftSize; u++)
                {
                    if (matchL[u] == -1 && DFS(u))
                    {
                        matchingCount++;
                    }
                }
            }

            List<Tuple<int, int>> matchingEdges = new List<Tuple<int, int>>();

            for (int u = 0; u < leftSize; u++)
            {
                if (matchL[u] != -1)
                {
                    matchingEdges.Add(new Tuple<int, int>(u, matchL[u]));
                }
            }

            return matchingEdges;
        }
    }
}
