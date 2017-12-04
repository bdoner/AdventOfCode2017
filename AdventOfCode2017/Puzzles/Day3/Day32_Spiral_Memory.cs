using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2017.Puzzles.Day3
{
    public class Day32_Spiral_Memory : IPuzzle
    {
        public string Run()
        {
            var memory = new Dictionary<(int, int), int>();
            int
                inpt = 368078,
                xPos = 0,
                yPos = 0,
                tInc = 0,
                cVal = 1,

                currTurnAt = 0,
                lastTurnAt = 0,
                nextTurnAt = 1;

            bool didInc = true;
            var direction = Direction.Right;

            while (cVal < inpt)
            {
                memory[(xPos, yPos)] = GetSurroundingSum((xPos, yPos), memory);// cVal++;
                cVal++;

                if(memory[(xPos, yPos)] > inpt) break;

                if (currTurnAt++ == nextTurnAt)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            direction = Direction.Left;
                            break;
                        case Direction.Down:
                            direction = Direction.Right;
                            break;
                        case Direction.Left:
                            direction = Direction.Down;
                            break;
                        case Direction.Right:
                            direction = Direction.Up;
                            break;
                    }
                    lastTurnAt = cVal - 1;
                    tInc += didInc ? 0 : 1;
                    didInc = !didInc;
                    nextTurnAt = currTurnAt + tInc;
                }

                switch (direction)
                {
                    case Direction.Up:
                        yPos--;
                        break;
                    case Direction.Down:
                        yPos++;
                        break;
                    case Direction.Left:
                        xPos--;
                        break;
                    case Direction.Right:
                        xPos++;
                        break;
                }
            }

            var res = memory[(xPos, yPos)];

            return res.ToString();
        }

        int GetSurroundingSum((int, int) pos, Dictionary<(int, int), int> memory)
        {

            if(pos.Item1 == 0 && pos.Item2 == 0) return 1;

            var (x, y) = pos;
            var surroundingPositions = new List<(int, int)>()
            {
                (x, y - 1),
                (x + 1, y - 1),
                (x + 1, y),
                (x + 1, y + 1),
                (x, y + 1),
                (x - 1, y + 1),
                (x - 1, y),
                (x - 1, y - 1)
            };

            var sum = 0;
            foreach (var p in surroundingPositions)
            {
                if (memory.ContainsKey(p))
                    sum += memory[p];
            }

            return sum;
        }
    }


    // private enum Direction
    // {
    //     Up,
    //     Down,
    //     Left,
    //     Right
    // }
}
