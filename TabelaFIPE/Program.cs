using System.Linq;
using System.Text.Json; // ----- para realizar açoes em arquivos json
using TabelaFIPE.Modelos; // ----- modelos do programa, classes e afins

// acessando a api
using (HttpClient client = new()) // using httpclient para encerrar o uso apos as operaçoes dentro das chaves
{
    try
    {
        string resposta;

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
        string linkModelos = $"{marcasLink}{id}/models/";
        // link para acessar os modelos a partir do id.

        resposta = await client.GetStringAsync(linkModelos);
        // aguardando resposta da api de maneira async

        var modelos = JsonSerializer.Deserialize<List<Modelo>>(resposta)!;
        // desserializando a resposta em formato json e transformando em lista do tipo MODELOS

        Console.Write("Digite o nome do modelo: ");
        var modeloInput = Console.ReadLine()!;

        foreach (var x in modelos)
        {
            if (x.Name == modeloInput)
            {
                foreach (var i in modelos) // verificando cada item via foreach
                {
                    if (i.Name!.Contains(modeloInput))  // filtrando itens que contem a entrada do usuario na lista item
                        id = i.ID; // id recebe o id do item que contem o nome
                }
                Console.Clear();
            }
        }
        // pegando input do usuario

        Console.Clear();

        IEnumerable<Modelo> modelosEnum = modelos.Where(x => x.Name!.Contains(modeloInput)).OrderBy(x => x.ID);
        // transformando a lista modelos em um IEnumerable para poupar processamento
        // passando apenas os itens filtrados que contem o nome digitado e ordenando pelo ID

        foreach (var item in modelosEnum) // verificando cada item IEnum via foreach
        {
            Console.WriteLine(item); // escrevendo os itens na tela
        }
        #endregion


        #region ID
        Console.Write("Escolha um dos IDs acima: ");
        var idEscolhido = Console.ReadLine(); // pedindo a entrada de um ID da lista de mostrados na tela

        Console.Clear();

        string linkEscolhido = $"{linkModelos}{idEscolhido}/years/";
        // acessando os anos do modelo a partir do id escolhido

        resposta = await client.GetStringAsync(linkEscolhido);
        // esperando a resposta de maneira async

        var modelosAnos = JsonSerializer.Deserialize<List<Anos>>(resposta)!;
        // desserializando a resposta recebida em json para uma lista do tipo ANOS

        foreach (var item in modelosAnos) // verificando cada item da lista via foreach
        {
            Console.WriteLine(item); // escrevendo todos os itens na tela
        }
        #endregion


        #region Modelo Escolhido
        Console.WriteLine("Digite o ano do modelo: ");
        var anoEscolhido = Console.ReadLine();

        string linkModeloEscolhido = $"{linkEscolhido}{anoEscolhido}";
        // definindo o link de acesso com o ano digitado

        resposta = await client.GetStringAsync(linkModeloEscolhido);
        // esperando a resposta da api de maneira async

        var modeloEscolhido = JsonSerializer.Deserialize<Carro>(resposta)!;
        // desserializando em formato de lista do tipo CARRO

        Console.WriteLine(modeloEscolhido);
        Console.WriteLine(linkModeloEscolhido);
        // mostrando os dados do modelo escolhido
        #endregion
    }
    catch (InputException ex)
    {
        Console.WriteLine(ex.Message);
    }

}

