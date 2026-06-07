using VentasShop.Dominio.Abstracciones;
using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.Infraestructura.Pagos;

/// <summary>
/// Pasarela de pago simulada: siempre cobra con éxito. En producción se sustituiría por la
/// integración real; en los tests, por un mock configurable (éxito/fallo).
/// </summary>
public sealed class PasarelaPagoSimulada : IPasarelaPago
{
    public ResultadoPago Cobrar(Dinero importe, string referencia) => new(true, $"SIM-{referencia}");
}
