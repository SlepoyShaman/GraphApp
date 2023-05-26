using MapShortestWays.Objects;
using System.Drawing;

namespace MapShortestWays.Algoritms
{
    public class Astar
    {
        private readonly Map _map;
        public Astar(Map map) 
        {
            _map = map;
        }

        public int FindWay(Point start, Point goal, Func<Point, Point, int> heuristic, out List<Point> way)
        {
            var closetList = new List<Cell>();
            var openList = new List<Cell>();

            var startCell = new Cell(start.X, start.Y)
            {
                CameFrom = null,
                DistanceFromStart = 0,
                HeuristicDistanceToEnd = heuristic(start, goal)
            };
            openList.Add(startCell);

            while (openList.Any())
            {
                var currentCell = openList.OrderBy(c => c.FullDistance).First();
                if (currentCell.Position == goal)
                {
                    way = GetWayFromCell(currentCell);
                    return currentCell.DistanceFromStart;
                }
                openList.Remove(currentCell);
                closetList.Add(currentCell);

                foreach (var neighbor in _map.GetNeighbors(currentCell))
                {
                    neighbor.HeuristicDistanceToEnd = heuristic(neighbor.Position, goal);

                    if (closetList.FirstOrDefault(c => c.Position == neighbor.Position) != null)
                        continue;

                    var openCell = openList.FirstOrDefault(c => c.Position == neighbor.Position);

                    if(openCell == null)
                    {
                        openList.Add(neighbor);
                    }
                    else if(openCell.DistanceFromStart > neighbor.DistanceFromStart)
                    {
                        openCell.CameFrom = currentCell;
                        openCell.DistanceFromStart = neighbor.DistanceFromStart;
                    }
                }
            }

            way = new List<Point>();
            return -1;
        }

        private List<Point> GetWayFromCell(Cell cell)
        {
            var result = new List<Point>();
            var currentCell = cell;
            while (currentCell != null)
            {
                result.Add(currentCell.Position);
                currentCell = currentCell.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }
}
