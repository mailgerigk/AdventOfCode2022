using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;
internal static class RainbowConsoleText
{
    public static void AnimateLine(string text)
    {
        bool exit = false;
        int offset = 0;
        var cursorTop = Console.CursorTop;
        var cursorLeft = Console.CursorLeft;
        Console.CursorVisible = false;
        while (!exit)
        {
            Console.CursorTop = cursorTop;
            Console.CursorLeft = cursorLeft;

            RainbowWriteLine(text, offset++);

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                exit = key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape;
            }

            Thread.Sleep(50);
        }

        static void RainbowWriteLine(string text, int offset)
        {
            var colors = new ConsoleColor[]
            {
                ConsoleColor.Red,
                ConsoleColor.DarkYellow,
                ConsoleColor.Yellow,
                ConsoleColor.Green,
                ConsoleColor.Cyan,
                ConsoleColor.DarkCyan,
                ConsoleColor.Magenta,
            };

            for (int i = 0; i < text.Length; i++)
            {
                Console.ForegroundColor = colors[(i + offset) % colors.Length];
                Console.Write(text[i]);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
