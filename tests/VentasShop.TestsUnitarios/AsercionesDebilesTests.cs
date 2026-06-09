using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Precios;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M7.2 · Falsos positivos — ASERCIONES DÉBILES (anti-ejemplos a propósito).
///
/// ⚠️ Los tres primeros tests están MAL hechos aposta: pasan en verde y "cubren"
/// <see cref="CalculadoraDescuentos.CalcularTasaDescuento"/>, pero no comprueban casi nada. Son el
/// material del submódulo: al pasar mutation testing (Stryker), el mutante que cambia el tramo del
/// descuento (p. ej. 0.10m → 0.0m) SOBREVIVE a estos tres, porque ninguno afirma el valor exacto.
/// El cuarto es el contraste: una aserción de verdad que mata ese mutante.
///
/// NO los copies como modelo. Ejecuta:  cd tests/VentasShop.TestsUnitarios && dotnet stryker --test-runner mtp
/// y mira en StrykerOutput/ cuáles sobreviven. Ver material/labs/M7.2-inspector-ciego-y-reloj-traidor.md.
/// </summary>
public class AsercionesDebilesTests
{
    private readonly CalculadoraDescuentos _calculadora = new();

    [Fact]
    public void CalcularTasaDescuento_DevuelveValorNoNegativo_DEBIL()
    {
        var tasa = _calculadora.CalcularTasaDescuento(500m, TipoCliente.Vip);
        Assert.True(tasa >= 0);   // ❌ se cumple para 0%, 15% y 99%: deja pasar casi cualquier bug
    }

    [Fact]
    public void CalcularTasaDescuento_NoLanza_DEBIL()
    {
        var ex = Record.Exception(() => _calculadora.CalcularTasaDescuento(500m, TipoCliente.Vip));
        Assert.Null(ex);          // ❌ solo dice "no petó", no dice "calculó bien"
    }

    [Fact]
    public void CalcularTasaDescuento_DevuelveDentroDelTope_DEBIL()
    {
        var tasa = _calculadora.CalcularTasaDescuento(500m, TipoCliente.Vip);
        Assert.True(tasa <= 0.15m);   // ❌ se cumple también para 0%, 5%, 10%: no fija el valor
    }

    // ✅ El contraste: la aserción fuerte. Afirma el valor exacto (10% volumen + 5% VIP = 15%).
    // Este SÍ mata el mutante que pone el tramo de volumen a cero (0.10m → 0.0m): 0 no es 0,15.
    [Fact]
    public void CalcularTasaDescuento_PedidoDe500ClienteVip_Aplica15Porciento_FUERTE()
    {
        var tasa = _calculadora.CalcularTasaDescuento(500m, TipoCliente.Vip);
        Assert.Equal(0.15m, tasa);
    }
}
