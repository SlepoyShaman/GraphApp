using GraphApp.Extentions;
using GraphApp.IO;
using GraphApp.Objects;
using GraphPairs.Algoritms;

//try
//{
    var input = new InputFlagReader(args);
    var output = new Output(args);

    if (input.IsArgsContainsHeader())
    {
        return;
    }

    var graph = new Graph(input.GetFactory());
    var pairsChecker = new PairsChecker(graph);

    var result = pairsChecker.FindMaxMatching();
output.WriteLine(result.ConvertToString());

//catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}