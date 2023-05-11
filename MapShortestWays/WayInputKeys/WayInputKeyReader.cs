

using System.Drawing;

namespace MapShortestWays.WayInputKeys
{
    public class WayInputKeyReader
    {
        private readonly string[] _args;
        public WayInputKeyReader(string[] args)
        {
            _args = args;
        }

        public Point GetStartPoint()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "-n")
                {
                    return new Point(int.Parse(_args[i + 1]), int.Parse(_args[i + 2]));
                }
            }

            return new Point(-1, -1);
        }

        public Point GetEndPoint()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "-d")
                {
                    return new Point(int.Parse(_args[i + 1]), int.Parse(_args[i + 2]));
                }
            }

            return new Point(-1, -1);
        }
    }
}
