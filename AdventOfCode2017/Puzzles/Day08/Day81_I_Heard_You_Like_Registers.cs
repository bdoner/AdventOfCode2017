using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day08
{
    public class Day81_I_Heard_You_Like_Registers : IPuzzle
    {
        public string Run()
        {
            var instructions =
                File.ReadAllLines("Puzzles\\Day8\\input.txt")
                .Select(ParseLine)
                .ToList();

            var registers = new Dictionary<string, int>();
            instructions.ForEach(i =>
            {
                if (!registers.ContainsKey(i.Register)) registers.Add(i.Register, 0);
                if (!registers.ContainsKey(i.CompReg)) registers.Add(i.CompReg, 0);
            });
            foreach (var inst in instructions)
            {
                var compRegVal = registers[inst.CompReg];
                bool shouldChange = false;
                switch (inst.Comparer)
                {
                    case Comparer.GreaterThan:
                        shouldChange = compRegVal > inst.CompVal;
                        break;
                    case Comparer.LessThan:
                        shouldChange = compRegVal < inst.CompVal;
                        break;
                    case Comparer.Equal:
                        shouldChange = compRegVal == inst.CompVal;
                        break;
                    case Comparer.LessThanOrEqual:
                        shouldChange = compRegVal <= inst.CompVal;
                        break;
                    case Comparer.GreaterThanOrEqual:
                        shouldChange = compRegVal >= inst.CompVal;
                        break;
                    case Comparer.NotEqual:
                        shouldChange = compRegVal != inst.CompVal;
                        break;
                }

                if (!shouldChange) continue;

                switch (inst.Operation)
                {
                    case Operation.Dec:
                        registers[inst.Register] -= inst.Value;
                        break;
                    case Operation.Inc:
                        registers[inst.Register] += inst.Value;
                        break;
                }
            }

            return registers.OrderByDescending(r => r.Value).First().Value.ToString();
        }


        Instruction ParseLine(string line)
        {
            var match = Regex.Match(line, "([a-z]+) (inc|dec) (\\-?[0-9]+) if ([a-z]+) (\\>|\\<|\\>=|==|\\<=|!=) (\\-?[0-9]+)");
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
                    return Comparer.LessThan;
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

    [DebuggerDisplay("Reg = {Register}, Op = {Operation}, Value = {Value}")]
    class Instruction
    {
        public string Register { get; set; }
        public Operation Operation { get; set; }
        public int Value { get; set; }

        public string CompReg { get; set; }
        public Comparer Comparer { get; set; }
        public int CompVal { get; set; }
    }

    enum Operation
    {
        Dec,
        Inc
    }

    enum Comparer
    {
        GreaterThan,
        LessThan,
        Equal,
        LessThanOrEqual,
        GreaterThanOrEqual,
        NotEqual
    }
}
