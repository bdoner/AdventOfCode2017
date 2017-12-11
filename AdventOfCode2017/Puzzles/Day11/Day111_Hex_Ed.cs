using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day11
{
    public class Day111_Hex_Ed : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllText("Puzzles\\Day11\\input.txt")
                .Split(",", StringSplitOptions.RemoveEmptyEntries);

            var groups = input
                .GroupBy(q => q)
                .Select(q => new KeyValuePair<string, int>(q.Key, q.Count()))
                .ToDictionary(k => k.Key, v => v.Value);

            var n_s = groups["n"] - groups["s"];
            var ne_sw = groups["ne"] - groups["sw"];
            var se_nw = groups["se"] - groups["nw"];

            //a < 987 & a > 723
            return (n_s + ne_sw + se_nw).ToString();
        }
    }
}
