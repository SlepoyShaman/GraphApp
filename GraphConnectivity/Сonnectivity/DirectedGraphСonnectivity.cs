using GraphApp.Extentions;
using GraphApp.Objects;

namespace GraphConnectivity.Сonnectivity
{
    public class DirectedGraphСonnectivity : AbstarctСonnectivity
    {
        private readonly List<List<int>> _correlatedMatrix;
        public DirectedGraphСonnectivity(Graph graph) : base(graph) 
        {
            _correlatedMatrix = graph.GetCorrelatedMatrix();
        }

        public bool IsGraphWeekСonnected()
            => IsMatrixСonnected(_correlatedMatrix);

        public IEnumerable<IEnumerable<int>> GetWeekСonnectivityComponents()
            => GetСonnectivityComponentsByMatrix(_correlatedMatrix);
        
        public IEnumerable<IEnumerable<int>> GetStrongСonnectivityComponents()
        {
            var result = new List<IEnumerable<int>>();
            var counters = new int[_graphMatrix.Count];
            var updatedCounters = new int[_graphMatrix.Count];
            var allVertexes = new List<int>();
            var reversiveGraphMatrix = _graphMatrix.Transpose();
            int counter = 0;

            for (int i = 0; i < _graphMatrix.Count; i++) { allVertexes.Add(i); };
            
            while(allVertexes.Any())
            {
                var dfsResult = DFSWithCounters(allVertexes.First(), ref counter, counters, _graphMatrix);
                foreach (var v in dfsResult) { allVertexes.Remove(v); }
            }

            for (int i = 0; i < _graphMatrix.Count; i++) { allVertexes.Add(i); };
            counter = 0;

            while(allVertexes.Any())
            {
                var vertexInOneComponent = DFSWithCounters(counters.MaxValueIndex(), ref counter, updatedCounters, reversiveGraphMatrix);
                result.Add(vertexInOneComponent);
                foreach(var v in vertexInOneComponent)
                {
                    allVertexes.Remove(v);
                    counters[v] = 0;
                }
            }

            return result;
        }
    }
}
