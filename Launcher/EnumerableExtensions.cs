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
