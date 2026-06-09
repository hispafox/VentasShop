using VentasShop.Dominio.Entidades;

namespace VentasShop.Dominio.Abstracciones;

/// <summary>
/// Repositorio de pedidos. En los unitarios se sustituye por un doble en memoria;
/// en los tests de integración (M6) se usa la implementación real sobre EF Core.
/// M6.4 amplía el mostrador con la lectura con líneas, la consulta por cliente y el borrado.
/// </summary>
public interface IRepositorioPedidos
{
    Pedido? ObtenerPorId(Guid id);                       // Find: no carga navegaciones
    Pedido? ObtenerConLineas(Guid id);                   // con Include de Lineas y Cliente
    IReadOnlyList<Pedido> ObtenerPorCliente(Guid clienteId);
    void Agregar(Pedido pedido);
    void Guardar(Pedido pedido);
    void Eliminar(Pedido pedido);
}
