using Microsoft.Extensions.Logging;
using NSubstitute;
using VentasShop.Aplicacion;
using VentasShop.Dominio.Abstracciones;
using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Excepciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.TestsUnitarios.Builders;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M5.2 — Mocking con NSubstitute. Los mismos casos del ServicioPedidos que en M5.1 se montaban a mano,
/// ahora con la librería: <c>Substitute.For</c> crea el doble, <c>Returns</c> le da el guion (stub) y
/// <c>Received</c>/<c>DidNotReceive</c> revisan la toma (mock). Dos gestos: configurar antes, verificar
/// después. Assert nativo para el estado (las aserciones fluidas llegan en M5.3).
/// </summary>
public class MockingNSubstituteTests
{
    private static readonly DateTimeOffset InstanteFijo = new(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Pagar_PedidoValidoYPasarelaAcepta_MarcaPagadoYGuarda()
    {
        // Arrange: un doble por dependencia. Returns = el guion (stub).
        var repositorio = Substitute.For<IRepositorioPedidos>();
        var pasarela = Substitute.For<IPasarelaPago>();
        var reloj = Substitute.For<IReloj>();
        var registro = Substitute.For<ILogger<ServicioPedidos>>();

        var pedido = new PedidoBuilder().Confirmado().ConLinea(100m, 1).Build();
        repositorio.ObtenerPorId(pedido.Id).Returns(pedido);
        pasarela.Cobrar(Arg.Any<Dinero>(), Arg.Any<string>()).Returns(new ResultadoPago(true));
        reloj.Ahora.Returns(InstanteFijo);

        var servicio = new ServicioPedidos(repositorio, pasarela, reloj, registro);

        // Act
        servicio.Pagar(pedido.Id);

        // Assert: estado (lo preferible) + interacción (Received = revisar la toma).
        Assert.Equal(EstadoPedido.Pagado, pedido.Estado);
        repositorio.Received(1).Guardar(pedido);
        pasarela.Received(1).Cobrar(pedido.CalcularTotal(), Arg.Any<string>());
    }

    [Fact]
    public void Pagar_PasarelaRechaza_NoAvanzaNiGuarda()
    {
        var repositorio = Substitute.For<IRepositorioPedidos>();
        var pasarela = Substitute.For<IPasarelaPago>();
        var pedido = new PedidoBuilder().Confirmado().ConLinea(100m, 1).Build();
        repositorio.ObtenerPorId(pedido.Id).Returns(pedido);
        pasarela.Cobrar(Arg.Any<Dinero>(), Arg.Any<string>()).Returns(new ResultadoPago(false)); // BR-09

        var servicio = new ServicioPedidos(repositorio, pasarela,
            Substitute.For<IReloj>(), Substitute.For<ILogger<ServicioPedidos>>());

        servicio.Pagar(pedido.Id);

        // Pagar no devuelve nada: la prueba del rechazo TIENE que ser de interacción.
        Assert.Equal(EstadoPedido.Confirmado, pedido.Estado);
        repositorio.DidNotReceive().Guardar(Arg.Any<Pedido>());
    }

    [Fact]
    public void Pagar_GuardaElPedidoYaEnEstadoPagado_VerificandoElArgumento()
    {
        var repositorio = Substitute.For<IRepositorioPedidos>();
        var pasarela = Substitute.For<IPasarelaPago>();
        var pedido = new PedidoBuilder().Confirmado().ConLinea(100m, 1).Build();
        repositorio.ObtenerPorId(pedido.Id).Returns(pedido);
        pasarela.Cobrar(Arg.Any<Dinero>(), Arg.Any<string>()).Returns(new ResultadoPago(true));

        var servicio = new ServicioPedidos(repositorio, pasarela,
            Substitute.For<IReloj>(), Substitute.For<ILogger<ServicioPedidos>>());

        servicio.Pagar(pedido.Id);

        // No solo QUE se guardó, sino CON QUÉ: el pedido tenía que estar ya pagado.
        repositorio.Received(1).Guardar(Arg.Is<Pedido>(p => p.Estado == EstadoPedido.Pagado));
    }

    [Fact]
    public void Pagar_PedidoInexistente_LanzaSinIntentarCobrar()
    {
        // El doble del repositorio es permisivo: ObtenerPorId devuelve null por defecto.
        var repositorio = Substitute.For<IRepositorioPedidos>();
        var pasarela = Substitute.For<IPasarelaPago>();
        var servicio = new ServicioPedidos(repositorio, pasarela,
            Substitute.For<IReloj>(), Substitute.For<ILogger<ServicioPedidos>>());

        Assert.Throws<InvalidOperationException>(() => servicio.Pagar(Guid.NewGuid()));
        pasarela.DidNotReceive().Cobrar(Arg.Any<Dinero>(), Arg.Any<string>());
    }

    [Fact]
    public void Pagar_PedidoSinLineas_LanzaPedidoSinLineasException()
    {
        // El dominio hace cumplir BR-07 al llamar a Pagar() (M4.3), aun pasando por el servicio.
        var repositorio = Substitute.For<IRepositorioPedidos>();
        var pasarela = Substitute.For<IPasarelaPago>();
        var pedido = new PedidoBuilder().SinLineas().Build();   // Borrador, sin líneas
        repositorio.ObtenerPorId(pedido.Id).Returns(pedido);
        pasarela.Cobrar(Arg.Any<Dinero>(), Arg.Any<string>()).Returns(new ResultadoPago(true));

        var servicio = new ServicioPedidos(repositorio, pasarela,
            Substitute.For<IReloj>(), Substitute.For<ILogger<ServicioPedidos>>());

        Assert.Throws<PedidoSinLineasException>(() => servicio.Pagar(pedido.Id));
        repositorio.DidNotReceive().Guardar(Arg.Any<Pedido>());
    }
}
