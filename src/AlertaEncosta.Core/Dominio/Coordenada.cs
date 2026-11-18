namespace AlertaEncosta.Core.Dominio;

/// <summary>Ponto geográfico (graus decimais). Usado para achar o ponto de apoio mais próximo.</summary>
public readonly record struct Coordenada(double Latitude, double Longitude)
{
    private const double RaioTerraKm = 6371.0;

    public void Validar()
    {
        if (Latitude is < -90 or > 90 || Longitude is < -180 or > 180)
            throw new DominioException("Coordenada inválida: latitude entre -90 e 90 e longitude entre -180 e 180.");
    }

    /// <summary>Distância em linha reta pela fórmula de Haversine (em km).</summary>
    public double DistanciaKm(Coordenada outra)
    {
        double ParaRad(double graus) => graus * Math.PI / 180.0;

        var dLat = ParaRad(outra.Latitude - Latitude);
        var dLon = ParaRad(outra.Longitude - Longitude);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ParaRad(Latitude)) * Math.Cos(ParaRad(outra.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return RaioTerraKm * c;
    }

    public override string ToString() =>
        $"{Latitude.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture)}, " +
        $"{Longitude.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture)}";
}
