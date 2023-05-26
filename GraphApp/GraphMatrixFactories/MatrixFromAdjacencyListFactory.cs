namespace GraphApp.GraphMatrixFactories
{
    public class MatrixFromAdjacencyListFactory : MatrixFactory
    {
        public MatrixFromAdjacencyListFactory(string filepath) : base(filepath) { }
        
        public override List<List<int>> GetMatrix()
        {
            using (var reader = new StreamReader(_path))
            {
                var lines = reader.ReadToEnd().Split('\n').Where(s => s.Any()).ToArray();
                var result = CreateEmptyMatrix(lines.Length);

                for(int i = 0; i < lines.Length; i++)
                {
                    var numbers = lines[i].Split(' ').Where(s => s.Any()).Select(s => Convert.ToInt32(s)).ToList();
                    for (int j = 0; j < numbers.Count; j++)
                    {
                        result[i][numbers[j] - 1] = 1;
                    }
                }

                return result;
            }
        }
    }
}
