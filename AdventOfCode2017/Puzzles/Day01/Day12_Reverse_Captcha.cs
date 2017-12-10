using System.IO;
using System.Linq;

namespace AdventOfCode2017.Puzzles.Day01
{
    class Day12_Reverse_Captcha : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllText("Puzzles\\Day1\\input.txt")
                .ToList<char>()
                .Select(p => p.ToString())
                .Select(int.Parse)
                .ToArray();

            int sum = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + (input.Length / 2)) % input.Length])
                {
                    sum += input[i];
                }
            }

            return sum.ToString();
        }
    }
}
