using Microsoft.Extensions.Logging;
using VentasShop.Dominio.Abstracciones;

namespace VentasShop.Aplicacion;

/// <summary>
/// Servicio de aplicación que orquesta el dominio con sus dependencias externas
/// (repositorio, pasarela de pago, reloj y registro). Es el ejemplo natural de mocking (M5):
/// repositorio y pasarela se sustituyen por dobles; el reloj por un stub; el registro por un spy.
/// </summary>
public class ServicioPedidos(
    IRepositorioPedidos repositorio,
    IPasarelaPago pasarelaPago,
    IReloj reloj,
    ILogger<ServicioPedidos> registro)
{
    public void Pagar(Guid idPedido)
    {
        var pedido = repositorio.ObtenerPorId(idPedido)
            ?? throw new InvalidOperationException($"Pedido {idPedido} no encontrado.");

        var resultado = pasarelaPago.Cobrar(pedido.CalcularTotal(), pedido.Id.ToString());
        if (!resultado.Exito)
        {
            // BR-09: si la pasarela falla, el pedido no avanza ni se guarda.
            registro.LogWarning("Pago rechazado para el pedido {IdPedido} a las {Ahora}", pedido.Id, reloj.Ahora);
            return;
        }

        pedido.Pagar();
        repositorio.Guardar(pedido);
        // BR-10: al pagar, se registra un apunte de auditoría.
        registro.LogInformation("Pedido {IdPedido} pagado y auditado a las {Ahora}", pedido.Id, reloj.Ahora);
    }
}
