# GraphApp
## Общие параметры входной строки(задание графа)
`-e "edges_list_file_path"` - считывание списка ребер из файла

`-m "adjacency_matrix_file_path"` - считывание матрицы из файла

`-l "adjacency_list_file_path"` - считываение списка смежности из файла

## GraphApp 
Класс `Graph`

Конструктор класса `Graph` принимает объект дочернего от `MatrixFactory` класса. Расширить методы задания графа можно добавлением новой фабрики

`bool IsEdge(int v1, int v2)` - принимает номера
вершин и возвращает True, если в графе есть соответствующее ребро/дуга,
False, если ребра нет

`int Weight(int v1, int v2)` - принимает номера
вершин, возвращает вес ребра, связывающего их

`List<List<int>> AdjacencyMatrix()` - матрица смежности
графа/орграфа

`IEnumerable<int> AdjacencyList(int v)` - список вершин,
смежных вершине v

`IEnumerable<(int, int)> ListOfEdges()` - список всех рёбер графа

`IEnumerable<(int, int)> ListOfEdges(int v)` - список всех рёбер
графа, инцидентных вершине v / исходящих из вершины v

`bool IsDirected()` - True, если граф
ориентированный, False, если граф простой

`List<List<int>> GetCorrelatedMatrix()` - матрица соотнесенного графа
___

Класс `GraphParametrs`

Конструктор класса `GraphParametrs` принимает объект класса `Graph`

`List<List<int>> GetDistancesMatrix()` - матрица расстояний

`IEnumerable<(int, int)> VertexDegrees()` - вектор степеней вершин. (полустепень входа, полустепень выхода).

`int GetRadius()` - радиус графа

`int GetDiametr()` - диаметр графа

`IEnumerable<int> Centers()` - центральные вершины

`IEnumerable<int> Peripheral()` - периферийные вершины

## GraphConnectivity

Класс `DirectedGraphСonnectivity : AbstarctСonnectivity`

Конструктор класса `DirectedGraphСonnectivity` принимает объект класса `Graph`

 `bool IsGraphWeekСonnected()` - проверка слабой связности орграфа
 
 `IEnumerable<IEnumerable<int>> GetWeekСonnectivityComponents()` - компоненты слабой связности
 
 `IEnumerable<IEnumerable<int>> GetStrongСonnectivityComponents()` - компоненты сильной связности
 
 ___
 
 Класс `GraphСonnectivity : AbstarctСonnectivity`
 
 Конструктор класса `GraphСonnectivity` принимает объект класса `Graph`
  
 `bool IsGraphСonnected()` - проверка связности графа
 
 `IEnumerable<IEnumerable<int>> GetСonnectivityComponents()` - компоненты связности графа
