namespace AdventOfCode25.Solvers;

[SolverForADay(7)]
public class Day7Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        List<List<int>> table = new List<List<int>>();

        foreach (var line in input)
        {
            var newRow = new List<int>();
            foreach (var c in line)
            {
                if (c == '.')
                    newRow.Add(0);
                if (c == 'S')
                    newRow.Add(1);
                if (c == '^')
                    newRow.Add(2);
            }
            table.Add(newRow);
        }

        for (int i = 1; i < table.Count; i++)
        {
            //split all beams first
            for (int j = 0; j < table[i].Count; j++)
            {
                if (table[i][j] == 2)
                {
                    if (table[i - 1][j] == 1)
                    {
                        answer++;
                        if (table[i][j-1] != 2)
                            table[i][j-1] = 1;
                        if (table[i][j+1] != 2)
                            table[i][j+1] = 1;
                    }
                }
            }
            //Continue beams
            for (int j = 0; j < table[i].Count; j++)
            {
                if (table[i - 1][j] == 1)
                {
                    if (table[i][j] != 2)
                        table[i][j] = 1;
                }
            }
        }

        return answer;
    }

    public long SolveP2(string[] input)
    {
        long answer = 0L;
        List<List<int>> table = new List<List<int>>();

       
        foreach (var line in input)
        {
            var newRow = new List<int>();
            foreach (var c in line)
            {
                if (c == '.')
                    newRow.Add(0);
                if (c == 'S')
                    newRow.Add(1);
                if (c == '^')
                    newRow.Add(2);
            }
            table.Add(newRow);
        }

        for (int i = 1; i < table.Count; i++)
        {
            //split all beams first
            for (int j = 0; j < table[i].Count; j++)
            {
                if (table[i][j] == 2)
                {
                    if (table[i - 1][j] == 1)
                    {
                        if (table[i][j-1] != 2)
                            table[i][j-1] = 1;
                        if (table[i][j+1] != 2)
                            table[i][j+1] = 1;
                    }
                }
            }
            //Continue beams
            for (int j = 0; j < table[i].Count; j++)
            {
                if (table[i - 1][j] == 1)
                {
                    if (table[i][j] != 2)
                        table[i][j] = 1;
                }
            }
        }

        var dp = table.Select(r => r.Select(c => 0L).ToList()).ToList();
        for (int i = 0; i < table[0].Count; i++)
        {
            dp[0][i] = table[0][i];
        }

        for (int i = 1; i < dp.Count; i++)
        {
            for (int j = 0; j < dp[0].Count; j++)
            {
                if (table[i][j] == 2)
                {
                    dp[i][j - 1] += dp[i-1][j];
                    dp[i][j + 1] += dp[i-1][j];
                }
                else
                {
                    dp[i][j] += dp[i-1][j];
                }
            }
        }

        
        answer = dp.Last().Sum();

        return answer;
    }
}