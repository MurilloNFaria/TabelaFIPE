using System.Text.Json;
using System.Text.Json.Serialization;

namespace TabelaFIPE.Modelos;

internal class Marca
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? StringID { get; set; }

    public int ID
    {
        get
        {
            return int.Parse(StringID!);
        }
    }

    public override string ToString()
    {
        return $"Nome: {Name}, ID: {ID}\n";
    }

    public static async Task<int> ObterIdMarca(HttpClient client, string marcasLink, string marcaInput)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client), "HttpClient não pode ser nulo.");
        }

        if (string.IsNullOrEmpty(marcasLink))
        {
            throw new InputException("O link das marcas não pode ser nulo ou vazio.");
        }

        if (string.IsNullOrEmpty(marcaInput))
        {
            throw new InputException("A marca digitada não pode ser nula ou vazia.");
        }

        string resposta = await client.GetStringAsync(marcasLink);

        var marcas = JsonSerializer.Deserialize<List<Marca>>(resposta);
        if (marcas == null)
        {
            throw new InvalidOperationException("Erro ao desserializar a resposta da API.");
        }

        var marcaEncontrada = marcas.FirstOrDefault(m => string.Equals(m.Name, marcaInput, StringComparison.OrdinalIgnoreCase));

        if (marcaEncontrada != null)
        {
            return marcaEncontrada.ID;
        }
        else
        {
            throw new MarcaNotFoundException($"A marca '{marcaInput}' não foi encontrada.");
        }
    }
}
