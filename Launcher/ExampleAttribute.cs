namespace Launcher;
[AttributeUsage(AttributeTargets.Method)]
internal class ExampleAttribute : Attribute
{
    public string Input { get; }
    public string Expected { get; }

    public ExampleAttribute(string input, string expected)
    {
        Input = input;
        Expected = expected;
    }
}
