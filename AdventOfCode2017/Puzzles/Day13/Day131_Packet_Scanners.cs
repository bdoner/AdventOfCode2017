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
                File.ReadAllLines("Puzzles\\Day13\\input.txt")
                .Select(ParseLine)
                .ToList();


            var layers = new List<FirewallLayer>();
            for(var i = 0; i <= input.Last().Depth; i++)
            {
                layers.Add(input.SingleOrDefault(q => q.Depth == i) ?? new FirewallLayer { Depth = i });
            }
            
            foreach(var layer in layers)
            {
                if (layer.ScannerPos == 0 && layer.Range != 0) layer.CaughtPacket = true;
                layers.ForEach(Update);
            }

            return 
                layers
                .Where(l => l.CaughtPacket)
                .Select(l => l.Depth * l.Range)
                .Sum()
                .ToString();
        }

        private void Update(FirewallLayer layer)
        {
            if (layer.Range == 0) return;

            var newPos = layer.IsScanningDown ? layer.ScannerPos + 1 : layer.ScannerPos - 1;
            layer.ScannerPos = newPos;
            if(newPos == layer.Range - 1)
            {
                layer.IsScanningDown = false;
            }
            else if(newPos == 0)
            {
                layer.IsScanningDown = true;
            }
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

    [DebuggerDisplay("Depth: {Depth}, Range: {Range}, ScannerPos: {ScannerPos}, IsScanningDown: {IsScanningDown}, CaughtPacket: {CaughtPacket}")]
    class FirewallLayer
    {
        public int Depth { get; set; }
        public int Range { get; set; } = 0;
        public int ScannerPos { get; set; } = 0;
        public bool IsScanningDown { get; set; } = true;
        public bool CaughtPacket { get; set; } = false;
    }
}
