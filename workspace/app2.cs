#!/usr/bin/dotnet run
///: A three column layout version
static int FindAdjacents(int[] input)
{
    var allSpots = new int[input.First()].Select((_, i) => i + 1); ;
    var occupiedSpots = input.Skip(1);
    var availableSpots = allSpots.Except(occupiedSpots);
    
    var horizontal = allSpots
                        .Select((v, i) => v % 3 == 2 ? new[] { v, v } : [v])
                        .SelectMany(x => x)
                        .Chunk(2)
                        .Where(arr =>
    {
        var amendedArray = arr.Except(occupiedSpots);
        return amendedArray.ElementAtOrDefault(1) - amendedArray.FirstOrDefault() == 1;
    });
    Console.WriteLine($"horizontal:{horizontal.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    var mod0 = availableSpots.AdjacentSpots(
        _ => _ % 3 == 0,
        arr => arr.ElementAtOrDefault(1) - arr.First() == 3);
    Console.WriteLine($"mod0:{mod0.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    var mod1 = availableSpots.AdjacentSpots(
        _ => _ % 3 == 1,
        arr => arr.ElementAtOrDefault(1) - arr.First() == 3);
    Console.WriteLine($"mod1:{mod1.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    var mod2 = availableSpots.AdjacentSpots(
        _ => _ % 3 == 2,
        arr => arr.ElementAtOrDefault(1) - arr.First() == 3);
    Console.WriteLine($"mod2:{mod2.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    
    return horizontal.Union(mod0).Union(mod1).Union(mod2).Count();
}

Console.WriteLine(FindAdjacents([12, 2, 5, 8, 12]));

public static class EnumerableExtensions
{
    public static IEnumerable<T[]> AdjacentSpots<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T[], bool> areAdjacent)
        => source.Where(predicate)
                .Select((v, i) => i > 0 ? new[] { v, v }: [v])
                .SelectMany(x => x)
                .Chunk(2)
                .Where(areAdjacent);
}