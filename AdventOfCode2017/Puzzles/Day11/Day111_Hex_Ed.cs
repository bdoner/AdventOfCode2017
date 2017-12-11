using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017.Puzzles.Day11
{
    public class Day111_Hex_Ed : IPuzzle
    {
        public string Run()
        {
            var input =
                File.ReadAllText("Puzzles\\Day11\\input.txt")
                .Split(",", StringSplitOptions.RemoveEmptyEntries);

            var coord = new Coordinate() { X = 0, Y = 0, Z = 0 };
            foreach (var dir in input)
            {
                switch (dir)
                {
                    case "n":
                        coord.Y++;
                        coord.Z--;
                        break;
                    case "ne":
                        coord.X++;
                        coord.Z--;
                        break;
                    case "se":
                        coord.Y--;
                        coord.X++;
                        break;
                    case "s":
                        coord.Y--;
                        coord.Z++;
                        break;
                    case "sw":
                        coord.X--;
                        coord.Z++;
                        break;
                    case "nw":
                        coord.X--;
                        coord.Y++;
                        break;

                    default:
                        throw new NotImplementedException(dir);
                }
            }

            return ((Math.Abs(coord.X) + Math.Abs(coord.Y) + Math.Abs(coord.Z)) / 2).ToString();
        }
    }

    struct Coordinate
    {
        public int X, Y, Z;
    }
}
