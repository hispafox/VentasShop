using VentasShop.Dominio.Enumeraciones;

namespace VentasShop.Dominio.Entidades;

/// <summary>Cliente. Sus propiedades simples (Nombre) son el ejemplo de "qué NO testear" (getters).</summary>
public class Cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
    public TipoCliente Tipo { get; set; } = TipoCliente.Estandar;
}
