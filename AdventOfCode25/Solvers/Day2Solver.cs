namespace AdventOfCode25.Solvers;
[SolverForADay(2)]
public class Day2Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        var inputString = input[0];
        var ranges = inputString.Split(',');
        var rangesTwo = ranges.Select(range => range.Split('-')).Select(r => r.Select(long.Parse).ToList()).ToList();
        // var rangesTwo = ranges.Select(range => range.Split('-').ToList()).ToList();
        long answer = 0L;
        foreach (var range in rangesTwo)
        {
            for (long i = range[0]; i<=range[1]; i++)
            {
                var str = i.ToString();
                if (str.Length > 0 && str.Length % 2 == 0)
                {
                    var len = str.Length;
                    var isG = true;
                    for (int j = 0; j < str.Length / 2; j++)
                    {
                        if (str[j] != str[j + (len / 2)])
                        {
                            isG = false;
                            break;
                        }
                    }
                    if (isG) answer += i;
                }
            }
        }
        return answer;
    }

    public long SolveP2(string[] input)
    {
        var inputString = input[0];
        var ranges = inputString.Split(',');
        var rangesTwo = ranges.Select(range => range.Split('-')).Select(r => r.Select(long.Parse).ToList()).ToList();
        // var rangesTwo = ranges.Select(range => range.Split('-').ToList()).ToList();
        long answer = 0L;
        foreach (var range in rangesTwo)
        {
            for (long i = range[0]; i<=range[1]; i++)
            {
                var str = i.ToString();
                if (str.Length <= 0) continue;
                var len = str.Length;
                var isG = false;
                for (int j = 1; j <= len/2; j++)
                {
                    var split = splitInParts(str, j);
                    if (split.Count == 0)
                        break;
                    var first =  split[0];
                    if (split.Any(s => s != first)) continue;
                    isG = true;
                    // Console.WriteLine(i);
                    break;
                }
                if (isG) answer += i;
            }
        }
        return answer;
    }

    private List<string> splitInParts(string original, int length)
    {
        try
        {
            var result = new List<string>();
            for (int i = 0; i < original.Length; i += length)
                result.Add(original.Substring(i, Math.Min(length, original.Length - i)));
            return result;
        }
        catch
        {
            return [];
        }
    }
}