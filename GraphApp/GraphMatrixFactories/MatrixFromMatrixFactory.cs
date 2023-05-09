namespace GraphApp.GraphMatrixFactories
{
    public class MatrixFromMatrixFactory : MatrixFactory
    {
        public MatrixFromMatrixFactory(string filepath) : base(filepath) { }
        public override List<List<int>> GetMatrix()
        {
            var result = new List<List<int>>();
            using (var reader = new StreamReader(_path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(
                            line.Split(' ').Where(s => s.Any()).Select(s => Convert.ToInt32(s)).ToList()
                        );
                }
            }
            return result;
        }
    }
}
