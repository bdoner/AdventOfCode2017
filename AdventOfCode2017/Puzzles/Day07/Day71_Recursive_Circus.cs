using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day07
{
    public class Day71_Recursive_Circus : IPuzzle
    {
        public string Run()
        {
            var programs =
                File.ReadAllLines("Puzzles\\Day7\\input_example.txt")
                .Select(ParseLine)
                .ToList();

            for (var i = 0; i < programs.Count; i++)
            {
                var pbc = programs.Count;
                ResolveChildNames(programs[i], programs);
                if (programs.Count != pbc) i = 0;
            }


            return programs.Single().Name;
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

    [DebuggerDisplay("Name = {Name}, Children = {Children.Count}, WeightSum = {WeightSum}")]
    class Program
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int WeightSum
        {
            get
            {
                return this.Weight + this.Children.Sum(q => q.WeightSum);
            }
        }
        public bool Moved { get; set; }
        public List<Program> Children { get; set; }
        public List<string> ChildNames { get; set; }
    }
}
