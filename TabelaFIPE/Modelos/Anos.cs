using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TabelaFIPE.Modelos;

internal class Anos
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? ID { get; set; }

    public static async Task<string> ObterAnos(HttpClient client, string modelosLink, string idEscolhido, List<Modelo> modelosEncontrados)
    {
        List<Anos> listaAnosCarro = new();
        List<Carro> Carros = new();
        int contador = 0;
        var padraoRegEx = $@"\b{Regex.Escape(idEscolhido)}\b";
        var regex = new Regex(padraoRegEx, RegexOptions.IgnoreCase);
        foreach (var item in modelosEncontrados)
        {
            string linkTemporario = $"{modelosLink}{item.ID}/years/";
            string resposta = await Processos.TentarSolicitacao(client, linkTemporario);
            var anosCarro = JsonSerializer.Deserialize<Anos[]>(resposta)!;
            listaAnosCarro.AddRange(anosCarro);

            contador++;

            foreach (var a in listaAnosCarro)
            {
                if (regex.IsMatch(a.ID!))
                {
                    Console.WriteLine(a.ID);
                    try
                    {
                        IEnumerable<Anos> verifyList;
                        Console.WriteLine($"{modelosLink}{item.ID}/years/{a.ID}");
                        string verify = await Processos.TentarSolicitacao(client, $"{modelosLink}{item.ID}/years/");
                        var verify2 = JsonSerializer.Deserialize<Anos[]>(verify)!;
                        verifyList.(verify2);
                        foreach (var q in verifyList)
                        {
                            if (regex.IsMatch(q.ID!))
                            {
                                resposta = await Processos.TentarSolicitacao(client, $"{modelosLink}{item.ID}/years/{a.ID}");
                                var carro = JsonSerializer.Deserialize<Carro>(resposta)!;

                                if (!Carros.Contains(carro))
                                    Carros.Add(carro);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Processos.MensagemExcecao(ex);
                    }
                }
            }
        }

        if (Carros.Count == 0)
            throw new Exception($"Não foram encontrados modelos do ano {idEscolhido}.");

        Console.WriteLine(Carros.Count);
        foreach (var item in Carros)
        {
            Console.WriteLine(item);
        }
        return "";
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
