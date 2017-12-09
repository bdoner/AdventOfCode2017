using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
    class Program
    {
        static void Main(string[] args)
        {
            var (puzzle, shouldExit) = SelectDay();
            if (shouldExit) Environment.Exit(0);

            Console.WriteLine(puzzle.Run());
            Console.Write("Press enter to select another puzzle...");
            Console.ReadLine();
            Main(args);
        }

        static (IPuzzle, bool) SelectDay()
        {
            Console.Clear();

            var type = typeof(IPuzzle);
            var puzzleTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                .OrderBy(t => t.FullName);

            Console.WriteLine($"Select a puzzle by entering a number from 1-{puzzleTypes.Count()} or 'q' to quit.");

            var counter = 0;
            foreach (var puzzleType in puzzleTypes)
            {
                if (counter % 2 == 0) Console.WriteLine("-------------");

                Console.WriteLine($"{counter + 1, 2}) {puzzleType.FullName.Replace("AdventOfCode2017.Puzzles.", "")}");
                counter++;
            }
            Console.WriteLine("-------------");
            Console.Write("Enter a puzzle > ");
            var selectedStr = Console.ReadLine();
            if (selectedStr == "q") return (null, true);

            int selected = 0;
            if(!int.TryParse(selectedStr, out selected))
            {
                Console.WriteLine("Please enter only integer numbers!");
                Thread.Sleep(3000);
                return SelectDay();
            }
            selected -= 1;
            if(selected < 0 || selected >= puzzleTypes.Count())
            {
                Console.WriteLine("Please enter a number within the given range!");
                Thread.Sleep(3000);
                return SelectDay();
            }

            var puzzle = puzzleTypes.ElementAt(selected);
            return ((IPuzzle)Activator.CreateInstance(puzzle), false);
        }
    }
}
