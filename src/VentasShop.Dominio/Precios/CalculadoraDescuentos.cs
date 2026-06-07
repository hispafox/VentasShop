using VentasShop.Dominio.Enumeraciones;

namespace VentasShop.Dominio.Precios;

/// <summary>
/// Lógica pura de descuento (sin dependencias externas). Núcleo de los ejemplos de
/// tests unitarios, partición de equivalencia, valores límite y tabla de decisión.
/// </summary>
public class CalculadoraDescuentos
{
    // BR-01..BR-05: descuento por volumen + por tipo de cliente, con tope del 15%.
    public decimal CalcularTasaDescuento(decimal totalPedido, TipoCliente tipo)
    {
        decimal descuentoVolumen = totalPedido switch
        {
            >= 500m => 0.10m,   // BR-03: pedido grande
            >= 100m => 0.05m,   // BR-02: pedido medio
            _       => 0m       // BR-01: pedido pequeño, sin descuento por volumen
        };

        decimal bonificacionTipo = tipo switch
        {
            TipoCliente.Vip     => 0.05m,  // BR-04: el VIP suma 5%
            TipoCliente.Premium => 0.03m,  // BR-04: el Premium suma 3%
            _                   => 0m      // el cliente estándar no suma nada
        };

        return Math.Min(descuentoVolumen + bonificacionTipo, 0.15m);  // BR-05: nunca pasa del 15%
    }
}
