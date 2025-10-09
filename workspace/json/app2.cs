#!/usr/bin/dotnet run

using System.Text.Json;
using System.Text.Json.Nodes;

static JsonDocument StripValues(string json, params string[] values)
{
    Console.WriteLine($"Input JSON: {json}");
    using var jsonObj = JsonDocument.Parse(json);
    var doc = new JsonObject([]);
    foreach (var property in jsonObj.RootElement.EnumerateObject())
    {
        (string propertyName, JsonNode? value) = EvaluateProperty(values, property);
        if (value is not null)
            doc.Add(propertyName, value);
    }
    return JsonDocument.Parse(doc.ToJsonString());
}

Console.WriteLine(StripValues(Console.In.ReadToEnd(), "N\\A").RootElement.GetRawText());

static (string propertyName, JsonNode? value) EvaluateProperty(string[] values, JsonProperty property) => (property.Name, property.Value.ValueKind switch
{
    JsonValueKind.Object => new JsonObject(property.Value.EnumerateObject()
                                    .Select(subProperty => EvaluateProperty(values, subProperty))
                                    .Where(t => t.value is not null)
                                    .ToDictionary(t => t.propertyName, t => t.value)),
    JsonValueKind.Array when property.Value.EnumerateArray().All(x => x.ValueKind == JsonValueKind.String) => new JsonArray([..property.Value.EnumerateArray()
                                        .Where(x => x.ValueKind == JsonValueKind.String)
                                        .Select(x => x.GetString()!)
                                        .Except(values).Select(v => JsonValue.Create(v))]),
    JsonValueKind.Array => JsonNode.Parse(property.Value.GetRawText()),  // Convert array to JsonNode
    JsonValueKind.String when values.Contains(property.Value.GetString()) || string.IsNullOrWhiteSpace(property.Value.GetString()) => null,
    JsonValueKind.String => property.Value.GetString(),
    JsonValueKind.Number => property.Value.GetDouble(), // or GetInt32, GetDecimal etc. based on expected number type
    JsonValueKind.True => true,
    JsonValueKind.False => false,
    _ => null
});