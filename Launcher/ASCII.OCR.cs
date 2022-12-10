using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher;

public static class ASCII
{
	// table from https://github.com/OliverFlecke/advent-of-code-rust/blob/main/advent-of-code-ocr/src/lib.rs
	static readonly Dictionary<string, char> stringToChar = new()
		{
			{".##.\n#..#\n#..#\n####\n#..#\n#..#", 'A'},
			{"###.\n#..#\n###.\n#..#\n#..#\n###.", 'B'},
			{".##.\n#..#\n#...\n#...\n#..#\n.##.", 'C'},
			{"####\n#...\n###.\n#...\n#...\n####", 'E'},
			{"####\n#...\n###.\n#...\n#...\n#...", 'F'},
			{".##.\n#..#\n#...\n#.##\n#..#\n.###", 'G'},
			{"#..#\n#..#\n####\n#..#\n#..#\n#..#", 'H'},
			{".###\n..#.\n..#.\n..#.\n..#.\n.###", 'I'},
			{"..##\n...#\n...#\n...#\n#..#\n.##.", 'J'},
			{"#..#\n#.#.\n##..\n#.#.\n#.#.\n#..#", 'K'},
			{"#...\n#...\n#...\n#...\n#...\n####", 'L'},
			{".##.\n#..#\n#..#\n#..#\n#..#\n.##.", 'O'},
			{"###.\n#..#\n#..#\n###.\n#...\n#...", 'P'},
			{"###.\n#..#\n#..#\n###.\n#.#.\n#..#", 'R'},
			{".###\n#...\n#...\n.##.\n...#\n###.", 'S'},
			{"#..#\n#..#\n#..#\n#..#\n#..#\n.##.", 'U'},
			{"#...\n#...\n.#.#\n..#.\n..#.\n..#.", 'Y'},
			{"####\n...#\n..#.\n.#..\n#...\n####", 'Z' }
		};

	public static string Parse(string input) => Parse(input.Split());

	public static string Parse(IEnumerable<string> input)
	{
		var length = input.First().Length;
		var index = 0;
		var chars = new List<char>();
		while (index < length)
		{
			var s = string.Join('\n', input.Select(s => new string( s.Skip(index).Take(4).ToArray())));
			chars.Add(stringToChar[s]);
			// 4 from chars + 1 space
			index += 5;
		}
		return new(chars.ToArray());
	}
}
