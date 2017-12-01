using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzle = SelectDay();
            Console.WriteLine(puzzle.Run());

            Console.ReadLine();
        }

        static IPuzzle SelectDay()
        {
            var type = typeof(IPuzzle);
            var puzzleTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                .OrderBy(t => t.FullName);

            Console.WriteLine($"Select a puzzle by entering a number from 1-{puzzleTypes.Count()}");

            var counter = 0;
            foreach (var puzzleType in puzzleTypes)
            {
                Console.WriteLine($"{counter + 1}) {puzzleType.Name}");
                counter++;
            }
            Console.Write("Enter a puzzle > ");
            var selectedStr = Console.ReadLine();
            var selected = int.Parse(selectedStr) - 1;

            var puzzle = puzzleTypes.ElementAt(selected);

            return (IPuzzle)Activator.CreateInstance(puzzle);
        }
    }
}
