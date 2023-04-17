using GraphApp.Objects;

namespace GraphApp.Algoritms.Сonnectivity
{
    public class GraphСonnectivity : AbstarctСonnectivity
    {
        public GraphСonnectivity(Graph graph) : base(graph) 
        {
            if (graph.IsDirected())
            { throw new ArgumentException("Graph is Directed. Use DirectedGraphСonnectivity"); }
        }

        public bool IsGraphСonnected()
            => IsMatrixСonnected(_graphMatrix);

        public IEnumerable<IEnumerable<int>> GetСonnectivityComponents()
            => GetСonnectivityComponentsByMatrix(_graphMatrix);
    }
}
