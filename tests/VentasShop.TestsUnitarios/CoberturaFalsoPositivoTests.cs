using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Precios;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M2.3 · Métricas de cobertura — el FALSO POSITIVO de cobertura.
///
/// ⚠️ ANTI-EJEMPLOS A PROPÓSITO. Los dos primeros tests están mal hechos aposta: pasan en verde y
/// "cubren" <see cref="CalculadoraDescuentos.CalcularTasaDescuento"/> (suben el porcentaje de
/// cobertura), pero no comprueban nada útil. Son el momento didáctico del submódulo: ejecuta la
/// suite con cobertura (ver <c>material/labs/M2.3-generar-cobertura.md</c>) y verás cómo el número
/// sube sin que ganes una pizca de seguridad. La cobertura mide ejecución, no verificación.
///
/// NO los copies como modelo. El tercero es el contraste: una aserción de verdad sobre el mismo
/// método, la única que cazaría un bug de cálculo.
/// </summary>
public class CoberturaFalsoPositivoTests
{
    private readonly CalculadoraDescuentos _calculadora = new();

    // ❌ FALSO POSITIVO: ejecuta el método y no comprueba nada. Pasa siempre mientras no lance una
    //    excepción, aunque la tasa devuelta fuera 0, 0.99 o un número al azar. Cámara apuntando a la pared.
    [Fact]
    public void CalcularTasaDescuento_NoRevienta()
    {
        _calculadora.CalcularTasaDescuento(500m, TipoCliente.Vip);

        // ... y aquí no hay ninguna aserción: la cobertura sube, la seguridad no.
    }

    // ❌ ASERCIÓN DÉBIL: `tasa >= 0` se cumple para 0%, para 15% y para 99%. Cubre, parece riguroso,
    //    y deja pasar cualquier bug de cálculo siempre que el resultado no sea negativo.
    [Fact]
    public void CalcularTasaDescuento_DevuelveAlgo()
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(500m, TipoCliente.Vip);

        Assert.True(tasa >= 0);   // ¿qué comprueba esto, en realidad? casi nada
    }

    // ✅ EL CONTRASTE: la misma línea ejecutada, pero con una aserción que SÍ verifica el resultado.
    //    Esta sí se pondría roja si alguien rompe el cálculo del tope (BR-05).
    [Fact]
    public void CalcularTasaDescuento_VipPedidoGrande_AplicaElTope()
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(800m, TipoCliente.Vip);

        Assert.Equal(0.15m, tasa);   // 10% de volumen + 5% de VIP = 15%, justo en el tope
    }
}
