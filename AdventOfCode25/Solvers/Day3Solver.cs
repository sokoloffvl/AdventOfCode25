namespace AdventOfCode25.Solvers;
[SolverForADay(3)]
public class Day3Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;

        foreach (var line in input)
        {
            long max = -1;
            for (int j = 0; j < line.Length - 1; j++)
            {
                for (int i = j+1; i < line.Length; i++)
                {
                    var tmp = Convert.ToInt64(new String([line[j], line[i]]));
                    if (tmp > max)
                        max = tmp;
                }
            }

            answer += max;
        }

        return answer;
    }
    
    public long SolveP2(string[] input)
    {
        long answer = 0L;

        foreach (var line in input)
        {
            Console.WriteLine("-----------------------");
            var max = line[..12];
            //find reverse, if there's a reverse - do it and append newChar
            for (int i = 12; i < line.Length; i++)
            {
                Console.WriteLine(max);
                var reverseIndex = -1;
                for (int j = 0; j < 11; j++)
                {
                    if (max[j] < max[j + 1])
                    {
                        reverseIndex = j;
                        break;
                    }
                }

                if (reverseIndex != -1)
                {
                    max = max[..reverseIndex] + max[(reverseIndex + 1)..] + line[i];
                }
                else
                {
                    var newEnd = line[i] > max[11] ? line[i] : max[11];
                    max = max[..11] + newEnd;
                }
            }
            Console.WriteLine(max);
            answer += Convert.ToInt64(max);
            //if no reverse, check if new char is bigger than the last
        
        }

        return answer;
    }

    public long SolveP2_old(string[] input)
    {
        long answer = 0L;

        foreach (var line in input)
        {
            Console.WriteLine("-----------------------");
            var max = line.Substring(0, 12);
        
            for (int i = 12; i < line.Length; i++)
            {
                Console.WriteLine(max);
                var newChar =  line[i];
                var min = '9';
                var minIndex = -1;
                var hadBigger = false;
                for (int j = 11; j >= 0; j--)
                {
                    if (max[j] <= min)
                    {
                        min = max[j];
                        minIndex = j;
                    }
                    else
                    {
                        hadBigger = true;
                    }
                }

                if (hadBigger)
                {
                    max = max[..minIndex] + max[(minIndex + 1)..] + newChar;
                }
                else
                {
                    if (newChar > min)
                        max = max[1..] + newChar;
                }
            }
            Console.WriteLine(max);
            answer += Convert.ToInt64(max);
        }

        return answer;
    }
}