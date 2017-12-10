using System.IO;
using System;
using System.Linq;

namespace AdventOfCode2017.Puzzles.Day02
{
    public class Day21_Corruption_Checksum : IPuzzle
    {
        public string Run()
        {
            var input = 
                File.ReadAllLines("Puzzles\\Day2\\input.txt")
                .Select(line => 
                    line.Split("\t", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                )
                .ToArray();

            int sum = 0;
            foreach(var line in input)
            {
                var min = line.Min();
                var max = line.Max();

                var diff = max - min;
                sum += diff;
            }

            
            return sum.ToString();
        }
    }
}