using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.TestsUnitarios.Builders;

/// <summary>
/// Test Data Builder (M3.3) para <see cref="Pedido"/>: monta un pedido a medida encadenando decisiones,
/// con valores por defecto razonables. Cada test especifica solo lo que le importa; el resto lo pone el
/// Builder. Patrón de Nat Pryce. Se reutiliza sin re-explicar en M4-M6.
/// </summary>
public class PedidoBuilder
{
    private TipoCliente _tipo = TipoCliente.Estandar;
    private readonly List<(decimal precio, int cantidad)> _lineas = [];
    private EstadoPedido _estadoObjetivo = EstadoPedido.Borrador;

    public PedidoBuilder ParaVip()     { _tipo = TipoCliente.Vip; return this; }
    public PedidoBuilder ParaPremium() { _tipo = TipoCliente.Premium; return this; }
    public PedidoBuilder ConLinea(decimal precio, int cantidad) { _lineas.Add((precio, cantidad)); return this; }
    public PedidoBuilder SinLineas()   { _lineas.Clear(); return this; }
    public PedidoBuilder Confirmado()  { _estadoObjetivo = EstadoPedido.Confirmado; return this; }
    public PedidoBuilder Pagado()      { _estadoObjetivo = EstadoPedido.Pagado; return this; }

    public Pedido Build()
    {
        var pedido = new Pedido(new Cliente { Nombre = "Test", Tipo = _tipo });

        // Un pedido confirmado necesita al menos una linea (BR-07): si se pide un estado avanzado sin
        // lineas, el Builder pone una por defecto. Asi Confirmado()/Pagado() siempre construyen un
        // pedido valido sin que el test tenga que decir lo obvio. SinLineas() en estado Borrador deja
        // el pedido vacio a proposito (para el caso de "pagar sin lineas").
        List<(decimal precio, int cantidad)> lineas = _lineas;
        if (lineas.Count == 0 && _estadoObjetivo is not EstadoPedido.Borrador)
            lineas = [(50m, 1)];

        foreach (var (precio, cantidad) in lineas)
            pedido.AgregarLinea(
                new Producto { Nombre = "Test", PrecioUnitario = new Dinero(precio, "EUR"), UnidadesStock = 100 },
                new Cantidad(cantidad));

        if (_estadoObjetivo is EstadoPedido.Confirmado or EstadoPedido.Pagado) pedido.Confirmar();
        if (_estadoObjetivo is EstadoPedido.Pagado) pedido.Pagar();
        return pedido;
    }
}
