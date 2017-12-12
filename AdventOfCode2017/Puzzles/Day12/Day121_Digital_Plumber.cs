using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day12
{
    public class Day121_Digital_Plumber : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllLines("Puzzles\\Day12\\input_example.txt")
                .Select(ParseLine)
                .ToList();

            var pc = 0;
            while (true)
            {

            }

            return input.Count().ToString();
        }

        Program ParseLine(string line)
        {
            //6 <-> 4, 5
            return new Program
            {
                Id = int.Parse(line.Split(" ").First()),
                ChildIds = line.Remove(0, line.IndexOf(">") + 1).Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
                Children = new List<Program>()
            };

        }
    }

    class Program
    {
        public int Id { get; set; }
        public List<int> ChildIds { get; set; }
        public List<Program> Children { get; set; }
        public int TotalChildren { get { return Children.Count + TotalChildren; } }
    }
}
