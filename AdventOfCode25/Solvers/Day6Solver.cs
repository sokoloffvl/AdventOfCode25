namespace AdventOfCode25.Solvers;

[SolverForADay(6)]
public class Day6Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        var inputs = new List<List<long>>();
        var operations = new List<string>();
        foreach (var line in input)
        {
            var newline = line.Trim();
            if (char.IsDigit(newline[0]))
                inputs.Add(newline.Split(' ').Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(long.Parse).ToList());
            else operations = newline.Split(' ').Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s)).ToList();
        }

        for (int i = 0; i < operations.Count; i++)
        {
            if (operations[i] == "+")
                answer += inputs.Select(im => im[i]).Sum();
            else answer += inputs.Select(im => im[i]).Aggregate(1L, (acc, x) => acc * x);
            // Console.WriteLine(firstLine[i] + operations[i] + secondLine[i]  + operations[i]+ thirdLine[i]);
        }
        return answer;
    }

    public long SolveP2(string[] input)
    {
        long answer = 0L;
        List<long> buffer = new List<long>();
        char lastOperation = '+';
        for (int i = input[0].Length - 1; i >= 0; i--)
        {
            var chars = string.Empty;
            foreach (var line in input)
            {
                chars += line[i];
            }

            if (string.IsNullOrEmpty(chars.Trim()))
            {
                Console.WriteLine(lastOperation + string.Join(" ", buffer));
                if (lastOperation == '+')
                    answer += buffer.Sum();
                else answer += buffer.Aggregate(1L, (acc, x) => acc * x);
                buffer.Clear();
            }
            else if (!char.IsDigit(chars.Trim().Last()))
            {
                lastOperation = chars.Trim().Last();
                buffer.Add(long.Parse(chars.Trim()[..^1]));
            }
            else
            {
                buffer.Add(long.Parse(chars.Trim()));
            }
        }
        Console.WriteLine(lastOperation + string.Join(" ", buffer));
        if (lastOperation == '+')
            answer += buffer.Sum();
        else answer += buffer.Aggregate(1L, (acc, x) => acc * x);
        buffer.Clear();
        return answer;
    }
}