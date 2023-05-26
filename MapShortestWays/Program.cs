using GraphApp.IO;
using System.Drawing;
using MapShortestWays.Algoritms;
using MapShortestWays.Objects;
using System.Text;
using MapShortestWays.WayInputKeys;
using MapShortestWays.IO;

var heuristics = new Func<Point, Point, int>[]
{
    (Point start, Point end) => 0,
    (Point start, Point end) => (int)Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2)),
    (Point start, Point end) => Math.Max(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y)),
    (Point start, Point end) => Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y)
};

var heuristicsNames = new string[]
{
    "Нулевая", "Евклидово расстояние", "Расстояние Чебышева", "Манхэтенское расстояние"
};

try
{
    var input = new MapInputKeyReader(args);
    var output = new Output(args);
    var wayInputKeyReader = new WayInputKeyReader(args);

    if (input.IsArgsContainsHeader())
    {
        return;
    }

    var map = new Map(input.GetFactory());
    var start = wayInputKeyReader.GetStartPoint();
    var end = wayInputKeyReader.GetEndPoint();

    var astar = new Astar(map);
    for (int i = 0; i < heuristics.Length; i++)
    {
        var distance = astar.FindWay(start, end, heuristics[i], out List<Point> way);
        if(distance == -1)
        {
            output.WriteLine("Путь не найден!");
            continue;
        }

        output.WriteLine(distance.ToString());
        output.WriteLine(heuristicsNames[i]);
        output.WriteLine($"Путь от {start} до {end}:");
        var waySb = new StringBuilder();
        var stringWay = way.Select(p => p.ToString());

        foreach(var s in stringWay) {  waySb.Append($"{s} "); }

        output.WriteLine(waySb.ToString());
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}