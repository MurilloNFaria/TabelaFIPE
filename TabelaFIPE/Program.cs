using TabelaFIPE.Modelos;

using HttpClient client = new();

Processos programa = new();

while (Processos.Execution)
{
    try
    {
        string marcasLink = $"{programa.APILink}cars/brands/";

        string modelosLink = await Processos.ObterInputMarca(client, marcasLink);

        await Processos.ObterInputModelo(client, modelosLink);

        string linkAnos = await Processos.ObterInputAnos(client, modelosLink);

        await Processos.ObterInputModeloEscolhido(client, linkAnos);

        Processos.EncerrarPrograma();
    }
    catch (InputException ex)
    {
        Processos.MensagemExcecao(ex);
    }
    catch (MarcaNotFoundException ex)
    {
        Processos.MensagemExcecao(ex);
    }
    catch (InvalidOperationException ex)
    {
        Processos.MensagemExcecao(ex);
    }
    catch (ModeloNotFoundException ex)
    {
        Processos.MensagemExcecao(ex);
    }
    catch (ArgumentNullException ex)
    {
        Processos.MensagemExcecao(ex);
    }
    catch (Exception ex)
    {
        Processos.MensagemExcecao(ex);
    }
    finally
    {
        await Task.Delay(1000);
    }
}
