using GraphApp.IO;
using GraphApp.Objects;

namespace MaxMatching
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
                int size = graph.AdjacencyMatrix().Count;

                Algcuna alg = new Algcuna(graph.AdjacencyMatrix());
                var maxMatching = alg.FindMaxMatching();

                foreach (var edge in maxMatching)
                {
                    Console.WriteLine($"({edge.Item1 + 1}, {edge.Item2 + 1})");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}