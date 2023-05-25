using DistanceFromAllVertexes.Algorithms;
using GraphApp.IO;
using GraphApp.Objects;
using System.Diagnostics;
using System.Text;

namespace DistanceFromAllVertexes
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

                var dst = Johnson.AlgJohnson(graph.AdjacencyMatrix());

                if (dst.Length== 0)
                {
                    output.WriteLine("Graph contains a negative cycle.");
                }
                else
                {
                    var str = new StringBuilder();
                    str.Append("Shortest paths lengths:\n");

                    for (int i = 0; i < dst.Length; i++)
                    {
                        for (int j = 0; j < dst[i].Length; j++)
                        {
                            if (dst[i][j] != 0 && dst[i][j] < 100000 && dst[i][j] > -100000)
                            {
                                str.Append($"{i + 1} - {j + 1}: {dst[i][j]}\n");
                            }
                        }
                    }
                    output.WriteLine(str.ToString());
                    str.Clear();
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}