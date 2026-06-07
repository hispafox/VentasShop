using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.Dominio.Abstracciones;

/// <summary>Resultado de un cobro en la pasarela de pago.</summary>
public record ResultadoPago(bool Exito, string? Referencia = null);

/// <summary>
/// Pasarela de pago. Dependencia externa de <c>ServicioPedidos</c>: en los tests se sustituye
/// por un mock (BR-09: si el cobro falla, el pedido no avanza ni se guarda).
/// </summary>
public interface IPasarelaPago
{
    ResultadoPago Cobrar(Dinero importe, string referencia);
}
