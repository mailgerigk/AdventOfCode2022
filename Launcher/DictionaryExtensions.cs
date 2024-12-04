namespace Launcher;
public static class DictionaryExtensions
{
    public static V? GetOr<K, V>(this Dictionary<K, V> dict, K key, V? elseValue = default)
        where K : notnull
    {
        if (dict.TryGetValue(key, out var value))
        {
            return value;
        }
        return elseValue;
    }
}
