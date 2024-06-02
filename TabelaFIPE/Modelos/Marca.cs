using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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

        string resposta = await Processos.TentarSolicitacao(client, marcasLink);

        var marcas = JsonSerializer.Deserialize<List<Marca>>(resposta) 
            ?? throw new InvalidOperationException("Erro ao desserializar a resposta da API.");

        // padrao regEx = insere modeloinput diretamente no padrao da expressao sem escapar caracteres especiais
        // \b faz marcainput ser lido como uma palavra inteira evitando correspondencias parciais
        var padraoRegEx = $@"\b{Regex.Escape(marcaInput)}\b";
        // cria um objeto regex com o padrao indicado e define como opçoes o ignorecase
        var regex = new Regex(padraoRegEx, RegexOptions.IgnoreCase);

        var marcaEncontrada = marcas.FirstOrDefault(m => regex.IsMatch(m.Name!));

        if (marcaEncontrada != null)
        {
            return marcaEncontrada.ID;
        }
        else
        {
            throw new MarcaNotFoundException($"A marca '{marcaInput}' não foi encontrada.");
        }
    }

    public override string ToString()
    {
        return $"Nome: {Name}, ID: {ID}\n";
    }
}
