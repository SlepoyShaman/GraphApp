using GraphApp.Extentions;
using GraphApp.IO;
using GraphApp.Objects;
using GraphShortestWays.Algoritms;
using GraphShortestWays.WayInputKeys;

try
{
    var input = new InputFlagReader(args);
    var output = new Output(args);
    var wayInputKeyReader = new WayInputKeyReader(args);

    if (input.IsArgsContainsHeader())
    {
        return;
    }

    var graph = new Graph(input.GetFactory());

    int start = wayInputKeyReader.GetStartVertex();
    int end = wayInputKeyReader.GetEndVertex();

    if(start == -1 || end == -1)
    {
        output.WriteLine("Необходимо указать начальную и конечную вершину!");
        return;
    }

    var dijkstra = new Dijkstra(graph);

    var shortestWay = dijkstra.FindShortestWay(start, end, out int distance);
    output.WriteLine($"{start} {shortestWay.ConvertToString()}");
    output.WriteLine(distance.ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}