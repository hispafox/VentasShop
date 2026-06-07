using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Excepciones;
using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// Técnica de transición de estados sobre el ciclo de vida del <see cref="Pedido"/>: se prueban las
/// transiciones válidas del camino principal y, sobre todo, las inválidas (que deben rechazarse).
/// El montaje del pedido es algo ruidoso aquí; en M3.3 se limpia con un builder.
/// </summary>
public class PedidoEstadosTests
{
    private static Pedido PedidoConfirmado()
    {
        var cliente = new Cliente { Nombre = "Cliente de prueba", Tipo = TipoCliente.Estandar };
        var producto = new Producto { Nombre = "Producto de prueba", PrecioUnitario = new Dinero(50m, "EUR") };
        var pedido = new Pedido(cliente);
        pedido.AgregarLinea(producto, new Cantidad(2));
        pedido.Confirmar();
        return pedido;
    }

    // --- Transiciones válidas (camino principal) ---

    [Fact]
    public void Pagar_PedidoConfirmado_PasaAPagado()
    {
        var pedido = PedidoConfirmado();

        pedido.Pagar();

        Assert.Equal(EstadoPedido.Pagado, pedido.Estado);
    }

    [Fact]
    public void Enviar_PedidoPagado_PasaAEnviado()
    {
        var pedido = PedidoConfirmado();
        pedido.Pagar();

        pedido.Enviar();

        Assert.Equal(EstadoPedido.Enviado, pedido.Estado);
    }

    // --- Transiciones inválidas (lo que de verdad importa de la técnica) ---

    [Fact]
    public void Enviar_PedidoSinPagar_LanzaTransicionInvalida()
    {
        var pedido = PedidoConfirmado(); // confirmado, todavía sin pagar

        Assert.Throws<TransicionPedidoInvalidaException>(() => pedido.Enviar());
    }

    [Fact]
    public void Cancelar_PedidoEnviado_LanzaTransicionInvalida()
    {
        var pedido = PedidoConfirmado();
        pedido.Pagar();
        pedido.Enviar();

        Assert.Throws<TransicionPedidoInvalidaException>(() => pedido.Cancelar());
    }

    // --- BR-07: no se puede pagar un pedido sin líneas ---

    [Fact]
    public void Pagar_PedidoSinLineas_LanzaPedidoSinLineas()
    {
        var pedido = new Pedido(new Cliente { Nombre = "Cliente de prueba" });

        Assert.Throws<PedidoSinLineasException>(() => pedido.Pagar());
    }
}
