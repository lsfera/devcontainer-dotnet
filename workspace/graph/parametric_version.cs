#!/usr/bin/env dotnet
#:property TargetFramework=net10.0
#:property LangVersion=preview
#:property Nullable=enable
#:property ImplicitUsings=enable

using System.IO.Pipelines;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

///: A parametric column layout version
static int FindAdjacents(int[] input, int columns)
{
    var availableSpotsCount = input[0];
    var allSpots = new int[availableSpotsCount].Select((_, i) => i + 1); ;
    var occupiedSpots = input[1..];
    var availableSpots = allSpots.Except(occupiedSpots);

    var horizontalMatches = allSpots.FindAdjacentHorizontally(occupiedSpots, columns);  
    Console.WriteLine($"horizontal:{horizontalMatches.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
    var verticalMatches = Enumerable.Range(1, columns).Select(i => i - 1)
                .SelectMany(col =>
                    availableSpots
                    .FindAdjacentVertically(
                        _ => _ % columns == col,
                        arr => arr.ElementAtOrDefault(1) - arr.First() == columns)
                    .Select(matches =>
                    {
                        Console.WriteLine($"mod{col}:{matches.Aggregate("", (a, b) => $"{a}[{string.Join(",", b)}],").TrimEnd(',')}");
                        return (col, matches);
                    }));


    return verticalMatches.Select(f => f.matches)
            .Union(horizontalMatches).Count() ;
}
var pipe = new Pipe();
await pipe.Writer.WriteAsync(System.Text.Encoding.UTF8.GetBytes((await Console.In.ReadLineAsync())!));
await pipe.Writer.CompleteAsync();
var inputs = await JsonSerializer.DeserializeAsync(pipe.Reader, JsonContext.Default.Int32ArrayArray) ?? throw new InvalidOperationException("Deserialization failed");
foreach (var column in inputs[1])
{
    Console.WriteLine($"Columns: {column}");
    Console.WriteLine(FindAdjacents(inputs[0],column));
}



[JsonSerializable(typeof(int[][]))]
public partial class JsonContext : JsonSerializerContext { }

public static class Extensions
{
    extension<T>(IEnumerable<T> source) where T : INumber<T>
    {
        public IEnumerable<IEnumerable<T>> FindAdjacentVertically(
            Func<T, bool> predicate,
            Func<T[], bool> areAdjacent)
                => source.Where(predicate)
                    .Select((v, i) => i > 0 ? new[] { v, v } : [v])
                    .SelectMany(x => x)
                    .Chunk(2)
                    .Where(areAdjacent);

        public IEnumerable<IEnumerable<T>> FindAdjacentHorizontally(
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
