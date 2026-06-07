namespace VentasShop.Dominio.Enumeraciones;

/// <summary>
/// Estados del ciclo de vida del pedido. Máquina de estados:
/// Borrador → Confirmado → Pagado → Enviado → Entregado. Cancelar válido hasta Pagado.
/// </summary>
public enum EstadoPedido
{
    Borrador,
    Confirmado,
    Pagado,
    Enviado,
    Entregado,
    Cancelado
}
