using GraphApp.Extentions;
using GraphMaxFlow.Algoritms;

var matrix = new List<List<int>>()
{
    new List<int>{ 0, 1, 1, 0, 0, 0 },
    new List<int>{ 0, 0, 1, 0, 1, 0 },
    new List<int>{ 0, 0, 0, 1, 1, 0 },
    new List<int>{ 0, 0, 0, 0, 0, 1 },
    new List<int>{ 0, 0, 0, 1, 0, 1 },
    new List<int>{ 0, 0, 0, 0, 0, 0 }
};

var a = new FlowChecker();

var r = a.FirstWayToSink(matrix);
foreach(var e in r)
{
    Console.WriteLine(e);
}