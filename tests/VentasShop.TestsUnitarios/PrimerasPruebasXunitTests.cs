using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.Dominio.Precios;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M4.1 — Introducción a xUnit. El primer contacto con la herramienta: un test es un método público
/// marcado con <c>[Fact]</c>, y se comprueba con la clase <c>Assert</c> nativa (las fluidas llegan en M5.3).
///
/// Aquí los tres tramos van como <c>[Fact]</c> SEPARADOS a propósito: es justo el tedio de copiar y pegar
/// cambiando dos números que detectamos en M2.2. La respuesta elegante —un solo método con varios casos,
/// <c>[Theory]</c>— es M4.2, y la ves aplicada en <see cref="CalculadoraDescuentosTests"/>. Compara los dos
/// ficheros: este es el "antes" (un Fact por caso); aquel, el "después" (un Theory con la tabla de casos).
/// </summary>
public class PrimerasPruebasXunitTests
{
    // === El primer [Fact]: el del capítulo, con AAA a la vista ===
    [Fact]
    public void CalcularTasaDescuento_PedidoDe500ClienteEstandar_Aplica10PorCiento()
    {
        // Arrange
        var calculadora = new CalculadoraDescuentos();

        // Act
        decimal tasa = calculadora.CalcularTasaDescuento(500m, TipoCliente.Estandar);

        // Assert
        Assert.Equal(0.10m, tasa);   // esperado primero, real despues
    }

    // === Los tres tramos, un [Fact] por caso (el "antes" de M4.2) ===
    [Fact]
    public void CalcularTasaDescuento_TramoAlto_Da10PorCiento()
    {
        var calculadora = new CalculadoraDescuentos();

        decimal tasa = calculadora.CalcularTasaDescuento(800m, TipoCliente.Estandar);

        Assert.Equal(0.10m, tasa);
    }

    [Fact]
    public void CalcularTasaDescuento_TramoMedio_Da5PorCiento()
    {
        var calculadora = new CalculadoraDescuentos();

        decimal tasa = calculadora.CalcularTasaDescuento(200m, TipoCliente.Estandar);

        Assert.Equal(0.05m, tasa);
    }

    [Fact]
    public void CalcularTasaDescuento_TramoBajo_NoAplicaDescuento()
    {
        var calculadora = new CalculadoraDescuentos();

        decimal tasa = calculadora.CalcularTasaDescuento(50m, TipoCliente.Estandar);

        Assert.Equal(0m, tasa);
    }

    // === El repertorio de Assert: usa la aserción mas especifica que encaje ===
    // No 'Assert.True(pedido.Lineas.Count == 1)' (mensaje pobre), sino 'Assert.Single' (mensaje util).
    [Fact]
    public void AgregarLinea_PedidoConUnaLinea_DejaUnaSolaLinea()
    {
        var pedido = new Pedido(new Cliente { Nombre = "Test", Tipo = TipoCliente.Estandar });

        pedido.AgregarLinea(
            new Producto { Nombre = "Teclado", PrecioUnitario = new Dinero(50m, "EUR"), UnidadesStock = 100 },
            new Cantidad(2));

        Assert.Single(pedido.Lineas);
    }

    [Fact]
    public void PedidoRecienCreado_NoTieneLineas()
    {
        var pedido = new Pedido(new Cliente { Nombre = "Test", Tipo = TipoCliente.Estandar });

        // 'Assert.Empty' dice "esperaba coleccion vacia, tenia N"; 'Assert.True(...Count == 0)' no.
        Assert.Empty(pedido.Lineas);
    }

    [Fact]
    public void PedidoRecienCreado_EstaEnBorrador()
    {
        var pedido = new Pedido(new Cliente { Nombre = "Test", Tipo = TipoCliente.Estandar });

        Assert.Equal(EstadoPedido.Borrador, pedido.Estado);
    }
}
