using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day13
{
    public class Day131_Packet_Scanners : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllLines("Puzzles\\Day13\\input_example.txt")
                .Select(ParseLine)
                .ToList();

            input.First().GetPosAt(0); // 0

            input.First().GetPosAt(1); // 1
            input.First().GetPosAt(2); // 2

            input.First().GetPosAt(3); // 1
            input.First().GetPosAt(4); // 0

            input.First().GetPosAt(5); // 1
            input.First().GetPosAt(6); // 2

            var layers = new List<FirewallLayer>();
            for (var i = 0; i <= input.Last().Depth; i++)
            {
                layers.Add(input.SingleOrDefault(q => q.Depth == i) ?? new FirewallLayer { Depth = i });
            }

            int picoSeconds = 0;
            foreach (var layer in layers)
            {
                if (layer.GetPosAt(picoSeconds++) == 0 && layer.Range != 0) layer.CaughtPacket = true;
                //layers.ForEach(Update);
            }

            return
                layers
                .Where(l => l.CaughtPacket)
                .Select(l => l.Depth * l.Range)
                .Sum()
                .ToString();
        }

        FirewallLayer ParseLine(string line)
        {
            return new FirewallLayer
            {
                Depth = int.Parse(line.Split(":").First()),
                Range = int.Parse(line.Split(":").Last())
            };

        }
    }

    [DebuggerDisplay("Depth: {Depth}, Range: {Range}, CaughtPacket: {CaughtPacket}")]
    class FirewallLayer
    {
        public int Depth { get; set; }
        public int Range { get; set; } = 0;
        public bool CaughtPacket { get; set; } = false;

        /// <summary>
        /// t = timer
        /// R = 3
        /// p = t % R
        ///  _
        /// |_| 0 4   8
        /// |_| 1 3 5 7
        /// |_| 2   6
        /// 
        /// </summary>
        /// <param name="picoSecond"></param>
        /// <returns></returns>
        public int GetPosAt(int picoSecond)
        {
            if (this.Range == 0) return 0;

            var p = picoSecond % 4 == 0 ? 0 : (this.Range - 1) - picoSecond % (this.Range - 1);
            return p;
            return (int)Math.Floor((double)picoSecond / (double)this.Range - 1) % 2 == 0 ? p : (this.Range - 1) - p;
        }
    }
}
