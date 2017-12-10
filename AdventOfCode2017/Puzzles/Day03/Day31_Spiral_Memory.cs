using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2017.Puzzles.Day03
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

            while (cVal < inpt)
            {
                memory[(xPos, yPos)] = cVal++;

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

            var res = Math.Abs(xPos - yPos);

            return res.ToString();
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
