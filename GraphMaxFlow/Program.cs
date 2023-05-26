using GraphApp.IO;
using GraphApp.Objects;
using GraphMaxFlow.Algoritms;
using System.Net.Http.Headers;
using System.Text;

try
{
    var input = new InputFlagReader(args);
    var output = new Output(args);

    if (input.IsArgsContainsHeader())
    {
        return;
    }

    var graph = new Graph(input.GetFactory());
    var flowChecker = new FlowChecker(graph);
    output.WriteLine($"Max flow from {flowChecker.GetSource()} to {flowChecker.GetSink()}");
    output.WriteLine(flowChecker.FindMaxFlow(out var matrix).ToString());

    var flowSb = new StringBuilder();
    for(int i = 0; i < matrix.Count; i++)
    {
        for(int j = 0; j < matrix.Count; j++)
        {
            if(graph.IsEdge(i, j))
            {
                flowSb.AppendLine($"{i} {j} {matrix[j][i]}/{matrix[j][i] + matrix[i][j]}");
            }
        }
    }

    output.WriteLine(flowSb.ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}