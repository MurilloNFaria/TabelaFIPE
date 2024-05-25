namespace TabelaFIPE.Modelos;

internal class Processos
{
    public string APILink { get;  }
    public static bool Execution { get; set; }

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

        await Modelo.ObterModelo(client, modelosLink, modeloInput);
    }

    public static async Task<string> ObterInputAnos(HttpClient client, string linkAnos)
    {
        Console.Write("Escolha um dos IDs acima: ");
        var idEscolhido = Console.ReadLine()!;

        return await Anos.ObterAnos(client, linkAnos, idEscolhido);
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

    public static void MensagemExcecao(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(". . . Pressione [ENTER] . . .");
        Console.ReadLine();
    }
}
