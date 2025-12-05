namespace AdventOfCode25.Solvers;

[SolverForADay(4)]
public class Day4Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        List<List<int>> cells = new List<List<int>>();
        foreach (var line in input)
        {
            var currentRow = new List<int>();
            foreach (var character in line)
            {
                currentRow.Add(character == '@' ? 1 : 0);
            }
            cells.Add(currentRow);
        }

        for (int i = 0; i < cells.Count; i++)
        {
            for (int j = 0; j < cells[0].Count; j++)
            {
                if (cells[i][j] == 1)
                {
                    var sum = SumNeighbours(cells, i, j);
                    if (sum < 4)
                        answer++;
                }
            }
        }
        
        return answer;
    }

    public int SumNeighbours(List<List<int>> cells, int row, int col)
    {
        var result = 0;
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                if (i == row && j == col)
                    continue;
                if (IsWithinBorders(cells,i,j))
                    result += cells[i][j];
            }
        }
        return result;
    }

    public bool IsWithinBorders(List<List<int>> cells, int row, int col)
    {
        var rows = cells.Count;
        var cols = cells[0].Count;
        
        return row >= 0 && row < rows && col >= 0 && col < cols;
    }

    public long SolveP2(string[] input)
    {
        long answer = 0L;
        List<List<int>> cells = new List<List<int>>();
        foreach (var line in input)
        {
            var currentRow = new List<int>();
            foreach (var character in line)
            {
                currentRow.Add(character == '@' ? 1 : 0);
            }
            cells.Add(currentRow);
        }

        while (true)
        {
            var removed = new HashSet<(int, int)>();
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[0].Count; j++)
                {
                    if (cells[i][j] == 1)
                    {
                        var sum = SumNeighbours(cells, i, j);
                        if (sum < 4)
                        {
                            answer++;
                            removed.Add((i, j));
                        }
                    }
                }
            }

            if (removed.Count == 0)
                break;
            
            foreach (var removedCell in removed)
                cells[removedCell.Item1][removedCell.Item2] = 0;
            
            removed.Clear();
        }

        return answer;
    }
}