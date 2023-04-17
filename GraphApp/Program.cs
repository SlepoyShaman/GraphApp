using GraphApp.Algoritms;
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
    var parametrs = new GraphParametrs(graph);

    output.WriteLine("Bектор степеней вершин: ");
    var degrees = parametrs.VertexDegrees();
    if (graph.IsDirected())
    {
        output.WriteLine($"полустепень входа - выхода: {degrees.ConvertToString()}");
    }
    else
    {
        output.WriteLine(degrees.Select(p => p.Item1).ConvertToString());
    }

    output.WriteLine("Матрица расстояний: ");
    foreach (var l in parametrs.GetDistancesMatrix()) { output.WriteLine(l.ConvertToString()); }

    output.WriteLine("Диаметр: ");
    var diametr = parametrs.GetDiametr();
    output.WriteLine(diametr == -1 ? "граф ориентирован или не связан" : diametr.ToString());

    output.WriteLine("Радиус: ");
    var radius = parametrs.GetRadius();
    output.WriteLine(radius == -1 ? "граф ориентирован или не связан" : radius.ToString());

    output.WriteLine("Mножество центральных вершин: ");
    output.WriteLine(parametrs.Centers().ConvertToString());

    output.WriteLine("Mножество периферийных вершин: ");
    output.WriteLine(parametrs.Peripheral().ConvertToString());
}
catch (Exception ex) 
{
    Console.WriteLine(ex.Message);
}