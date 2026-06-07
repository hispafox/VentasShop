namespace VentasShop.Infraestructura.Legacy;

/// <summary>
/// Código LEGACY neutro (Módulo 8). Intencionadamente acoplado: depende de la hora del sistema
/// y escribe por consola, sin costuras que permitan testearlo. El ejercicio de M8 es abrir esas
/// costuras (inyección de dependencias) para poder ponerle tests. NO es código de cliente (R1).
/// </summary>
public class NotificadorPedidos
{
    public string Notificar(string idPedido, decimal total)
    {
        var ahora = DateTime.Now; // acoplamiento al reloj del sistema
        var mensaje = $"[{ahora:yyyy-MM-dd HH:mm}] Pedido {idPedido} confirmado por {total:0.00} EUR";
        Console.WriteLine(mensaje); // acoplamiento a la consola
        return mensaje;
    }
}
