using System.Linq;
using System.Text.Json; // ----- para realizar açoes em arquivos json
using TabelaFIPE.Modelos; // ----- modelos do programa, classes e afins

// acessando a api
using (HttpClient client = new()) // using httpclient para encerrar o uso apos as operaçoes dentro das chaves
{
    try
    {

        string apiLink = "https://parallelum.com.br/fipe/api/v2/";
        // link raiz api


        #region Marcas

        string marcasLink = $"{apiLink}cars/brands/";
        // link acessar marcas

        Console.Write("Digite a marca: ");
        var marcaInput = Console.ReadLine()!;
        // pegando o input de marca

        var id = await Marca.ObterIdMarca(client, marcasLink, marcaInput);
        // metodo obter id compara o que foi digitado com o que existe na API, retornando o id

        Console.Clear();
        #endregion


        #region Modelos
        string modelosLink = $"{marcasLink}{id}/models/";
        // link para acessar os modelos a partir do id.

        Console.Write("Digite o nome do modelo: ");
        var modeloInput = Console.ReadLine()!;
        // pegando input do usuario

        await Modelo.ObterModelo(client, modelosLink, modeloInput);
        #endregion


        #region ID
        Console.Write("Escolha um dos IDs acima: ");
        var idEscolhido = Console.ReadLine()!; // pedindo a entrada de um ID da lista de mostrados na tela

        Console.Clear();

        string linkAnos = $"{modelosLink}{idEscolhido}/years/";
        // acessando os anos do modelo a partir do id escolhido

        await Anos.ObterAnos(client, linkAnos, idEscolhido);
        #endregion


        #region Modelo Escolhido
        Console.WriteLine("Digite o ano do modelo: ");
        var anoEscolhido = Console.ReadLine();

        string linkModeloEscolhido = $"{linkAnos}{anoEscolhido}";
        // definindo o link de acesso com o ano digitado

        await Modelo.ObterModeloEscolhido(client, linkModeloEscolhido);
        #endregion


    }
    catch (Exception ex)
    {
        Console.Write(ex.Message);
        Console.ReadKey();
    }

}

