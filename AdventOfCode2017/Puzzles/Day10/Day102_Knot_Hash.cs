using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day10
{
    public class Day102_Knot_Hash : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllText("Puzzles\\Day10\\input.txt");

            Console.WriteLine("'': " + KnotHash(""));
            Console.WriteLine("'AoC 2017': " + KnotHash("AoC 2017"));
            Console.WriteLine("'1,2,3': " + KnotHash("1,2,3"));
            Console.WriteLine("1,2,4: " + KnotHash("1,2,4"));

            return KnotHash(input);
        }

        private string KnotHash(string data)
        {
            var input =
                data
                .Trim()
                .ToList();

            input.AddRange(new char[] { (char)17, (char)31, (char)73, (char)47, (char)23 });

            var list = new List<int>(256);
            for (var i = 0; i < list.Capacity; i++)
            {
                list.Add(i);
            }

            //list = new List<int> { 0, 1, 2, 3, 4 };

            int position = 0,
                skip = 0;

            for (var its = 0; its < 64; its++)
            {
                foreach (var sub in input)
                {
                    if (sub <= 1)
                    {
                        position += sub + skip++;
                        continue;
                    }

                    var aPos = position % list.Count;

                    var wraps = sub + aPos > list.Count;
                    if (!wraps)
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
            }

            /// Yields 64. as it should
            //list = new List<int>
            //{
            //    65 , 27 , 9 , 1 , 4 , 3 , 40 , 50 , 91 , 7 , 6 , 0 , 2 , 5 , 68 , 22
            //};
            var dIndex = 0;
            var denseHash = new List<int>(16);
            for (var i = 0; i < 16; i++)
            {
                var val = list.Skip(dIndex++ * 16).Take(16).Aggregate((cur, last) => cur ^ last);
                denseHash.Add(val);
            }
            var hashVal = string.Join("", denseHash.Select(i => i.ToString("x2")));

            return hashVal;
        }
    }
}
