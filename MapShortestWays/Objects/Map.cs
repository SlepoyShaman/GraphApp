using GraphApp.GraphMatrixFactories;
using System.Drawing;

namespace MapShortestWays.Objects
{
    public class Map
    {
        private readonly int _maxHeight = 1024;
        private readonly List<List<int>> _fieldMatrix;
        public Map(MatrixFromMatrixFactory factory) 
        {
            _fieldMatrix = factory.GetMatrix();
        }

        public int this[int x, int y]
        {
            get => _fieldMatrix[x][y];
        }

        public List<Cell> GetNeighbors(Cell cell)
        {
            var result = new List<Cell>();

            var points = new Point[]
            {
                new Point(cell.Position.X - 1, cell.Position.Y),
                new Point(cell.Position.X, cell.Position.Y - 1),
                new Point(cell.Position.X + 1, cell.Position.Y),
                new Point(cell.Position.X, cell.Position.Y + 1)
            };

            foreach (var point in points ) 
            {
                if(point.X < 0 || point.Y < 0) 
                    continue;
                if(point.X >= _fieldMatrix.Count || point.Y >= _fieldMatrix.Count)
                    continue;
                if (this[point.X, point.Y] == _maxHeight)
                    continue;

                var neighbor = new Cell(point.X, point.Y)
                {
                    CameFrom = cell,
                    DistanceFromStart = cell.DistanceFromStart + 1 
                        + Math.Abs(this[point.X, point.Y] - this[cell.Position.X, cell.Position.Y]),
                };

                result.Add(neighbor);
            }

            return result;
        }
    }
}
