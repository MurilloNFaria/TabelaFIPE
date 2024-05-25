using System.Text.Json;
using System.Text.Json.Serialization;

namespace TabelaFIPE.Modelos;

internal class Anos
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? ID { get; set; }

    public static async Task<string> ObterAnos(HttpClient client, string modelosLink, string idEscolhido)
    {
        string linkAnos = $"{modelosLink}{idEscolhido}/years/";
        string resposta = await client.GetStringAsync(linkAnos);

        var modelosAnos = JsonSerializer.Deserialize<List<Anos>>(resposta)!;

        PrintarModeloAnos(modelosAnos);
        return linkAnos;
    }

    private static void PrintarModeloAnos(List<Anos> modelosAnos)
    {
        Console.Clear();
        foreach (var m in modelosAnos) // verificando cada item da lista via foreach
        {
            Console.WriteLine(m); // escrevendo todos os itens na tela
        }
    }

    public override string ToString()
    {
        return $"{ID} - {Name}";
    }
}
