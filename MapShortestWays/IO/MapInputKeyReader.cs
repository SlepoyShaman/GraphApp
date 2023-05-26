using GraphApp.GraphMatrixFactories;

namespace MapShortestWays.IO
{
    public class MapInputKeyReader
    {
        private readonly string[] _args;
        private readonly string _header = "Студенты: Илюшин Никита Сергеевич, Таласов Данил Сергеевич\nГруппа: М3О-219Бк-21\n" +
        "Список ключей: " +
        "-e \"edges_list_file_path\" - файл со списком ребер" +
        "\r\n-m \"adjacency_matrix_file_path\" - файл с матрицей смежности" +
        "\r\n-l \"adjacency_list_file_path\" - файл со списком смежности" +
        "\r\n-o \"output_file_path\" - файл для выходных данных";

        public MapInputKeyReader(string[] args)
        {
            _args = args;
        }

        public MatrixFromMatrixFactory GetFactory()
        {
            MatrixFromMatrixFactory? factory = null;

            for (int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "-m")
                {
                    factory = new MatrixFromMatrixFactory(_args[i+1]);
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
