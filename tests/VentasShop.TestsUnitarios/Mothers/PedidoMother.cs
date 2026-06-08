using VentasShop.Dominio.Entidades;
using VentasShop.TestsUnitarios.Builders;

namespace VentasShop.TestsUnitarios.Mothers;

/// <summary>
/// Object Mother (M3.3) para <see cref="Pedido"/>: los arquetipos que se repiten en la suite. Por dentro
/// usa el <see cref="PedidoBuilder"/> con valores por defecto — lo mejor de los dos patrones: la comodidad
/// del Mother para lo habitual y el Builder por debajo para construirlo.
/// </summary>
public static class PedidoMother
{
    public static Pedido Borrador()  => new PedidoBuilder().ConLinea(50m, 1).Build();
    public static Pedido Confirmado() => new PedidoBuilder().ConLinea(50m, 2).Confirmado().Build();
    public static Pedido Pagado()    => new PedidoBuilder().ConLinea(50m, 2).Pagado().Build();
    public static Pedido VipPagado() => new PedidoBuilder().ParaVip().ConLinea(300m, 2).Pagado().Build();
}
