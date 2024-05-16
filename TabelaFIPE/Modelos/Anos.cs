using System.Text.Json.Serialization;

namespace TabelaFIPE.Modelos;

internal class Anos
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? ID { get; set; }

    public override string ToString()
    {
        return $"{ID} - {Name}";
    }
}
