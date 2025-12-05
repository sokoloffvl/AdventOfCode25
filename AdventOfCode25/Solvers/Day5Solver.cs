namespace AdventOfCode25.Solvers;

[SolverForADay(5)]
public class Day5Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        List<List<long>> ranges = [];
        List<long> ids = [];
        
        var breakLine = input.ToList().IndexOf("");

        for (int i = 0; i < breakLine; i++)
        {
            var line = input[i];
            ranges.Add(line.Split('-').Select(long.Parse).ToList());
        }

        for (int i = breakLine+1; i < input.Length; i++)
        {
            ids.Add(long.Parse(input[i]));
        }
        
        var rangesOrdered = ranges.OrderBy(r => r[0]).ToList();
        var mergedRanges = new List<List<long>>();
        var prev = new List<long> { rangesOrdered[0][0], rangesOrdered[0][1] };
        
        for (var i = 1; i < rangesOrdered.Count; i++)
        {
            var range = rangesOrdered[i];
            if (range[0] <= prev[1])
                prev[1] = Math.Max(prev[1], range[1]);
            else
            {
                mergedRanges.Add(prev);
                prev = [range[0], range[1]];
            }
        }
        
        mergedRanges.Add(prev);

        foreach (var id in ids)
        {
            foreach (var range in mergedRanges)
            {
                if (id >= range[0] && id <= range[1])
                {
                    answer++;
                    break;
                }
            }
        }

        return answer;
    }

    public long SolveP2(string[] input)
    {
        long answer = 0L;
        List<List<long>> ranges = [];
        List<long> ids = [];
        
        var breakLine = input.ToList().IndexOf("");
        
        for (int i = 0; i < breakLine; i++)
        {
            var line = input[i];
            ranges.Add(line.Split('-').Select(long.Parse).ToList());
        }
        
        for (int i = breakLine+1; i < input.Length; i++)
        {
            ids.Add(long.Parse(input[i]));
        }
        
         
        var rangesOrdered = ranges.OrderBy(r => r[0]).ToList();
        var mergedRanges = new List<List<long>>();
        var prev = new List<long> { rangesOrdered[0][0], rangesOrdered[0][1] };
        
        for (var i = 1; i < rangesOrdered.Count; i++)
        {
            var range = rangesOrdered[i];
            if (range[0] <= prev[1])
                prev[1] = Math.Max(prev[1], range[1]);
            else
            {
                mergedRanges.Add(prev);
                prev = [range[0], range[1]];
            }
        }
        
        mergedRanges.Add(prev);

        foreach (var range in mergedRanges)
        {
            answer += range[1] - range[0] + 1;
        }

        return answer;
    }
}