using DistanceFromGivenVertex.Algorithms;
using GraphApp.IO;
using GraphApp.Objects;
using System.Text;

namespace DistanceFromGivenVertex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var input = new InputFlagReader(args);
                var output = new Output(args);

                if (input.IsArgsContainsHeader())
                {
                    return;
                }

                var graph = new Graph(input.GetFactory());

                int startVertex = int.MaxValue;

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-n") startVertex = int.Parse(args[i + 1]);
                }

                if (startVertex != int.MaxValue)
                {
                    for (int j = 0; j < args.Length; j++)
                    {
                        if (args[j] == "-d")
                        {
                            var dist = SearchDistancesFromGivenVertex.Dijkstra(--startVertex, graph.AdjacencyMatrix());

                            if (dist.Length > 0)
                            {
                                var str = new StringBuilder();
                                str.Append("Shortest paths lengths:\n");
                                for (int i = 0; i < dist.Length; i++)
                                {
                                    if (startVertex != i) 
                                        str.Append($"{startVertex + 1} - {i + 1}: {dist[i]}\n");
                                }
                                output.WriteLine(str.ToString());
                                str.Clear();
                            }
                            else
                            {
                                output.WriteLine("Graph contains a negative cycle.");
                            }
                        }

                        if (args[j] == "-b")
                        {
                            var dist = SearchDistancesFromGivenVertex.BellmanFord(--startVertex, graph.AdjacencyMatrix());

                            if (dist.Length > 0)
                            {
                                var str = new StringBuilder();
                                str.Append("Shortest paths lengths:\n");
                                for (int i = 0; i < dist.Length; i++)
                                {
                                    if (startVertex != i)
                                        str.Append($"{startVertex + 1} - {i + 1}: {dist[i]}\n");
                                }
                                output.WriteLine(str.ToString());
                                str.Clear();
                            }
                            else
                            {
                                output.WriteLine("Graph contains a negative cycle.");
                            }
                        }

                        if (args[j] == "-t")
                        {
                            var dist = SearchDistancesFromGivenVertex.Levit(--startVertex, graph.AdjacencyMatrix());

                            if (dist.Length > 0)
                            {
                                var str = new StringBuilder();
                                str.Append("Shortest paths lengths:\n");
                                for (int i = 0; i < dist.Length; i++)
                                {
                                    if (startVertex != i)
                                        str.Append($"{startVertex + 1} - {i + 1}: {dist[i]}\n");
                                }
                                output.WriteLine(str.ToString());
                                str.Clear();
                            }
                            else
                            {
                                output.WriteLine("Graph contains a negative cycle.");
                            }
                        }
                    }
                }
                else
                {
                    output.WriteLine("Uncorrected start vertex");
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}