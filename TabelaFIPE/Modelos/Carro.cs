using System.Text;
using System.Text.Json.Serialization;

namespace TabelaFIPE.Modelos;

internal class Carro
{
    [JsonPropertyName("vehicleType")]
    public int VehicleType { get; set; }

    [JsonPropertyName("price")]
    public string? Price { get; set; }

    [JsonPropertyName("brand")]
    public string? Brand { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("modelYear")]
    public int ModelYear { get; set; }

    [JsonPropertyName("fuel")]
    public string? Fuel { get; set; }

    [JsonPropertyName("codeFipe")]
    public string? CodeFipe { get; set; }

    [JsonPropertyName("referenceMonth")]
    public string? ReferenceMonth { get; set; }

    [JsonPropertyName("fuelAcronym")]
    public string? FuelAcronym { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        Console.Clear();
        sb.AppendLine($"Tipo do veículo: {VehicleType}");
        sb.AppendLine($"Código FIPE: {CodeFipe}");
        sb.AppendLine($"Nome: {Model}, Ano: {ModelYear}");
        sb.AppendLine($"Marca: {Brand}");
        sb.AppendLine($"Combustivel: {Fuel}, {FuelAcronym}");
        sb.AppendLine($"Valor: {Price} - {ReferenceMonth}");

        return sb.ToString();
    }
}
