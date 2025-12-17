using System.Drawing;
using Microsoft.Z3;

namespace AdventOfCode25.Solvers;

[SolverForADay(12)]
public class Day12Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        List<int> shapesAreas = [];
        var inputIndex = 0;
        var curCount = 0;
        while (true)
        {
            if (input[inputIndex] == string.Empty)
            {
                shapesAreas.Add(curCount);
                if (shapesAreas.Count == 6)
                    break;
                curCount = 0;
                inputIndex++;
                continue;
            }

            curCount += input[inputIndex].Count(c => c == '#');
            inputIndex++;
        }

        List<Grid> grids = [];

        for (int i = inputIndex+1; i < input.Length; i++)
        {
            var split =  input[i].Split(' ');
            var size = split[0].Remove(split[0].Length-1).Split('x').Select(int.Parse).ToList();
            var counts = split.Skip(1).Select(int.Parse).ToArray();
            grids.Add(new Grid(size[0], size[1], counts));
        }

        foreach (var grid in grids)
        {
            var area = grid.Height *  grid.Width;
            var areaTakenByShapes = grid.Counts.Select((c, i) => c * shapesAreas[i]).Sum();
            if (areaTakenByShapes <= area)
                answer++;
        }


        return answer;
    }
    
    record Grid(int Width, int Height, int[] Counts);

    public long SolveP2(string[] input)
    {
        long answer = 0L;
        
        return answer;
    }
}