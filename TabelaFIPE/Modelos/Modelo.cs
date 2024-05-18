using System.Linq;
using System.Text.Json;
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

    public static async Task<List<Modelo>> ObterModelo(HttpClient client, string modelosLink, string modeloInput)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client), "HttpClient não pode ser nulo.");
        }

        if (string.IsNullOrEmpty(modelosLink))
        {
            throw new InputException("O link das marcas não pode ser nulo ou vazio.");
        }

        if (string.IsNullOrEmpty(modeloInput))
        {
            throw new InputException("A marca digitada não pode ser nula ou vazia.");
        }

        string resposta = await client.GetStringAsync(modelosLink);

        var modelos = JsonSerializer.Deserialize<List<Modelo>>(resposta)!;

        List<Modelo> modelosEncontrados = new();

        if (modelos == null)
        {
            throw new InvalidOperationException("Erro ao desserializar a resposta da API.");
        }

        foreach (var m in modelos)
        {
            if (m.Name!.Contains(modeloInput, StringComparison.OrdinalIgnoreCase))
                modelosEncontrados.Add(m);
        }

        if (modelosEncontrados.Count == 0)
            throw new ModeloNotFoundException($"O modelo '{modeloInput}' não foi encontrada.");

        PrintarModelos(modelosEncontrados);
        return modelosEncontrados;
    }

    public static async Task ObterModeloEscolhido(HttpClient client, string modeloEscolhidoLink)
    {
        string resposta = await client.GetStringAsync(modeloEscolhidoLink);
        // esperando a resposta da api de maneira async

        var modeloEscolhido = JsonSerializer.Deserialize<Carro>(resposta)!;
        // desserializando em formato de lista do tipo CARRO

        Console.WriteLine(modeloEscolhido);
        Console.WriteLine(modeloEscolhidoLink);
        // mostrando os dados do modelo escolhido
    }

    private static void PrintarModelos(List<Modelo> modelosEncontrados) 
    {
        Console.Clear();
        foreach (var m in modelosEncontrados)
        {
            Console.WriteLine(m);
        }
    }

    public override string ToString()
    {
        return $"ID: {ID} - Nome: {Name}";
    }
}
