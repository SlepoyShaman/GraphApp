using GraphConnectivity.Сonnectivity;
using GraphApp.Extentions;
using GraphApp.IO;
using GraphApp.Objects;

try
{
    var input = new InputFlagReader(args);
    var output = new Output(args);

    if (input.IsArgsContainsHeader())
    {
        return;
    }

    var graph = new Graph(input.GetFactory());
    if (graph.IsDirected())
    {
        var directedConnectivity = new DirectedGraphСonnectivity(graph);
        bool isWeekConnected = directedConnectivity.IsGraphWeekСonnected();

        output.WriteLine($"Граф {(isWeekConnected ? "слабо связен" : "не связен")}");
        output.WriteLine("Компоненты слабой связности: ");
        foreach (var c in directedConnectivity.GetWeekСonnectivityComponents())
        {
            output.WriteLine(c.ConvertToString());
        }

        var strongConnectedComponents = directedConnectivity.GetStrongСonnectivityComponents();
        bool isStrongConnected = strongConnectedComponents.Count() == 1;
        output.WriteLine($"Граф {(isStrongConnected ? "сильно связен" : "сильно не связен")}");
        output.WriteLine("Компоненты сильной связности: ");
        foreach (var c in strongConnectedComponents)
        {
            output.WriteLine(c.ConvertToString());
        }

    }
    else
    {
        var connectivity = new GraphСonnectivity(graph);
        bool isConnected = connectivity.IsGraphСonnected();
        output.WriteLine($"Граф {(isConnected ? "связен" : "не связен")}");
        foreach (var c in connectivity.GetСonnectivityComponents())
        {
            output.WriteLine(c.ConvertToString());
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}