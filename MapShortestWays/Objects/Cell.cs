using System.Drawing;

namespace MapShortestWays.Objects
{
    public class Cell
    {
        public Point Position { get; set; }
        public int DistanceFromStart { get; set; }
        public Cell? CameFrom { get; set; }
        public int HeuristicDistanceToEnd { get; set; }
        public int FullDistance
        {
            get => DistanceFromStart + HeuristicDistanceToEnd;
        }

        public Cell(int x, int y)
        {
            Position = new Point(x, y);
        }
    }
}
