using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day13
{
    public class Day132_Packet_Scanners : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllLines("Puzzles\\Day13\\input.txt")
                .Select(ParseLine)
                .ToList();


            var delay = 0;
            var passed = true;
            do
            {
                var layers = ResetLayers(input);
                passed = true;
                for (var i = 0; i < delay; i++) layers.ForEach(Update);

                foreach (var layer in layers)
                {
                    if (layer.ScannerPos == 0 && layer.Range != 0)
                    {
                        delay++;
                        passed = false;
                        break;
                    }
                    layers.ForEach(Update);
                }
            } while (!passed);


            return delay.ToString();
        }

        private List<FirewallLayer> ResetLayers(List<FirewallLayer> input)
        {
            var layers = new List<FirewallLayer>();
            for (var i = 0; i <= input.Last().Depth; i++)
            {
                var l = input.SingleOrDefault(q => q.Depth == i);
                if (l != null)
                {
                    l.CaughtPacket = false;
                    l.Depth = l.Depth;
                    l.IsScanningDown = true;
                    l.Range = l.Range;
                    l.ScannerPos = 0;
                }

                layers.Add(l ?? new FirewallLayer { Depth = i });
            }
            return layers;
        }

        private void Update(FirewallLayer layer)
        {
            if (layer.Range == 0) return;

            var newPos = layer.IsScanningDown ? layer.ScannerPos + 1 : layer.ScannerPos - 1;
            layer.ScannerPos = newPos;
            if (newPos == layer.Range - 1)
            {
                layer.IsScanningDown = false;
            }
            else if (newPos == 0)
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
}
