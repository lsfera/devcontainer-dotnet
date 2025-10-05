#!/usr/bin/dotnet run
#:property TargetFramework=net10.0
#:property LangVersion=preview
#:property Nullable=enable
#:property ImplicitUsings=enable

using System.Numerics;

///: A parametric column layout version
static int FindAdjacents(int[][] input)
{
    var layout = input.First();
    var columns = layout.ElementAt(1)!;
    var availableSpotsCount = layout.ElementAt(0);
    var allSpots = new int[availableSpotsCount].Select((_, i) => i + 1); ;
    var occupiedSpots = input.ElementAt(1);
    var availableSpots = allSpots.Except(occupiedSpots);

    var accumulator = allSpots.FindAdjacentHorizontally(occupiedSpots, columns);
    Console.WriteLine($"horizontal:{accumulator.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    var verticalMatches = from col in Enumerable.Range(1, columns).Select(i => i - 1)
                let matches = availableSpots
                    .FindAdjacentVertically(
                        _ => _ % columns == col,
                        arr => arr.ElementAtOrDefault(1) - arr.First() == columns)
                select (col, matches);
                                    
    foreach (var (col, matches) in verticalMatches)
      {
        Console.WriteLine($"mod{col}:{matches.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    }

    return accumulator.Union(verticalMatches.SelectMany(f => f.matches)).Count();
}

foreach (var columns in new[] { 2, 3, 4 })
{
    Console.WriteLine($"Columns: {columns}");
    Console.WriteLine(FindAdjacents([[12, columns], [2, 5, 8, 12]]));
}

public static class Extensions
{
    extension<T>(IEnumerable<T> source)where T : INumber<T>
    {
        public IEnumerable<T[]> FindAdjacentVertically(
            Func<T, bool> predicate,
            Func<T[], bool> areAdjacent)
                => source.Where(predicate)
                    .Select((v, i) => i > 0 ? new[] { v, v } : [v])
                    .SelectMany(x => x)
                    .Chunk(2)
                    .Where(areAdjacent);

        public IEnumerable<T[]> FindAdjacentHorizontally(
            IEnumerable<T> occupiedSpots,
            int columns)  
                => source
                .Select((v, i) => Enumerable.Range(2, columns - 2)
                                            .Select(i => T.CreateChecked(i))
                                            .Contains(v % T.CreateChecked(columns)) ? new[] { v, v } : [v])
                .SelectMany(x => x)
                .Chunk(2)
                .Where(arr =>
                {
                    var amendedArray = arr.Except(occupiedSpots);
                    var first = amendedArray.FirstOrDefault();
                    var second = amendedArray.ElementAtOrDefault(1);
                    return first != null && second != null && second - first == T.CreateChecked(1);
                });
    }   
}
