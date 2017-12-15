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
            var programs =
                File.ReadAllLines("Puzzles\\Day12\\input_example.txt")
                .Select(ParseLine)
                .ToList();

            var pc = 0;
            for (var i = 0; i < programs.Count; i++)
            {
                var p = programs[i];
                if(p.Id == 0)
                {
                    pc++;
                    programs[i].Connected = true;
                    continue;
                }
                var prev = programs.TakeWhile(q => q.Id != p.Id).Where(q => q.Connected).ToList();
                if(!p.Connected && prev.SelectMany(q => q.ChildIds).Any(q => q == p.Id))
                {
                    pc++;
                    programs[i].Connected = true;
                    i = 0;
                }
            }

            return pc.ToString();
        }

        Prog ParseLine(string line)
        {
            //6 <-> 4, 5
            return new Prog
            {
                Id = int.Parse(line.Split(" ").First()),
                ChildIds = line.Remove(0, line.IndexOf(">") + 1).Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList(),
                Connected = false
            };

        }
    }

    [DebuggerDisplay("Id: {Id}, ChildIds: {ChildIds.Count}, Connected: {Connected}")]
    class Prog
    {
        public int Id { get; set; }
        public List<int> ChildIds { get; set; }
        public bool Connected { get; set; }
    }
}
