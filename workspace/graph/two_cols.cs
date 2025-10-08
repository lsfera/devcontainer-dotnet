#!/usr/bin/dotnet run
///: A two column layout version
static int FindAdjacents(int[] input)
{
    var allSpots = new int[input.First()].Select((_, i) => i + 1); ;
    var occupiedSpots = input.Skip(1);
    var availableSpots = allSpots.Except(occupiedSpots);
    var horizontal = allSpots.Chunk(2).Where(arr =>
    {
        var amendedArray = arr.Except(occupiedSpots);
        return amendedArray.ElementAtOrDefault(1) - amendedArray.First() == 1;
    });
    Console.WriteLine($"horizontal:{horizontal.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    var even = availableSpots.AdjacentSpots(
        int.IsEvenInteger, arr =>
        arr.ElementAtOrDefault(1) - arr.First() == 2);
    Console.WriteLine($"even:{even.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    var odd = availableSpots.AdjacentSpots(
        int.IsOddInteger,
        arr => arr.ElementAtOrDefault(1) - arr.First() == 2);
    Console.WriteLine($"odd:{odd.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    
    return horizontal.Union(even).Union(odd).Count();
}

Console.WriteLine(FindAdjacents([.. Console.ReadLine()!.Split(',').Select(int.Parse)]));

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