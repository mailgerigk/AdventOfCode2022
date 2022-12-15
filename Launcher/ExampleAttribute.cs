using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
