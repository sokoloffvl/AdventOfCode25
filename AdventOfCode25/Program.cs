// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Reflection;
using AdventOfCode25;
using AdventOfCode25.Solvers;

var day = 8;

var dayName = $"Day{day}";
var testFileName = $"Inputs/{dayName}/test.txt";
var fileName =  $"Inputs/{dayName}/input.txt";

var inputTest = File.ReadAllLines(testFileName);
var input = File.ReadAllLines(fileName);
var solverType = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => typeof(IDaySolver).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
    .Select(t => new
    {
        Type = t,
        Attribute = t.GetCustomAttribute<SolverForADay>()
    })
    .FirstOrDefault(x => x.Attribute != null && x.Attribute.Day == day)?.Type;
if (solverType == null)
{
    throw new InvalidOperationException($"No solver found for day {day}.");
}

var solver = (IDaySolver)Activator.CreateInstance(solverType)!;

var timer = Stopwatch.StartNew();
var testSolutionPart1 = solver.SolveP1(inputTest);
var timeTakenToSolveTestP1 = timer.Elapsed;
timer.Restart();
var solutionPart1 = solver.SolveP1(input);
var timeTakenToSolveP1 = timer.Elapsed;
timer.Restart();
var testSolutionPart2 = solver.SolveP2(inputTest);
var timeTakenToSolveTestP2 = timer.Elapsed;
timer.Restart();
var solutionPart2= solver.SolveP2(input);
var timeTakenToSolveP2 = timer.Elapsed;

Console.WriteLine($"Day: {day}");
Console.WriteLine("------------------------------");
Console.WriteLine($"Part 1, Test input.");
Console.WriteLine($"Answer: {testSolutionPart1},  Time: {timeTakenToSolveTestP1}");
Console.WriteLine("------------------------------");
Console.WriteLine($"Part 1, Main input.");
Console.WriteLine($"Answer: {solutionPart1},  Time: {timeTakenToSolveP1}");
Console.WriteLine("------------------------------");
Console.WriteLine($"Part 2, Test input.");
Console.WriteLine($"Answer: {testSolutionPart2},  Time: {timeTakenToSolveTestP2}");
Console.WriteLine("------------------------------");
Console.WriteLine($"Part 2, Main input.");
Console.WriteLine($"Answer: {solutionPart2},  Time: {timeTakenToSolveP2}");