#!/usr/bin/dotnet run
///: A four column layout version
static int FindAdjacents(int[] input)
{
    const int columns = 4;
    var allSpots = new int[input.First()].Select((_, i) => i + 1); ;
    var occupiedSpots = input.Skip(1);
    var availableSpots = allSpots.Except(occupiedSpots);
    var accumulator = FindAdjacentHorizontally(allSpots, occupiedSpots, columns);
    Console.WriteLine($"horizontal:{accumulator.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    foreach (var (col, modSpots) in from col in Enumerable.Range(1, columns).Select(i => i - 1).ToArray()
                                    let modSpots = availableSpots.FindAdjacentVertically(
                _ => _ % columns == col,
                arr => arr.ElementAtOrDefault(1) - arr.First() == columns)
                                    select (col, modSpots))
    {
        Console.WriteLine($"mod{col}:{modSpots.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
        accumulator = accumulator.Union(modSpots);
    }

    return accumulator.Count();
    
    static IEnumerable<int[]> FindAdjacentHorizontally(
        IEnumerable<int> allSpots,
        IEnumerable<int> occupiedSpots,
        int columns
        )
    {
        return allSpots
                .Select((v, i) => Enumerable.Range(2, columns - 2).Select(i => i).ToArray().Contains(v % columns) ? new[] { v, v } : [v])
                .SelectMany(x => x)
                .Chunk(2)
                .Where(arr =>
                {
                    var amendedArray = arr.Except(occupiedSpots);
                    return amendedArray.ElementAtOrDefault(1) - amendedArray.FirstOrDefault() == 1;
                });
    }
}

Console.WriteLine(FindAdjacents([12, 2, 5, 8, 12]));

public static class EnumerableExtensions
{
    public static IEnumerable<T[]> FindAdjacentVertically<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T[], bool> areAdjacent)
        => source.Where(predicate)
                .Select((v, i) => i > 0 ? new[] { v, v }: [v])
                .SelectMany(x => x)
                .Chunk(2)
                .Where(areAdjacent);
}