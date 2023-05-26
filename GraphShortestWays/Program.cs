using GraphApp.Extentions;
using GraphApp.IO;
using GraphApp.Objects;
using GraphShortestWays.Algoritms;
using GraphShortestWays.WayInputKeys;
using System.Text;

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

    var shortestWay = dijkstra.FindShortestWay(start, end, out int distance).ToArray();
    var waySb = new StringBuilder($"({start}, {shortestWay[0]}, {graph.Weight(start, shortestWay[0])}) ");
    for(int i = 0; i < shortestWay.Length - 1; i++)
    {
        waySb.Append($"({shortestWay[i]}, {shortestWay[i+1]}, {graph.Weight(shortestWay[i], shortestWay[i + 1])}) ");
    }
    output.WriteLine(waySb.ToString());
    output.WriteLine(distance.ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}