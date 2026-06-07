using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.Dominio.Entidades;

/// <summary>Línea de pedido: un producto y su cantidad. Calcula su subtotal.</summary>
public class LineaPedido
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Producto Producto { get; private set; }
    public Cantidad Cantidad { get; private set; }

    private LineaPedido() => Producto = null!; // para EF Core

    public LineaPedido(Producto producto, Cantidad cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }

    public Dinero Subtotal => Producto.PrecioUnitario * Cantidad.Valor;
}
