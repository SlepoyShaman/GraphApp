using GraphApp.Extentions;
using GraphApp.IO;
using GraphApp.Objects;
using SpanningTree.Algorithms;
using System.Diagnostics;

namespace SpanningTree
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
                
                graph.GetCorrelatedMatrix();

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-k")
                    {
                        int summ = 0;
                        var lst = SearchSpanningTree.Kruskal(graph.AdjacencyMatrix(), out summ);

                        string ans = lst.ToEdgeListString();
                        output.WriteLine(ans);
                        output.WriteLine($"Weight of spanning tree: {summ}");
                        Console.ReadKey();
                    }

                    if (args[i] == "-p")
                    {
                        int summ = 0;
                        var lst = SearchSpanningTree.Prim(graph.AdjacencyMatrix(), out summ);

                        string ans = lst.ToEdgeListString();

                        output.WriteLine(ans);
                        output.WriteLine($"Weight of spanning tree: {summ}");
                        Console.ReadKey();

                    }

                    if (args[i] == "-b")
                    {
                        int summ = 0;
                        var lst = SearchSpanningTree.Boruvka(graph.AdjacencyMatrix(), out summ);

                        string ans = lst.ToEdgeListString();

                        output.WriteLine(ans);
                        output.WriteLine($"Weight of spanning tree: {summ}");
                        Console.ReadKey();
                    }

                    if (args[i] == "-s")
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        int summ = 0;
                        stopwatch.Start();
                        var lst = SearchSpanningTree.Kruskal(graph.AdjacencyMatrix(), out summ);
                        stopwatch.Stop();
                        string ans = lst.ToEdgeListString();
                        output.WriteLine(ans);
                        output.WriteLine($"Weight of spanning tree: {summ}");
                        output.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");
                        output.WriteLine("\n");

                        stopwatch.Reset();
                        stopwatch.Start();
                        lst = SearchSpanningTree.Prim(graph.AdjacencyMatrix(), out summ);
                        stopwatch.Stop();
                        ans = lst.ToEdgeListString();
                        output.WriteLine(ans);
                        output.WriteLine($"Weight of spanning tree: {summ}");
                        output.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");
                        output.WriteLine("\n");

                        stopwatch.Reset();
                        stopwatch.Start();
                        lst = SearchSpanningTree.Boruvka(graph.AdjacencyMatrix(), out summ);
                        stopwatch.Stop();
                        ans = lst.ToEdgeListString();
                        output.WriteLine(ans);
                        output.WriteLine($"Weight of spanning tree: {summ}");
                        output.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}ms");

                        Console.ReadKey();
                    }
                }

            }
			catch (Exception ex)
			{

                Console.WriteLine(ex.Message);
            }
        }
    }
}