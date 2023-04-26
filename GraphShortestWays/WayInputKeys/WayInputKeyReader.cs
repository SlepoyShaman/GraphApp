namespace GraphShortestWays.WayInputKeys
{
    public class WayInputKeyReader
    {
        private readonly string[] _args;
        public WayInputKeyReader(string[] args)
        {
            _args = args;
        }

        public int GetStartVertex()
        {
            for(int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "-n")
                {
                    return int.Parse(_args[i + 1]);
                }
            }

            return -1;
        }

        public int GetEndVertex()
        {
            for(int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "-d")
                {
                    return int.Parse(_args[i + 1]);
                }
            }

            return -1;
        }
    }
}
