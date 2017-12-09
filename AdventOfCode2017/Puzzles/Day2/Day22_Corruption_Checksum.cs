using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2017.Puzzles.Day2
{
    public class Day22_Corruption_Checksum : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllLines("Puzzles\\Day2\\input.txt")
                .Select(line =>
                    line.Split("\t", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                )
                .ToArray();

            int sum = 0;
            foreach (var line in input)
            {
                for (var i = 0; i < line.Count(); i++)
                {
                    for (var j = 0; j < line.Count(); j++)
                    {
                        if (i == j) continue;

                        if (line[i] % line[j] == 0)
                        {
                            sum += line[i] / line[j];
                        }
                    }
                }
            }

            return sum.ToString();
        }
    }
}