using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.Dominio.Entidades;

/// <summary>Producto del catálogo.</summary>
public class Producto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    // Codigo de catalogo (SKU). Unico a nivel de base de datos: ver el indice unico en
    // ContextoVentasShop. Es la restriccion que el provider in-memory NO refuerza (M6.2) y que
    // SQL Server real si caza (M6.3).
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public Dinero PrecioUnitario { get; set; }
    public int UnidadesStock { get; set; }
}
