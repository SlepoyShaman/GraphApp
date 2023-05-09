using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp.Extentions
{
    public static class ListExtentions
    {
        public static string ToEdgeListString(this List<List<int>> lst)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var edge in lst)
            {
                sb.Append("(");
                sb.Append(string.Join(",", edge));
                sb.Append("), ");
            }
            sb.Length -= 2; // удаляем последнюю запятую и пробел
            sb.Append("]");
            return sb.ToString();
        }
    }
}
