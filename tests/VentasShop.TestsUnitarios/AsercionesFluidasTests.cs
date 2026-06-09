using AwesomeAssertions;
using NSubstitute;
using Microsoft.Extensions.Logging;
using VentasShop.Aplicacion;
using VentasShop.Dominio.Abstracciones;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.Excepciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.Dominio.Precios;
using VentasShop.TestsUnitarios.Builders;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M5.3 — Aserciones fluidas con AwesomeAssertions. Las mismas comprobaciones de antes, ahora con
/// <c>Should()</c>: el valor (Be), las colecciones (HaveCount/Contain/AllSatisfy), el objeto entero
/// (BeEquivalentTo, excluyendo los Id autogenerados), las excepciones (Throw/WithMessage) y el porqué
/// (Because). La ganancia no es la legibilidad al escribir, es el mensaje cuando el test falla.
/// </summary>
public class AsercionesFluidasTests
{
    [Fact]
    public void TasaDescuento_PedidoGrandeEstandar_EsDiezPorCiento()
    {
        var tasa = new CalculadoraDescuentos().CalcularTasaDescuento(500m, TipoCliente.Estandar);

        tasa.Should().Be(0.10m);   // valor exacto, no BeGreaterThan(0)
    }

    [Fact]
    public void Pedido_Confirmado_TieneLineasPositivas()
    {
        var pedido = new PedidoBuilder().ConLinea(50m, 2).ConLinea(30m, 3).Confirmado().Build();

        pedido.Lineas.Should().HaveCount(2);
        pedido.Lineas.Should().Contain(l => l.Cantidad.Valor > 1);
        pedido.Lineas.Should().AllSatisfy(l => l.Subtotal.Importe.Should().BePositive());
    }

    [Fact]
    public void DosPedidosEquivalentes_SonIgualesSalvoLosIdAutogenerados()
    {
        var esperado = new PedidoBuilder().ParaVip().ConLinea(50m, 2).Build();
        var real = new PedidoBuilder().ParaVip().ConLinea(50m, 2).Build();

        // Sin Excluding fallaría: cada entidad lleva un Id (Guid) distinto por instancia.
        real.Should().BeEquivalentTo(esperado, opc => opc.Excluding(m => m.Name == "Id"));
    }

    [Fact]
    public void Pagar_PedidoSinLineas_LanzaConMensajeFluido()
    {
        var pedido = new PedidoBuilder().SinLineas().Build();

        pedido.Invoking(p => p.Pagar())
              .Should().Throw<PedidoSinLineasException>()
              .WithMessage("*sin líneas*");   // fragmento con comodín, no el texto exacto
    }

    [Fact]
    public void Pagar_PasarelaAcepta_DejaElPedidoPagado_ConPorque()
    {
        var repositorio = Substitute.For<IRepositorioPedidos>();
        var pasarela = Substitute.For<IPasarelaPago>();
        var pedido = new PedidoBuilder().Confirmado().ConLinea(100m, 1).Build();
        repositorio.ObtenerPorId(pedido.Id).Returns(pedido);
        pasarela.Cobrar(Arg.Any<Dinero>(), Arg.Any<string>()).Returns(new ResultadoPago(true));

        var servicio = new ServicioPedidos(repositorio, pasarela,
            Substitute.For<IReloj>(), Substitute.For<ILogger<ServicioPedidos>>());

        servicio.Pagar(pedido.Id);

        pedido.Estado.Should().Be(EstadoPedido.Pagado,
            "porque la pasarela aceptó el pago y el flujo debe marcarlo así");
    }
}
