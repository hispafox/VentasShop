using Microsoft.Extensions.Logging;
using VentasShop.Dominio.Abstracciones;   // IReloj (convención del curso)

namespace VentasShop.TestsUnitarios.Legacy;

// M8.1 — Ejemplo LEGACY NEUTRO e inventado (R1: jamás código de cliente). Es un mundo aparte de
// VentasShop, a propósito: representa "el proyecto real" que llega acoplado. Aquí se muestra la versión
// YA REFACTORIZADA (las costuras abiertas con inyección de dependencias). La versión "antes" (con los 4
// clavos: new RepositorioPedidosSql, DateTime.Now, new SmtpClient, Logger.Instance) está en
// material/labs/M8.1-diagnostico-legacy.md, porque clavada a producción no se puede ni compilar ni testear.

/// <summary>Pedido mínimo del ejemplo legacy (neutro). No es la entidad Pedido de VentasShop.</summary>
public sealed record PedidoLegacy(Guid Id, string ClienteEmail);

/// <summary>Acceso a datos del legacy. Costura: en el test se sustituye por un doble.</summary>
public interface IAccesoPedidos
{
    PedidoLegacy? Cargar(Guid pedidoId);
}

/// <summary>Envío de correo del legacy. Costura: en el test, un doble que no manda nada.</summary>
public interface IEnviadorCorreo
{
    void Enviar(string destinatario, string asunto);
}

/// <summary>
/// El NotificadorPedidos con las cuatro costuras abiertas: las dependencias (acceso a datos, reloj,
/// enviador de correo, logger) llegan por el constructor en vez de crearse/leerse por dentro. La LÓGICA
/// es idéntica a la del legacy original; solo cambia de dónde vienen las piezas. Por eso ahora se testea.
/// </summary>
public class NotificadorPedidos(
    IAccesoPedidos acceso,
    IReloj reloj,
    IEnviadorCorreo correo,
    ILogger<NotificadorPedidos> logger)
{
    public void NotificarEnvio(Guid pedidoId)
    {
        var pedido = acceso.Cargar(pedidoId);
        if (pedido is null) return;

        if (reloj.Ahora.Hour < 22)   // no molestar de noche
            correo.Enviar(pedido.ClienteEmail, "Tu pedido va de camino");

        logger.LogInformation("Pedido {PedidoId} notificado", pedidoId);
    }
}
