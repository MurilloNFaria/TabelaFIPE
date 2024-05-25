using System.Linq;
using TabelaFIPE.Modelos; // ----- modelos do programa, classes e afins

// acessando a api
using (HttpClient client = new()) // using httpclient para encerrar o uso apos as operaçoes dentro das chaves
{
    try
    {
        string apiLink = "https://parallelum.com.br/fipe/api/v2/";
        string marcasLink = $"{apiLink}cars/brands/";
        // link raiz api


        string modelosLink = await ObterInputMarca(client, apiLink, marcasLink);

        
        await ObterInputModelo(client, apiLink, modelosLink);


        string linkAnos = await ObterInputAnos(client, apiLink, modelosLink);

        
        await ObterInputModeloEscolhido(client, apiLink, linkAnos);

    }
    catch (Exception ex)
    {
        Console.Write(ex.Message + ". . . Pressione (ENTER)");
        Console.ReadKey();
    }
}

static async Task<string> ObterInputMarca(HttpClient client, string apiLink, string marcasLink)
{
    Console.Write("Digite a marca: ");
    var marcaInput = Console.ReadLine()!;
    // pegando o input de marca

    var id = await Marca.ObterIdMarca(client, marcasLink, marcaInput);
    // metodo obter id compara o que foi digitado com o que existe na API, retornando o id

    Console.Clear();

    return $"{marcasLink}{id}/models/";
}

static async Task ObterInputModelo(HttpClient client, string apiLink, string modelosLink)
{
    Console.Write("Digite o nome do modelo: ");
    var modeloInput = Console.ReadLine()!;
    // pegando input do usuario

    await Modelo.ObterModelo(client, modelosLink, modeloInput);
}

static async Task<string> ObterInputAnos(HttpClient client, string apiLink, string linkAnos)
{
    Console.Write("Escolha um dos IDs acima: ");
    var idEscolhido = Console.ReadLine()!; // pedindo a entrada de um ID da lista de mostrados na tela

    // acessando os anos do modelo a partir do id escolhido
    
    return await Anos.ObterAnos(client, linkAnos, idEscolhido);
}

static async Task ObterInputModeloEscolhido(HttpClient client, string apiLink, string linkAnos)
{
    Console.Write("Digite o ano do modelo: ");
    var anoEscolhido = Console.ReadLine();

    string linkModeloEscolhido = $"{linkAnos}{anoEscolhido}";
    await Modelo.ObterModeloEscolhido(client, linkModeloEscolhido);
}