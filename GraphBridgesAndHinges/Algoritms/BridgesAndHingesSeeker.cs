using GraphApp.Extentions;
using GraphApp.Objects;

namespace GraphBridgesAndHinges.Algoritms
{
    public class BridgesAndHingesSeeker
    {
        private readonly List<List<int>> _matrix;
        private readonly List<int> _hinges;
        private readonly List<(int v, int u)> _bridges;
        public BridgesAndHingesSeeker(Graph graph)
        {
            _matrix = graph.GetCorrelatedMatrix();
            FindBridgesAndHinges(out _bridges, out _hinges);
        }

        public IEnumerable<(int v, int u)> GetBridges() => _bridges.ToList();

        public IEnumerable<int> GetHinges() => _hinges.ToList();

        private void FindBridgesAndHinges(out List<(int, int)> bridges, out List<int> hinges)
        {
            var components = new DfsComponents(
                new int[_matrix.Count],
                new int[_matrix.Count],
                new bool[_matrix.Count]
            );

            bridges = new List<(int, int)>();
            hinges = new List<int>();
            var AllVertexes = new List<int>();
            for (int i = 0; i < _matrix.Count; i++) { AllVertexes.Add(i); }
            
            while(AllVertexes.Any())
            {
                var oneComponent = BridgesbyDFS(AllVertexes.First(), bridges, hinges, components);

                foreach(var v in oneComponent) { AllVertexes.Remove(v); }
            }
        }

        private IEnumerable<int> BridgesbyDFS(int v, List<(int, int)> bridges, List<int> hinges, DfsComponents components ,int p = -1)
        {
            var vertexes = new List<int>() { v };
            components.Used[v] = true;
            components.Tin[v] = components.Tup[v] = components.Timer++;
            int count = 0;

            var neigbors = _matrix.AdjacencyList(v);
            if(neigbors.Where(v => components.Used[v]).Count() == _matrix.Count) { return vertexes; }
            foreach(var to in neigbors)
            {
                if (to == p) continue;
                if (components.Used[to])
                    components.Tup[v] = Math.Min(components.Tup[v], components.Tin[to]);
                else
                {
                    vertexes.AddRange(BridgesbyDFS(to, bridges, hinges, components, v));
                    count++;
                    components.Tup[v] = Math.Min(components.Tup[v], components.Tup[to]);
                    if (components.Tup[to] >= components.Tin[v])
                    {
                        if(components.Tup[to] != components.Tin[v]) 
                            bridges.Add((v, to));
                        if(p != -1 & !hinges.Contains(v)) 
                            hinges.Add(v);
                    }
                }
            }

            if (p == -1 & count >= 2)
                hinges.Add(v);

            return vertexes;
        }

        private class DfsComponents
        {
            public DfsComponents(int[] tin, int[] tup, bool[] used )
            {
                Tin = tin;
                Tup = tup;
                Used = used;
            }
            public int[] Tin { get; }
            public int[] Tup { get; }
            public bool[] Used { get; }
            public int Timer { get; set; } = 0;
        }
    }
}
