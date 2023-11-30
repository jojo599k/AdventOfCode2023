using System;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Advent Of Code 2023");

        if (args.Length == 0)
        {
            Task.Run(async () => await Solver.SolveLast(opt => opt.ClearConsole = false)).Wait();
        }
        else if (args.Length == 1 && args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
        {
            Task.Run(async () => await Solver.SolveAll(opt => 
            {
                opt.ShowConstructorElapsedTime = true;
                opt.ShowTotalElapsedTimePerDay = true;
            })).Wait();
        }
        else
        {
            var indices = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);
            Task.Run(async () => await Solver.Solve(indices.Where(i => i < uint.MaxValue))).Wait();
        }
    }
}
