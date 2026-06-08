using VentasShop.Dominio.Entidades;
using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.Dominio.Precios;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// El patrón Arrange-Act-Assert (M3.1): la misma estructura en tres golpes de vista —preparar, actuar,
/// comprobar—. Estos dos tests son el modelo de cómo se escribe a partir de aquí; el resto de la suite
/// ya lo sigue (ver <see cref="PedidoEstadosTests"/>, que parte el ciclo de un pedido en tests AAA en
/// vez de meterlo todo en uno). Los comentarios //Arrange //Act //Assert son un andamio para aprender:
/// con el patrón interiorizado, basta la línea en blanco entre bloques.
/// </summary>
public class EstructuraAaaTests
{
    // AAA canónico: nombre que habla (Método_Escenario_ResultadoEsperado, convención de M3.2) y las
    // tres fases separadas. Léelo de un vistazo: arriba qué tienes, en medio qué haces, abajo qué esperas.
    [Fact]
    public void CalcularTasaDescuento_PedidoDe500ClienteEstandar_Aplica10Porciento()
    {
        // Arrange
        var calculadora = new CalculadoraDescuentos();
        decimal totalPedido = 500m;
        var tipo = TipoCliente.Estandar;

        // Act
        decimal tasa = calculadora.CalcularTasaDescuento(totalPedido, tipo);

        // Assert
        Assert.Equal(0.10m, tasa);
    }

    // "Un assert por test" = un solo CONCEPTO, no una sola línea. Aquí el concepto es "el total salió
    // bien": dos comprobaciones (importe y moneda) que son facetas de lo mismo. Partirlo en dos tests
    // con el mismo Arrange y el mismo Act no aportaría nada.
    [Fact]
    public void CalcularTotal_PedidoConDosUnidadesDe50_Da100Euros()
    {
        // Arrange
        var pedido = new Pedido(new Cliente { Nombre = "Ana" });
        pedido.AgregarLinea(
            new Producto { Nombre = "Teclado", PrecioUnitario = new Dinero(50m, "EUR") },
            new Cantidad(2));

        // Act
        Dinero total = pedido.CalcularTotal();

        // Assert — un concepto, dos comprobaciones que lo forman
        Assert.Equal(100m, total.Importe);
        Assert.Equal("EUR", total.Moneda);
    }
}
