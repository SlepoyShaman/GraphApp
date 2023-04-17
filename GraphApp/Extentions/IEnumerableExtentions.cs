using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp.Extentions
{
    public static class IEnumerableExtentions
    {
        public static string ConvertToString(this IEnumerable<int> nums)
            => String.Concat(nums.Select(n => $"{n} "));

        public static string ConvertToString(this IEnumerable<(int, int)> pairs)
            => String.Concat(pairs.Select(p => $"{p.Item1} - {p.Item2} "));
    }
}
