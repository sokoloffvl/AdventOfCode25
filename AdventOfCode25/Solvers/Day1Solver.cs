namespace AdventOfCode25.Solvers;
[SolverForADay(1)]
public class Day1Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        List<long> moves = [];
        foreach (var line in input)
        {
            if (line[0] == 'R')
                moves.Add(long.Parse(line[1..]));
            else moves.Add(-1 * long.Parse(line[1..]));
        }

        var next = 50L;
        var answer = 0;
        foreach (var move in moves)
        {
            next += move;
            if (next % 100 == 0)
                answer++;
            // if (next >= 100) next -= 100;
            // if (next < 0) next += 100;
            // if ()
        }

        return answer;
    }

    public long SolveP2(string[] input)
    {
        List<long> moves = [];
        foreach (var line in input)
        {
            if (line[0] == 'R')
                moves.Add(long.Parse(line[1..]));
            else moves.Add(-1 * long.Parse(line[1..]));
        }

        var next = 50L;
        var answer = 0L;
        foreach (var move in moves)
        {
            var crosses = Math.Abs(move / 100);
            var remainder = move % 100;
            var prev = next;
            next += remainder;
            switch (next)
            {
                case >= 100:
                    next -= 100;
                    if (prev != 0) crosses++;
                    break;
                case < 0:
                    next += 100;
                    if (prev != 0) crosses++;
                    break;
                case 0:
                    crosses++;
                    break;
                default:
                    break;
            }
            answer += crosses;
            
            Console.WriteLine($"{move} : {next} : {answer}");
        }

        return answer;
    }
}