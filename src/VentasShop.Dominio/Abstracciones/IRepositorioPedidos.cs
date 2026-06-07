using VentasShop.Dominio.Entidades;

namespace VentasShop.Dominio.Abstracciones;

/// <summary>
/// Repositorio de pedidos. En los unitarios se sustituye por un doble en memoria;
/// en los tests de integración (M6) se usa la implementación real sobre EF Core.
/// </summary>
public interface IRepositorioPedidos
{
    Pedido? ObtenerPorId(Guid id);
    void Agregar(Pedido pedido);
    void Guardar(Pedido pedido);
}
