using VentasShop.Dominio.Abstracciones;
using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.TestsUnitarios.Dobles;

/// <summary>
/// Doble de <see cref="IPasarelaPago"/> hecho a mano (M5.1) que hace dos papeles a la vez:
/// es STUB porque devuelve un resultado prefijado (el cobro sale bien o mal según lo que el test pida
/// al construirlo), y es SPY porque registra cada cobro —cuántas veces y con qué importe— para que el
/// test verifique la interacción después (BR-09: si el cobro falla, el pedido no avanza).
/// Sustituye a la pasarela real para no cobrar dinero de verdad: este es "el coche en llamas".
/// </summary>
public sealed class PasarelaPagoFalsa(bool exito) : IPasarelaPago
{
    /// <summary>Cuántas veces se ha intentado cobrar (verificación de interacción).</summary>
    public int VecesCobrado { get; private set; }

    /// <summary>El último importe con el que se llamó a <see cref="Cobrar"/>.</summary>
    public Dinero? UltimoImporte { get; private set; }

    public ResultadoPago Cobrar(Dinero importe, string referencia)
    {
        VecesCobrado++;
        UltimoImporte = importe;
        return new ResultadoPago(exito, exito ? "REF-TEST" : null);
    }
}
