namespace GraphApp.GraphMatrixFactories
{
    public class MatrixFromEdgeListFactory : MatrixFactory
    {
        public MatrixFromEdgeListFactory(string filepath) : base(filepath) { }
        
        public override List<List<int>> GetMatrix()
        {
            using (var reader = new StreamReader(_path))
            {
                var edges = reader.ReadToEnd().Split('\n').Select(
                        s => s.Split(' ').Select(s => Convert.ToInt32(s)).ToArray()
                    );
                int max = edges.Select(e => e.Take(2).Max()).Max();

                var result = CreateEmptyMatrix(max);

                bool haveWeight = false;
                if(edges.First().Count() > 2) haveWeight = true;

                foreach(var a in edges)
                {
                    result[a[0] - 1][a[1] - 1] = haveWeight ? a[2] : 1;
                }

                return result;
            }
        }
    }
}
