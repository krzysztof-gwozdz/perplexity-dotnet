#nullable disable
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Perplexity.AgenticResearch.Dtos;

public class OutputItemConverter : JsonConverter<OutputItem>
{
    public override OutputItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = root.TryGetProperty("type", out var typeProp) ? typeProp.GetString() : null;

        return type switch
        {
            "message" => root.Deserialize<MessageOutputItem>(options),
            "search_results" => root.Deserialize<SearchResultsOutputItem>(options),
            "fetch_url_results" => root.Deserialize<FetchUrlResultsOutputItem>(options),
            "function_call" => root.Deserialize<FunctionCallOutputItem>(options),
            _ => root.Deserialize<UnknownOutputItem>(options)
        };
    }

    public override void Write(Utf8JsonWriter writer, OutputItem value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
