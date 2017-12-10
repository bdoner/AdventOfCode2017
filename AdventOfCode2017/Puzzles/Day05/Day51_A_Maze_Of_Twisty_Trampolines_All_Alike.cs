using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2017.Puzzles.Day05
{
    public class Day51_A_Maze_Of_Twisty_Trampolines_All_Alike : IPuzzle
    {
        public string Run()
        {
            var input = 
                File.ReadAllLines("Puzzles\\Day5\\input.txt")
                .Select(int.Parse)
                .ToArray();

            var steps = 0;
            int ip = 0;
            while(true)
            {
                steps++;
                ip += input[ip]++;
                if (ip >= input.Length) break;
            }

            return steps.ToString();
        }
    }
}
