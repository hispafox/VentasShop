using VentasShop.Dominio.Enumeraciones;

namespace VentasShop.Dominio.Excepciones;

/// <summary>Transición de estado no permitida en la máquina de estados del pedido.</summary>
public sealed class TransicionPedidoInvalidaException : Exception
{
    public TransicionPedidoInvalidaException(EstadoPedido desde, string accion)
        : base($"Transición inválida: no se puede '{accion}' un pedido en estado {desde}.") { }
}
