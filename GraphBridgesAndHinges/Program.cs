using GraphApp.Extentions;
using GraphApp.IO;
using GraphApp.Objects;
using GraphBridgesAndHinges.Algoritms;

try
{
    var input = new InputFlagReader(args);
    var output = new Output(args);

    if (input.IsArgsContainsHeader())
    {
        return;
    }

    var graph = new Graph(input.GetFactory());
    var bridgeAndHingesSeeker = new BridgesAndHingesSeeker(graph);

    output.WriteLine("Мосты в графе: ");
    var bridges = bridgeAndHingesSeeker.GetBridges();
    output.WriteLine(bridges.ConvertToString());

    output.WriteLine("Шарниры в графе: ");
    var hinges = bridgeAndHingesSeeker.GetHinges();
    output.WriteLine(hinges.ConvertToString());

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}