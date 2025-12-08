namespace AdventOfCode25.Solvers;

[SolverForADay(8)]
public class Day8Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        var take = input.Length == 20 ? 10 : 1000;
        List<Vector> vectors = new List<Vector>();

        foreach (var line in input)
        {
            var split = line.Split(',').Select(long.Parse).ToList();
            vectors.Add(new Vector(split[0], split[1], split[2]));
        }
        
        Dictionary<(Vector, Vector), long> distances = new Dictionary<(Vector, Vector), long>();

        for (int i = 0; i < vectors.Count; i++)
        {
            for (int j = i + 1; j < vectors.Count; j++)
            {
                var distance = Distance(vectors[i], vectors[j]);
                distances.Add((vectors[i], vectors[j]), distance);
            }
        }

        var keyValuePairs = distances.OrderBy(d => d.Value).Take(take);

        List<HashSet<Vector>> clusters = new List<HashSet<Vector>>() {};

        foreach (var keyValuePair in keyValuePairs)
        {
            var p1 = keyValuePair.Key.Item1;
            var p2 = keyValuePair.Key.Item2;
            var c1 = clusters.Where(c => c.Contains(p1)).ToList();
            var c2 = clusters.Where(c => c.Contains(p2)).ToList();
            var all = c1.Union(c2).Distinct().ToList();
            
            if (all.Count == 0)
            {
                clusters.Add(new HashSet<Vector> { p1, p2 });
            }
            else
            {
                var merged = new HashSet<Vector>(all.SelectMany(c => c));
                merged.Add(p1);
                merged.Add(p2);
            
                // remove old clusters and add merged one
                foreach (var c in all)
                    clusters.Remove(c);
            
                clusters.Add(merged);
            }
            
            // var c1 = clusters.FirstOrDefault(c => c.Contains(keyValuePair.Key.Item1));
            // var c2 = clusters.FirstOrDefault(c => c.Contains(keyValuePair.Key.Item2));
            //
            // if (c1 == null && c2 == null)
            // {
            //     clusters.Add([keyValuePair.Key.Item1, keyValuePair.Key.Item2]);
            //     continue;
            // }
            //
            // if (c1 is null && c2 is not null)
            // {
            //     c2.Add(keyValuePair.Key.Item1);
            //     c2.Add(keyValuePair.Key.Item2);
            //     continue;
            // }
            //
            // if (c1 is not null && c2 is null)
            // {
            //     c1.Add(keyValuePair.Key.Item1);
            //     c1.Add(keyValuePair.Key.Item2);
            //     continue;
            // }
            //
            // if (c1 is not null && c2 is not null && !c1.SetEquals(c2))
            // {
            //     clusters.Remove(c1);
            //     foreach (var point in c1)
            //     {
            //         c2.Add(point);
            //     }
            //     c2.Add(keyValuePair.Key.Item1);
            //     c2.Add(keyValuePair.Key.Item2);
            // }

        }
        
        var newClusters = clusters.OrderByDescending(c => c.Count).Take(3).ToArray();
        
        Console.WriteLine($"{newClusters[0].Count} and {newClusters[1].Count} and {newClusters[2].Count}");
        
        answer = newClusters[0].Count * newClusters[1].Count * newClusters[2].Count;

        return answer;
    }

    private long Distance(Vector a, Vector b)
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
    }

    public long SolveP2(string[] input)
    {
         long answer = 0L;
        var total = input.Length;
        List<Vector> vectors = new List<Vector>();

        foreach (var line in input)
        {
            var split = line.Split(',').Select(long.Parse).ToList();
            vectors.Add(new Vector(split[0], split[1], split[2]));
        }
        
        Dictionary<(Vector, Vector), long> distances = new Dictionary<(Vector, Vector), long>();

        for (int i = 0; i < vectors.Count; i++)
        {
            for (int j = i + 1; j < vectors.Count; j++)
            {
                var distance = Distance(vectors[i], vectors[j]);
                distances.Add((vectors[i], vectors[j]), distance);
            }
        }

        var keyValuePairs = distances.OrderBy(d => d.Value);

        List<HashSet<Vector>> clusters = new List<HashSet<Vector>>() {};

        foreach (var keyValuePair in keyValuePairs)
        {
            var p1 = keyValuePair.Key.Item1;
            var p2 = keyValuePair.Key.Item2;
            var c1 = clusters.Where(c => c.Contains(p1)).ToList();
            var c2 = clusters.Where(c => c.Contains(p2)).ToList();
            var all = c1.Union(c2).Distinct().ToList();
            
            if (all.Count == 0)
            {
                clusters.Add([p1, p2]);
            }
            else
            {
                var merged = new HashSet<Vector>(all.SelectMany(c => c));
                merged.Add(p1);
                merged.Add(p2);
            
                // remove old clusters and add merged one
                foreach (var c in all)
                    clusters.Remove(c);
            
                clusters.Add(merged);
            }

            if (clusters[0].Count == total)
            {
                answer = keyValuePair.Key.Item1.x * keyValuePair.Key.Item2.x;
                break;
            }
        }
        
        // var newClusters = clusters.OrderByDescending(c => c.Count).Take(3).ToArray();
        //
        // Console.WriteLine($"{newClusters[0].Count} and {newClusters[1].Count} and {newClusters[2].Count}");
        
        // answer = newClusters[0].Count * newClusters[1].Count * newClusters[2].Count;

        return answer;
    }

    private record Vector(long x, long y, long z);
}