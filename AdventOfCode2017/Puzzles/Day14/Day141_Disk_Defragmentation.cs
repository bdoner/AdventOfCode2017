using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day14
{
    public class Day141_Disk_Defragmentation : IPuzzle
    {
        public string Run()
        {
            var input = "hfdlxzhv";
            bool[,] grid = new bool[128, 128];
            int count = 0;

            for (var i = 0; i < 128; i++)
            {
                var hash = Day10.Day102_Knot_Hash.KnotHash($"{input}-{i}");
                for (var j = 0; j < hash.Length; j++)
                {
                    var b = int.Parse(hash[j].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    var bits = Convert.ToString(b, 2).PadLeft(4, '0');
                    for (var k = 0; k < 4; k++)
                    {
                        grid[i, (j * 4) + k] = bits[k] == '1';
                        count += bits[k] == '1' ? 1 : 0;
                    }
                }
            }

            return count.ToString();
        }
    }
}
