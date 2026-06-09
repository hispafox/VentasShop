using Microsoft.EntityFrameworkCore;
using VentasShop.Dominio.Abstracciones;
using VentasShop.Dominio.Entidades;

namespace VentasShop.Infraestructura.Persistencia;

/// <summary>Implementación real del repositorio de pedidos sobre EF Core (Módulo 6).</summary>
public class RepositorioPedidos : IRepositorioPedidos
{
    private readonly ContextoVentasShop _contexto;

    public RepositorioPedidos(ContextoVentasShop contexto) => _contexto = contexto;

    // Find: trae el pedido por su clave, pero NO carga las navegaciones (Lineas, Cliente).
    public Pedido? ObtenerPorId(Guid id) => _contexto.Pedidos.Find(id);

    // Con Include: trae el pedido con sus lineas y su cliente cargados (M6.4).
    public Pedido? ObtenerConLineas(Guid id) =>
        _contexto.Pedidos
            .Include(p => p.Lineas)
            .Include(p => p.Cliente)
            .SingleOrDefault(p => p.Id == id);

    public IReadOnlyList<Pedido> ObtenerPorCliente(Guid clienteId) =>
        _contexto.Pedidos
            .Include(p => p.Cliente)
            .Where(p => p.Cliente.Id == clienteId)
            .ToList();

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

    public void Eliminar(Pedido pedido)
    {
        _contexto.Pedidos.Remove(pedido);
        _contexto.SaveChanges();
    }
}
