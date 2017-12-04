using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2017.Puzzles.Day3
{
    public class Day31_Spiral_Memory : IPuzzle
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

            while (cVal <= inpt)
            {
                memory[(xPos, yPos)] = cVal++;

                nextTurnAt += tInc;
                tInc += didInc ? 0 : 1;
                didInc = !didInc;
                if (currTurnAt++ == nextTurnAt)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            direction = Direction.Left;
                            lastTurnAt = cVal - 1;
                            break;
                        case Direction.Down:
                            direction = Direction.Right;
                            lastTurnAt = cVal - 1;
                            break;
                        case Direction.Left:
                            direction = Direction.Down;
                            lastTurnAt = cVal - 1;
                            break;
                        case Direction.Right:
                            direction = Direction.Up;
                            lastTurnAt = cVal - 1;
                            break;
                    }
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

            return 0.ToString();
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
