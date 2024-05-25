using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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

        // padrao regEx = insere modeloinput diretamente no padrao da expressao sem escapar caracteres especiais
        // \b faz modeloinput ser lido como uma palavra inteira evitando correspondencias parciais
        var padraoRegEx = $@"\b{Regex.Escape(modeloInput)}\b";
        // cria um objeto regex com o padrao indicado e define como opçoes o ignorecase
        var regex = new Regex(padraoRegEx, RegexOptions.IgnoreCase);

        foreach (var m in modelos)
        {
            if (regex.IsMatch(m.Name!))
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

        var modeloEscolhido = JsonSerializer.Deserialize<Carro>(resposta)!;

        Console.WriteLine(modeloEscolhido);
        Console.WriteLine("Fonte: " + modeloEscolhidoLink);
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