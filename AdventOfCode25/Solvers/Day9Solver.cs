using System.Drawing;

namespace AdventOfCode25.Solvers;

[SolverForADay(9)]
public class Day9Solver : IDaySolver
{
    public long SolveP1(string[] input)
    {
        long answer = 0L;
        List<Point> coords = new List<Point>();

        foreach (var s in input)
        {
            var line = s.Split(',').Select(int.Parse).ToList();
            coords.Add(new Point(line[0], line[1]));
        }
        var maxArea = 0L;

        for (int i = 0; i < coords.Count - 1; i++)
        {
            for (int j = i + 1; j < coords.Count; j++)
            {
                var area = Area(coords[i], coords[j]);
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }
        return maxArea;
    }
    
    public long SolveP2(string[] input)
    {
        long answer = 0L;
        List<Point> coords = new List<Point>();

        foreach (var s in input)
        {
            var line = s.Split(',').Select(int.Parse).ToList();
            coords.Add(new Point(line[0], line[1]));
        }
        
        var segments = new List<Segment>();
        for (var i = 0; i < coords.Count - 1; i++)
        {
            segments.Add(new Segment(coords[i], coords[i + 1]));
        }
        segments.Add(new Segment(coords[^1], coords[0]));
        
        var maxArea = 0L;

        for (int i = 0; i < coords.Count - 1; i++)
        {
            for (int j = i + 1; j < coords.Count; j++)
            {
                var area = Area(coords[i], coords[j]);
                
                if (segments.Any(line => line.Intersects(new Segment(coords[i], coords[j]))))
                    continue;
                
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }
        return maxArea;
    }
    public record Point(int X, int Y);
    public record Segment(Point Start, Point End)
    {
        public bool Intersects(Segment other)
        {
            var otherMinX = Math.Min(other.Start.X, other.End.X);
            var otherMaxX = Math.Max(other.Start.X, other.End.X);
            var otherMinY = Math.Min(other.Start.Y, other.End.Y);
            var otherMaxY = Math.Max(other.Start.Y, other.End.Y);
 
            var minX = Math.Min(Start.X, End.X);
            var maxX = Math.Max(Start.X, End.X);
            var minY = Math.Min(Start.Y, End.Y);
            var maxY = Math.Max(Start.Y, End.Y);
 
            return maxX > otherMinX && minX < otherMaxX && maxY > otherMinY && minY < otherMaxY;
        }
    }

    public long Area(Point one, Point two)
    {
        var length = Math.Abs(one.X - two.X) + 1;
        var height = Math.Abs(one.Y - two.Y) + 1;
        
        return (long)length * height;
    }
}