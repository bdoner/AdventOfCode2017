using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day8
{
    public class Day82_I_Heard_You_Like_Registers : IPuzzle
    {
        public string Run()
        {
            var programs =
                File.ReadAllLines("Puzzles\\Day8\\input.txt")
                .Select(ParseLine)
                .ToList();
            

            return 0.ToString() ;
        }
        Instruction ParseLine(string line)
        {
            var match = Regex.Match(line, "([a-z]+) (inc|dec) ([0-9]+) if ([a-z]+) (\\>|\\<|\\>=|==|\\<=|!=) ([0-9]+)");
            return new Instruction
            {
                Register = match.Groups[1].Value,
                Operation = (Operation)Enum.Parse(typeof(Operation), match.Groups[2].Value, true),
                Value = int.Parse(match.Groups[3].Value),
                CompReg = match.Groups[4].Value,
                Comparer = GetComparer(match.Groups[5].Value),
                CompVal = int.Parse(match.Groups[6].Value)
            };
        }

        Comparer GetComparer(string strRep)
        {
            switch (strRep)
            {
                case ">":
                    return Comparer.GreaterThan;
                case "<":
                    return Comparer.LessThank;
                case ">=":
                    return Comparer.GreaterThanOrEqual;
                case "==":
                    return Comparer.Equal;
                case "<=":
                    return Comparer.LessThanOrEqual;
                case "!=":
                    return Comparer.NotEqual;

                default:
                    throw new NotImplementedException(strRep);
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
