using System.Drawing;
using Microsoft.Z3;

namespace AdventOfCode25.Solvers;

[SolverForADay(11)]
public class Day11Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        // Dictionary<string, HashSet<string>> map  = new Dictionary<string, HashSet<string>>();
        // foreach (var line in input)
        // {
        //     var devices =  line.Split(' ');
        //     var firstInput = devices[0].Substring(0, devices[0].Length - 1);
        //     map[firstInput] = devices[1..].ToHashSet();
        // }
        //
        // var visited = new HashSet<string>() {"you"};
        // var uniquePaths = new HashSet<string>();
        // var queue = new Queue<string>();
        // queue.Enqueue("you");
        //
        // while (queue.Count > 0)
        // {
        //     var current = queue.Dequeue();
        //     var node = new String(current.AsSpan()[^3..]);
        //     if (node.Equals("out"))
        //     {
        //         uniquePaths.Add(current);
        //         continue;
        //     }
        //
        //     foreach (var path in map[node])
        //     {
        //         queue.Enqueue(current + path);
        //     }
        //     
        //     visited.Add(node);
        // }
        // return uniquePaths.Count;

        return answer;
    }

    public long SolveP2(string[] input)
    {
        long answer = 0L;
        Dictionary<string, HashSet<string>> map  = new Dictionary<string, HashSet<string>>();
        foreach (var line in input)
        {
            var devices =  line.Split(' ');
            var firstInput = devices[0].Substring(0, devices[0].Length - 1);
            map[firstInput] = devices[1..].ToHashSet();
        }
        
        var memo = new Dictionary<(string node, bool fft, bool dac), long>();

        long Dfs(string node, bool fft, bool dac)
        {
            if (node == "out")
            {
                return fft && dac ? 1 : 0;
            }

            var key = (node, fft, dac);
            if (memo.TryGetValue(key, out var cached))
                return cached;

            long total = 0;

            if (map.TryGetValue(node, out var nextNodes))
            {
                foreach (var nextNode in nextNodes)
                {
                    var nextFft = fft;
                    var nextDac = dac;
                    if (nextNode == "fft")
                        nextFft = true;
                    if (nextNode == "dac")
                        nextDac = true;

                    total += Dfs(nextNode, nextFft, nextDac);
                }
            }

            memo[key] = total;
            return total;
        }

        return Dfs("svr", false, false);
    }
}