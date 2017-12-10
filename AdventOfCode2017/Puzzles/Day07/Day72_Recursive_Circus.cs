using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day07
{
    public class Day72_Recursive_Circus : IPuzzle
    {
        public string Run()
        {
            var programs =
                File.ReadAllLines("Puzzles\\Day7\\input.txt")
                .Select(ParseLine)
                .ToList();

            for (var i = 0; i < programs.Count; i++)
            {
                var pbc = programs.Count;
                ResolveChildNames(programs[i], programs);
                if (programs.Count != pbc) i = 0;
            }

            var groups = new List<IEnumerable<IGrouping<int, Program>>>();
            GroupWeights(programs, groups);
            var imbalancedGroup = groups.Where(q => q.Count() != 1).ToList();
            var fg = imbalancedGroup.First();
            var lg = imbalancedGroup.Last();


            return programs.Single().Name;
        }

        void GroupWeights(List<Program> programs, List<IEnumerable<IGrouping<int, Program>>> groups)
        {
            if(programs.Count > 0)
                groups.Add(programs.GroupBy(q => q.WeightSum));

            foreach (var p in programs)
            {
                
                GroupWeights(p.Children, groups);
            }
        }

        void ResolveChildNames(Program program, List<Program> programs)
        {
            foreach (var name in program.ChildNames)
            {
                var p = FindProgramByName(name, programs);
                if (p == null) continue;
                if (p.Moved) continue;

                programs.Remove(p);
                p.Moved = true;
                program.Children.Add(p);
            }

            for (var i = 0; i < program.Children.Count; i++)
            {
                var pbc = program.Children.Count;
                ResolveChildNames(program.Children[i], programs);
                if (program.Children.Count != pbc) i = 0;
            }

        }

        Program FindProgramByName(string name, List<Program> programs)
        {
            foreach (var p in programs)
            {
                if (p.Name == name) return p;
            }

            foreach (var p in programs.Where(q => q.Children.Count > 0))
            {
                return FindProgramByName(name, p.Children);
            }

            return null;
        }

        Program ParseLine(string line)
        {
            var match = Regex.Match(line, "([a-z]+) \\(([0-9]+)\\)");
            if (line.Contains("->"))
            {
                var children = line
                    .Remove(0, line.IndexOf(">") + 1)
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList();

                return new Program
                {
                    Name = match.Groups[1].Value,
                    Weight = int.Parse(match.Groups[2].Value),
                    Children = new List<Program>(),
                    ChildNames = children
                };
            }
            else
            {

                return new Program
                {
                    Name = match.Groups[1].Value,
                    Weight = int.Parse(match.Groups[2].Value),
                    Children = new List<Program>(),
                    ChildNames = new List<string>()
                };

            }

        }
    }

    //[DebuggerDisplay("Name = {Name}, Children = {Children.Count}, WeightSum = {WeightSum}")]
    //class Program
    //{
    //    public string Name { get; set; }
    //    public int Weight { get; set; }
    //    public int WeightSum
    //    {
    //        get
    //        {
    //            return this.Children.Select(q => q.Children).Sum(q => q.Sum(c => c.Weight));
    //        }
    //    }
    //    public bool Moved { get; set; }
    //    public List<Program> Children { get; set; }
    //    public List<string> ChildNames { get; set; }
    //}
}
