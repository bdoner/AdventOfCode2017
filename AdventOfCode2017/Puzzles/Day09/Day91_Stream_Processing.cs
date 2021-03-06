using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day09
{
    public class Day91_Stream_Processing : IPuzzle
    {
        public string Run()
        {
            var stream =
                File.ReadAllText("Puzzles\\Day9\\input.txt")
                .ToCharArray();

            var groups = new List<int>();

            bool readingGarbage = false;
            int openGroups = 0;

            for (var i = 0; i < stream.Length; i++)
            {
                char c = stream[i];
                if (c == '!')
                {
                    i++;
                    continue;
                }

                if (readingGarbage && c == '>')
                {
                    readingGarbage = false;
                    continue;
                }
                else if(readingGarbage)
                {
                    continue;
                }

                if(!readingGarbage && c == '<')
                {
                    readingGarbage = true;
                    continue;
                }

                if (c == '{')
                {
                    openGroups++;
                }
                else if(c == '}')
                {
                    groups.Add(openGroups--);
                }
                
            }

            return groups.Sum().ToString();
        }
    }
}
