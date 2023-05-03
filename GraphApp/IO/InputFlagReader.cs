using GraphApp.GraphMatrixFactories;

namespace GraphApp.IO
{
    public class InputFlagReader
    {
        private readonly string[] _args;
        private readonly string _header = "Студент: Илюшин Никита Сергеевич\nГруппа: М3О-219Бк-21\n" +
        "Список ключей: " +
        "-e \"edges_list_file_path\" - файл со списком ребер" +
        "\r\n-m \"adjacency_matrix_file_path\" - файл с матрицей смежности" +
        "\r\n-l \"adjacency_list_file_path\" - файл со списком смежности" +
        "\r\n-o \"output_file_path\" - файл для выходных данных";
        private readonly Dictionary<string, Func<string, MatrixFactory>> factories = new()
        {
            ["-e"] = (path) => new MatrixFromEdgeListFactory(path),
            ["-m"] = (path) => new MatrixFromMatrixFactory(path),
            ["-l"] = (path) => new MatrixFromAdjacencyListFactory(path)
        };

        public InputFlagReader(string[] args)
        {
            if (args.Where(f => factories.ContainsKey(f)).Count() > 1)
            {
                throw new ArgumentException("Использованно более одного флага ввода!");
            }

            _args = args;
        }

        public MatrixFactory GetFactory()
        {
            MatrixFactory? factory = null;

            for (int i = 0; i < _args.Length; i++)
            {
                if (factories.ContainsKey(_args[i]))
                {
                    factory = factories[_args[i]](_args[i + 1]);
                }
            }

            if (factory == null) { throw new Exception("Укажите путь к файлу!"); }

            return factory;
        }

        public bool IsArgsContainsHeader()
        {
            if (_args.FirstOrDefault(s => s == "-h") != null)
            {
                Console.WriteLine(_header);
                return true;
            }

            return false;
        }
    }
}
