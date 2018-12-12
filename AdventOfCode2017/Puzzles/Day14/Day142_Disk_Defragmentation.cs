using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2017.Puzzles.Day14
{
    public class Day142_Disk_Defragmentation : IPuzzle
    {
        public string Run()
        {
            var input = "hfdlxzhv";
            Bit[,] grid = new Bit[128, 128];
            int count = 0;

            for (var i = 0; i < 128; i++)
            {
                var hash = Day10.Day102_Knot_Hash.KnotHash($"{input}-{i}");
                for (var j = 0; j < hash.Length; j++)
                {
                    var b = int.Parse(hash[j].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    var bits = Convert.ToString(b, 2).PadLeft(4, '0');
                    for (var k = 0; k < 4; k++)
                    {
                        var used = bits[k] == '1';
                        grid[i, (j * 4) + k] = new Bit(used);
                        if (used)
                        {
                            count++;
                        }

                    }
                }
            }

            var region = 1;
            var lastReg = -1;
            for (var row = 0; row < 128; row++)
            {
                for (var col = 0; col < 128; col++)
                {
                    SetNeighbors(row, col, grid, ref region);

                }
            }

            return lastReg.ToString();
        }

        private void SetNeighbors(int row, int col, Bit[,] grid, ref int region)
        {
            if (grid[row, col].Region != 0 || !grid[row, col].Used) return;

            var neighbors = new List<(int, int)>
            {
                (row, col - 1),
                //(row + 1, col - 1),
                (row + 1, col),
                //(row + 1, col + 1),
                (row, col + 1),
                //(row - 1, col + 1),
                (row - 1, col),
                //(row - 1, col - 1)
            };

            var foundNeighbor = false;
            foreach (var (r, c) in neighbors)
            {
                if (r < 0 || c < 0) continue;
                if (r > 127 || c > 127) continue;

                if (grid[r, c].Region != 0)
                {
                    grid[row, col].Region = grid[r, c].Region;
                    DisplayGrid(grid);
                    foundNeighbor = true;
                    SetNeighbors(r, c, grid, ref region);
                    break;
                }
            }
            if (!foundNeighbor)
            {
                grid[row, col].Region = region++;
                DisplayGrid(grid);
            }

        }

        private void DisplayGrid(Bit[,] grid)
        {
            Console.Clear();
            for (var row = 0; row < 32; row++)
            {
                for (var col = 0; col < 64; col++)
                {
                    if (grid[row, col].Region != 0)
                        Console.Write(grid[row, col].Region);
                    else if (grid[row, col].Used)
                        Console.Write("#");
                    else
                        Console.Write(".");

                }
                Console.WriteLine();
            }

            Thread.Sleep(100);
        }
    }

    class Bit
    {
        public Bit(bool used)
        {
            this.Used = used;
        }

        public bool Used { get; set; }
        public int Region { get; set; }
    }
}
