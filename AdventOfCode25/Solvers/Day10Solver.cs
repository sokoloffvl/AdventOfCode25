using System.Drawing;
using Microsoft.Z3;

namespace AdventOfCode25.Solvers;

[SolverForADay(10)]
public class Day10Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        
        var inputs = new List<Input>();

        foreach (var line in input)
        {
            var brackets =  line.Split(' ');
            var endGoal = brackets[0].Substring(1, brackets[0].Length - 2).Select(c => c != '.').ToList();
            var lightsEndGoal = BoolListToInt(endGoal);
            var voltages = brackets[^1].Substring(1, brackets[^1].Length - 2).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var buttons = brackets[1..^1].Select(
                g => FromIndexesMSB(g.Substring(1, g.Length - 2).Split(',').Select(int.Parse).ToList(), endGoal.Count)).ToList();
            
             inputs.Add(new Input(lightsEndGoal, buttons, voltages));
        }

        foreach (var challenge in inputs)
        {
            answer += SolveInputP1(challenge);
        }

        return answer;
    }

    private int SolveInputP1(Input input)
    {
        var visited = new Dictionary<int, int>();
        visited[0] = 0;
        var queue = new Queue<int>();
        queue.Enqueue(0);
            

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentDist = visited[current];
            foreach (var button in input.Buttons)
            {
                var next = current ^ button;

                // If out of bounds, skip (defensive)
                if (next < 0) continue;

                if (!visited.ContainsKey(next)) // not visited
                {
                    visited[next] = currentDist + 1;

                    if (next == input.LightsEndGoal)
                        return visited[next];

                    queue.Enqueue(next);
                }
            }
        }

        return 0;
    }

    public long SolveP2(string[] input)
    {
        long answer = 0L;
        
        var inputs = new List<InputP2>();

        foreach (var line in input)
        {
            var brackets =  line.Split(' ');
            var voltages = brackets[^1].Substring(1, brackets[^1].Length - 2).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var buttons = brackets[1..^1].Select(
                g => g.Substring(1, g.Length - 2).Split(',').Select(int.Parse).ToList()).ToList();
            
            inputs.Add(new InputP2(buttons, voltages));
        }

        for (var index = 0; index < inputs.Count; index++)
        {
            var challenge = inputs[index];
            answer += SolveWithZ3(challenge.Voltages.ToArray(), challenge.Buttons.Select(s => s.ToArray()).ToList());
        }

        return answer;
    }
    
    public static int SolveWithZ3(int[] goal, List<int[]> moves)
    {
        using var ctx = new Context();

        var nDigits = goal.Length;
        var nButtons = moves.Count;

        var x = new IntExpr[nButtons];
        for (int j = 0; j < nButtons; j++)
        {
            x[j] = ctx.MkIntConst($"x_{j}");
        }

        using var opt = ctx.MkOptimize();

        // 1) Non-negativity constraints: x_j >= 0
        foreach (var v in x)
        {
            opt.Assert(ctx.MkGe(v, ctx.MkInt(0)));
        }

        for (int i = 0; i < nDigits; i++)
        {
            var touchingButtons = new List<IntExpr>();

            for (int j = 0; j < nButtons; j++)
            {
                if (moves[j].Contains(i))
                {
                    touchingButtons.Add(x[j]);
                }
            }
            
            ArithExpr sum = touchingButtons.Count == 1
                ? (ArithExpr)touchingButtons[0]
                : ctx.MkAdd(touchingButtons.ToArray());

            opt.Assert(ctx.MkEq(sum, ctx.MkInt(goal[i])));
        }

        // minimize total button presses Σ x_j
        ArithExpr totalPresses = ctx.MkAdd(x);
        opt.MkMinimize(totalPresses);

        // Check and extract model
        if (opt.Check() != Status.SATISFIABLE && opt.Check() != Status.UNKNOWN)
        {
            Console.WriteLine("No solution (UNSAT).");
            return -1;
        }

        var model = opt.Model;

        int total = 0;
        for (int j = 0; j < nButtons; j++)
        {
            int presses = ((IntNum)model.Evaluate(x[j])).Int;
            total += presses;
        }

        return total;
    }
    
    private int SolveInputP2(InputP2 input)
    {
        var start = input.Voltages.Select(v => 0).ToList();
        var startKey = ListToString(start);

        var visited = new HashSet<string>();
        visited.Add(startKey);

        var queue = new Queue<(List<int> state, int dist)>();
        queue.Enqueue((start, 0));

        var goal = input.Voltages;

        while (queue.Count > 0)
        {
            var (current, currentDist) = queue.Dequeue();

            foreach (var button in input.Buttons)
            {
                var next = ApplyMove(current, button);

                if (IsOOB(next, goal))
                    continue;

                var nextKey = ListToString(next);

                if (!visited.Contains(nextKey))
                {
                    if (IsGoal(next, goal))
                        return currentDist + 1;

                    visited.Add(nextKey);
                    queue.Enqueue((next, currentDist + 1));
                }
            }
        }

        return 0;
    }
    
    private bool IsGoal(List<int> state, List<int> goal)
    {
        if (state.Count != goal.Count) return false;
        for (int i = 0; i < state.Count; i++)
            if (state[i] != goal[i]) return false;
        return true;
    }

    public string ListToString(List<int> elements)
    {
        return string.Join(",", elements);
    }
    
    public bool IsOOB(List<int> elements, List<int> endGoal)
    {
        for (var index = 0; index < elements.Count; index++)
        {
            if (elements[index] > endGoal[index])
                return true;
        }

        return false;
    }
    
    public bool IsReached(List<int> elements, List<int> endGoal)
    {
        for (var index = 0; index < elements.Count; index++)
        {
            if (elements[index] != endGoal[index])
                return false;
        }

        return true;
    }
    
    public List<int> ApplyMove(List<int> current, List<int> move)
    {
        var result = current.Select(c => 0 + c).ToList();
        foreach (var bit in move)
        {
            result[bit] += 1;
        }

        return result;
    }

    public record Input(int LightsEndGoal, List<int> Buttons, List<int> Voltages);
    public record InputP2(List<List<int>> Buttons, List<int> Voltages);
    
    int BoolListToInt(List<bool> bits)
    {
        int value = 0;
        foreach (bool bit in bits)
        {
            value = (value << 1) | (bit ? 1 : 0);
        }
        return value;
    }
    
    int FromIndexesMSB(List<int> indexes, int bitLength)
    {
        int value = 0;
        foreach (int i in indexes)
        {
            int pos = bitLength - 1 - i;  // flip index
            value |= (1 << pos);
        }
        return value;
    }
}