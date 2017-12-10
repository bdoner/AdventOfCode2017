using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2017.Puzzles.Day06
{
    public class Day62_Memory_Reallocation : IPuzzle
    {
        public string Run()
        {
            int indx = 0;
            var banks =
                File.ReadAllText("Puzzles\\Day6\\input.txt")
                .Split("\t", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Bank { Index = indx++, Value = int.Parse(x) })
                .ToList();

            //banks = new List<Bank>
            //{
            //    new Bank { Index = 0, Value = 0 },
            //    new Bank { Index = 1, Value = 2 },
            //    new Bank { Index = 2, Value = 7 },
            //    new Bank { Index = 3, Value = 0 }
            //};

            var seen = new List<int[]>();

            while (true)
            {
                if (seen.Any(q => q.ToList().SequenceEqual(banks.Select(s => s.Value))))
                {
                    seen.Add(banks.Select(q => q.Value).ToArray());
                    break;
                }

                seen.Add(banks.Select(q => q.Value).ToArray());

                var bigBank = banks.OrderByDescending(q => q.Value).ThenBy(q => q.Index).First();
                var blocks = bigBank.Value;
                bigBank.Value = 0;
                for (var i = (bigBank.Index + 1) % banks.Count; i < ((bigBank.Index + 1) % banks.Count) + blocks; i++)
                {
                    banks[i % banks.Count].Value += 1;
                }                
            }

            seen.Reverse();
            var toFind = seen.First();
            seen.RemoveAt(0);
            var cycles = seen.FindIndex(0, q => q.SequenceEqual(toFind)) + 1;

            return cycles.ToString();
        }
    }

}
