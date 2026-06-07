using VentasShop.Dominio.Abstracciones;
using VentasShop.Dominio.Entidades;

namespace VentasShop.Infraestructura.Persistencia;

/// <summary>Implementación real del repositorio de pedidos sobre EF Core (Módulo 6).</summary>
public class RepositorioPedidos : IRepositorioPedidos
{
    private readonly ContextoVentasShop _contexto;

    public RepositorioPedidos(ContextoVentasShop contexto) => _contexto = contexto;

    public Pedido? ObtenerPorId(Guid id) => _contexto.Pedidos.Find(id);

    public void Agregar(Pedido pedido)
    {
        _contexto.Pedidos.Add(pedido);
        _contexto.SaveChanges();
    }

    public void Guardar(Pedido pedido)
    {
        _contexto.Pedidos.Update(pedido);
        _contexto.SaveChanges();
    }
}
