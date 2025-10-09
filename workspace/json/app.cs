#!/usr/bin/dotnet run
#:package Newtonsoft.Json@13.*

using Newtonsoft.Json.Linq;

JContainer StripValues(string json, params string[] values)
{
    Console.WriteLine($"Input JSON: {json}");
    JObject jsonObj = JObject.Parse(json);
    jsonObj.DescendantsAndSelf()
        .OfType<JValue>()
        .Where(x => x.Type == JTokenType.String && (values.Contains(x.Value<string>()) || string.IsNullOrWhiteSpace(x.Value<string>())))
        .Select(x => x.Parent is JProperty ? x.Parent : x as JToken)
        .ToList()
        .ForEach(x => x.Remove());
    return jsonObj;
}

Console.WriteLine(StripValues(Console.In.ReadToEnd(), "", "N\\A"));