namespace MinhaWebApiComRedis.Models
{
    internal static class CotacoesBaseLayout
    {
        internal static readonly string[] NomeMoedas = new[]
        {
            "Real", "Dólar", "Euro", "Peso Argentino", "Dólar Canadense", "Libra Esterlina"
        };

        internal static readonly string[] SiglasMoedas = new[]
        {
            "BRL", "USD", "EUR", "ARS", "CAD", "GBP"
        };

        internal static readonly decimal[] ValoresMoedas = new[]
        {
            1, 5.31m, 6.4m, 0.06m, 4.19m, 7.22m
        };
    }
}
