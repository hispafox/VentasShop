using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;

namespace VentasShop.TestsUnitarios.Mothers;

/// <summary>
/// Object Mother (M3.3) para <see cref="Cliente"/>: arquetipos fijos listos para servir. Un punto unico
/// de cambio para el atrezzo comun. Cuando hagan falta variaciones combinadas, migrar a un Builder.
/// </summary>
public static class ClienteMother
{
    public static Cliente Estandar() => new() { Nombre = "Cliente Estandar", Tipo = TipoCliente.Estandar };
    public static Cliente Premium()  => new() { Nombre = "Cliente Premium",  Tipo = TipoCliente.Premium };
    public static Cliente Vip()      => new() { Nombre = "Cliente VIP",      Tipo = TipoCliente.Vip };
}
