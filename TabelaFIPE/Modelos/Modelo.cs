using System.Text.Json.Serialization;

namespace TabelaFIPE.Modelos;

internal class Modelo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string StringID { get; set; }

    public int ID
    {
        get
        {
            return int.Parse(StringID!);
        }
    }

    public override string ToString()
    {
        return $"ID: {ID} - Nome: {Name}";
    }
}
