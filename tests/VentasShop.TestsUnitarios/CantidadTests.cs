using VentasShop.Dominio.ObjetosValor;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// Análisis de valores límite sobre la invariante de <see cref="Cantidad"/> (BR-08): el caso
/// interesante no es el centro (5), es la frontera (el 0, que distingue <c>&lt;= 0</c> de <c>&lt; 0</c>).
/// Con enteros, <c>[InlineData]</c> sí admite los valores; con decimales habría que usar TheoryData.
/// </summary>
public class CantidadTests
{
    [Fact]
    public void Cantidad_ConValorPositivo_SeCrea()
    {
        var cantidad = new Cantidad(1);   // el primer valor válido, justo en la frontera

        Assert.Equal(1, cantidad.Valor);
    }

    [Theory]
    [InlineData(0)]    // la frontera exacta
    [InlineData(-1)]
    public void Cantidad_ConCeroONegativo_LanzaExcepcion(int valor)
    {
        Assert.Throws<ArgumentException>(() => new Cantidad(valor));
    }
}
