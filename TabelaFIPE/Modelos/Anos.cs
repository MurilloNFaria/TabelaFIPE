using System.Text.Json;
using System.Text.Json.Serialization;

namespace TabelaFIPE.Modelos;

internal class Anos
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? ID { get; set; }

    public static async Task ObterAnos(HttpClient client, string anosLink, string idEscolhido)
    {
        string resposta = await client.GetStringAsync(anosLink);
        // esperando a resposta de maneira async

        var modelosAnos = JsonSerializer.Deserialize<List<Anos>>(resposta)!;
        // desserializando a resposta recebida em json para uma lista do tipo ANOS

        PrintarModeloAnos(modelosAnos);
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
