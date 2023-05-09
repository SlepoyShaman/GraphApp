using GraphApp.IO;
using GraphApp.Objects;
using GraphMaxFlow.Algoritms;

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

    output.WriteLine(flowChecker.FindMaxFlow().ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}