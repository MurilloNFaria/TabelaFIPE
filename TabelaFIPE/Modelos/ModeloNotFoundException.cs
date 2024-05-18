
namespace TabelaFIPE.Modelos;
internal class ModeloNotFoundException : Exception
{
    public ModeloNotFoundException(string? message) : base(message)
    {
    }
}