using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
public static class EnumerableExtensions
{
    public static int Product(this IEnumerable<int> items)
    {
        var product = 1;
        foreach (var item in items)
        {
            product *= item;
        }
        return product;
    }
}
