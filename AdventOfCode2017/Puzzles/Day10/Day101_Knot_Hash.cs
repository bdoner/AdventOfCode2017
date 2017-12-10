using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day10
{
    public class Day101_Knot_Hash : IPuzzle
    {
        public string Run()
        {
            var sublists =
                File.ReadAllText("Puzzles\\Day10\\input.txt")
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var list = new List<int>(256);
            for (var i = 0; i < list.Capacity; i++)
            {
                list.Add(i);
            }

            //list = new List<int> { 0, 1, 2, 3, 4 };

            int position = 0,
                skip = 0;

            foreach (var sub in sublists)
            {
                if(sub <= 1)
                {
                    position += sub + skip++;
                    continue;
                }

                var aPos = position % list.Count;

                var wraps = sub + aPos > list.Count;
                if(!wraps)
                {
                    list.Reverse(aPos, sub);
                }
                else
                {
                    
                    var lastPart = (aPos, list.Count - aPos);
                    var firstPart = (0, sub - lastPart.Item2);
                    var subrange = list.GetRange(lastPart.Item1, lastPart.Item2);
                    subrange.AddRange(list.GetRange(firstPart.Item1, firstPart.Item2));
                    subrange.Reverse();

                    list.RemoveRange(lastPart.Item1, lastPart.Item2);
                    list.AddRange(subrange.Take(lastPart.Item2));

                    list.RemoveRange(firstPart.Item1, firstPart.Item2);
                    list.InsertRange(0, subrange.Skip(lastPart.Item2));
                }
                position += sub + skip++;
                
            }

            return (list[0] * list[1]).ToString();
        }
    }
}
