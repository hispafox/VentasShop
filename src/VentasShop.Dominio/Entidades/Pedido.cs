using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Excepciones;
using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.Dominio.Entidades;

/// <summary>
/// Raíz del agregado pedido. Encapsula la máquina de estados:
/// Borrador → Confirmado → Pagado → Enviado → Entregado, con Cancelar válido hasta Pagado.
/// Las transiciones ilegales lanzan <see cref="TransicionPedidoInvalidaException"/>.
/// </summary>
public class Pedido
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Cliente Cliente { get; private set; }
    public EstadoPedido Estado { get; private set; } = EstadoPedido.Borrador;

    private readonly List<LineaPedido> _lineas = new();
    public IReadOnlyList<LineaPedido> Lineas => _lineas;

    private Pedido() => Cliente = null!; // para EF Core

    public Pedido(Cliente cliente) => Cliente = cliente;

    public void AgregarLinea(Producto producto, Cantidad cantidad)
    {
        if (Estado != EstadoPedido.Borrador)
            throw new TransicionPedidoInvalidaException(Estado, "agregar línea");
        _lineas.Add(new LineaPedido(producto, cantidad));
    }

    public Dinero CalcularTotal()
    {
        if (_lineas.Count == 0) return new Dinero(0m, "EUR");
        var total = _lineas[0].Subtotal;
        for (int i = 1; i < _lineas.Count; i++)
            total += _lineas[i].Subtotal;
        return total;
    }

    public void Confirmar()
    {
        if (Estado != EstadoPedido.Borrador)
            throw new TransicionPedidoInvalidaException(Estado, "confirmar");
        if (_lineas.Count == 0)
            throw new PedidoSinLineasException();
        Estado = EstadoPedido.Confirmado;
    }

    public void Pagar()
    {
        if (_lineas.Count == 0)
            throw new PedidoSinLineasException(); // BR-07
        if (Estado != EstadoPedido.Confirmado)
            throw new TransicionPedidoInvalidaException(Estado, "pagar");
        Estado = EstadoPedido.Pagado;
    }

    public void Enviar()
    {
        if (Estado != EstadoPedido.Pagado)
            throw new TransicionPedidoInvalidaException(Estado, "enviar");
        Estado = EstadoPedido.Enviado;
    }

    public void Entregar()
    {
        if (Estado != EstadoPedido.Enviado)
            throw new TransicionPedidoInvalidaException(Estado, "entregar");
        Estado = EstadoPedido.Entregado;
    }

    public void Cancelar()
    {
        if (Estado is EstadoPedido.Enviado or EstadoPedido.Entregado or EstadoPedido.Cancelado)
            throw new TransicionPedidoInvalidaException(Estado, "cancelar");
        Estado = EstadoPedido.Cancelado;
    }
}
