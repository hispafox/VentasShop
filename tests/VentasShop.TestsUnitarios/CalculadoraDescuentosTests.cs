using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Precios;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// Tests del <see cref="CalculadoraDescuentos"/> que muestran tres técnicas de diseño de casos:
/// partición en clases de equivalencia, análisis de valores límite y tabla de decisión.
/// Aserciones nativas de xUnit (las fluidas llegan en M5.3). Los decimales van en <c>TheoryData</c>,
/// no en <c>[InlineData]</c>: un atributo de C# no admite constantes <c>decimal</c>.
/// </summary>
public class CalculadoraDescuentosTests
{
    private readonly CalculadoraDescuentos _calculadora = new();

    // === Partición en clases de equivalencia: un representante por tramo de volumen ===
    [Theory]
    [MemberData(nameof(CasosPorTramo))]
    public void CalcularTasaDescuento_SegunTramoDeVolumen(decimal total, decimal esperado)
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(total, TipoCliente.Estandar);

        Assert.Equal(esperado, tasa);
    }

    public static TheoryData<decimal, decimal> CasosPorTramo() => new()
    {
        { 50m,  0.00m },   // clase baja  (BR-01)
        { 200m, 0.05m },   // clase media (BR-02)
        { 800m, 0.10m },   // clase alta  (BR-03)
    };

    // === Análisis de valores límite: el 100 y el 500, cada uno con sus vecinos ===
    [Theory]
    [MemberData(nameof(CasosFrontera))]
    public void CalcularTasaDescuento_EnLasFronteras(decimal total, decimal esperado)
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(total, TipoCliente.Estandar);

        Assert.Equal(esperado, tasa);
    }

    public static TheoryData<decimal, decimal> CasosFrontera() => new()
    {
        { 99.99m,  0.00m },   // justo por debajo del primer salto
        { 100.00m, 0.05m },   // el límite de los 100
        { 499.99m, 0.05m },   // justo por debajo del segundo salto
        { 500.00m, 0.10m },   // el límite de los 500: AQUÍ vive el bug típico (>= frente a >)
        { 500.01m, 0.10m },   // justo por encima
    };

    // === Tabla de decisión: importe × tipo de cliente (incluye el tope, BR-05) ===
    [Theory]
    [MemberData(nameof(CasosImporteYTipo))]
    public void CalcularTasaDescuento_PorImporteYTipo(decimal total, TipoCliente tipo, decimal esperado)
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(total, tipo);

        Assert.Equal(esperado, tasa);
    }

    public static TheoryData<decimal, TipoCliente, decimal> CasosImporteYTipo() => new()
    {
        { 50m,  TipoCliente.Estandar, 0.00m },
        { 50m,  TipoCliente.Vip,      0.05m },   // 0 de volumen + 5% VIP
        { 200m, TipoCliente.Premium,  0.08m },   // 5% + 3%
        { 800m, TipoCliente.Estandar, 0.10m },
        { 800m, TipoCliente.Premium,  0.13m },   // 10% + 3%
        { 800m, TipoCliente.Vip,      0.15m },   // 10% + 5% = 15%, el tope (BR-05)
    };
}
