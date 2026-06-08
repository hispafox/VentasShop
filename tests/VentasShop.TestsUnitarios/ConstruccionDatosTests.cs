using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Excepciones;
using VentasShop.TestsUnitarios.Builders;
using VentasShop.TestsUnitarios.Mothers;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M3.3: los mismos comportamientos del ciclo del pedido, ahora con el Arrange limpio gracias al
/// <see cref="PedidoBuilder"/> y a los Object Mothers. Compara con <see cref="PedidoEstadosTests"/>
/// (que monta el pedido a mano): misma cobertura, mucho menos ruido. A la vista queda solo la condicion
/// del test; el resto lo esconde el atrezzo.
/// </summary>
public class ConstruccionDatosTests
{
    [Fact]
    public void Pagar_PedidoConfirmado_PasaAPagado()
    {
        // Arrange — una linea que se lee: "un pedido confirmado"
        var pedido = new PedidoBuilder().Confirmado().Build();

        // Act
        pedido.Pagar();

        // Assert
        Assert.Equal(EstadoPedido.Pagado, pedido.Estado);
    }

    [Fact]
    public void Enviar_PedidoSinPagar_LanzaTransicionInvalida()
    {
        var pedido = new PedidoBuilder().Confirmado().Build();   // confirmado, todavia sin pagar

        Assert.Throws<TransicionPedidoInvalidaException>(() => pedido.Enviar());
    }

    [Fact]
    public void Pagar_PedidoSinLineas_LanzaPedidoSinLineas()
    {
        // SinLineas() en estado borrador deja el pedido vacio a proposito (no inyecta linea por defecto)
        var pedido = new PedidoBuilder().SinLineas().Build();

        Assert.Throws<PedidoSinLineasException>(() => pedido.Pagar());
    }

    [Fact]
    public void CalcularTotal_PedidoVipConDosUnidadesDe300_Da600Euros()
    {
        // Arrange — el Mother da el arquetipo; al test solo le importa que el total salga bien
        var pedido = PedidoMother.VipPagado();

        // Act
        var total = pedido.CalcularTotal();

        // Assert — un concepto, dos comprobaciones que lo forman
        Assert.Equal(600m, total.Importe);
        Assert.Equal("EUR", total.Moneda);
    }

    [Fact]
    public void Vip_ClienteMother_DaUnClienteVip()
    {
        var cliente = ClienteMother.Vip();

        Assert.Equal(TipoCliente.Vip, cliente.Tipo);
    }
}
