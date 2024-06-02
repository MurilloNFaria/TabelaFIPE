namespace TabelaFIPE.Modelos;

internal class Processos
{
    public string APILink { get;  }
    public static bool Execution { get; set; }
    private static List<Modelo> modelosEncontrados = [];

    public Processos()
    {
        APILink = "https://parallelum.com.br/fipe/api/v2/";
        Execution = true;
    }

    public static async Task<string> ObterInputMarca(HttpClient client, string marcasLink)
    {
        Console.Write("Digite a marca: ");
        var marcaInput = Console.ReadLine()!;

        var id = await Marca.ObterIdMarca(client, marcasLink, marcaInput);

        Console.Clear();

        return $"{marcasLink}{id}/models/";
    }

    public static async Task ObterInputModelo(HttpClient client, string modelosLink)
    {
        Console.Write("Digite o nome do modelo: ");
        var modeloInput = Console.ReadLine()!;

        modelosEncontrados = await Modelo.ObterModelo(client, modelosLink, modeloInput);
    }

    public static async Task ObterInputAnos(HttpClient client, string linkAnos)
    {
        Console.Write("Insira o ano: ");
        var idEscolhido = Console.ReadLine()!;

        await Anos.ObterAnos(client, linkAnos, idEscolhido, modelosEncontrados);
    }

    public static async Task ObterInputModeloEscolhido(HttpClient client, string linkAnos)
    {
        Console.Write("Digite o ano do modelo: ");
        var anoEscolhido = Console.ReadLine();

        string linkModeloEscolhido = $"{linkAnos}{anoEscolhido}";
        await Modelo.ObterModeloEscolhido(client, linkModeloEscolhido);
    }

    public static void EncerrarPrograma()
    {
        Execution = false;
    }

    public static async Task<string> TentarSolicitacao(HttpClient client, string url, int maxTentativas = 5, int delayMilissegundos = 3000)
    {
        for (int i = 0; i < maxTentativas; i++)
        {
            try
            {
                Console.WriteLine("Carregando...");
                return await client.GetStringAsync(url);
            }
            catch (HttpRequestException ex) when ((int)ex.StatusCode! == 429)
            {
                if (i == maxTentativas - 1) throw;
                await Task.Delay(delayMilissegundos * (i + 1));
            }
        }
        throw new Exception("Falha ao obter uma resposta após várias tentativas.");
    }

    public static void MensagemExcecao(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(". . . Pressione [ENTER] . . .");
        Console.ReadLine();
    }
}
