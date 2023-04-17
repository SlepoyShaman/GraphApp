namespace GraphApp.GraphMatrixFactories
{
    public abstract class MatrixFactory
    {
        protected readonly string _path;
        public MatrixFactory(string filename)
        {
            _path = $"Files\\{filename}";
        }

        public abstract List<List<int>> GetMatrix(); 

        protected static List<List<int>> CreateEmptyMatrix(int size)
        {
            var result = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                var list = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    list.Add(0);
                }

                result.Add(list);
            }

            return result;
        }
    }
}
