using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day11
{
    public class Day112_Hex_Ed : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllText("Puzzles\\Day11\\input.txt")
                .Split(",", StringSplitOptions.RemoveEmptyEntries);

            var groups = input.GroupBy(q => q).Select(q => (q.Key, q.Count()));

            return 0.ToString();
        }
    }
}
