using VentasShop.Dominio.Enumeraciones;
using VentasShop.Dominio.ObjetosValor;
using VentasShop.Dominio.Precios;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M4.2 — Tests parametrizados. Las CUATRO formas de dar datos a un <c>[Theory]</c>, una al lado de
/// otra, sobre la misma idea: separar el molde (la lógica del test) de las láminas (los datos).
///
/// Regla del curso (ver CalculadoraDescuentosTests y CantidadTests):
///  - <c>[InlineData]</c> solo admite constantes de compilación → enteros/enum. NO admite <c>decimal</c>.
///  - los importes <c>decimal</c> van por <c>[MemberData]</c> / <c>TheoryData&lt;&gt;</c> / <c>[ClassData]</c>.
/// El "antes" (un [Fact] por caso) está en PrimerasPruebasXunitTests; el "después" ya consolidado, aquí.
/// </summary>
public class ParametrizacionTests
{
    private readonly CalculadoraDescuentos _calculadora = new();

    // === 1) [InlineData]: constantes simples (enteros). Cada caso = un test independiente. ===
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void Cantidad_ConValorPositivo_GuardaElValor(int valor)
    {
        var cantidad = new Cantidad(valor);

        Assert.Equal(valor, cantidad.Valor);
    }

    // === 2) [MemberData]: desde un metodo estatico. Aqui SI caben decimales (es codigo C# normal). ===
    [Theory]
    [MemberData(nameof(CasosDescuento))]
    public void CalcularTasaDescuento_ConMemberData(decimal total, TipoCliente tipo, decimal esperado)
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(total, tipo);

        Assert.Equal(esperado, tasa);
    }

    public static IEnumerable<object[]> CasosDescuento =>
    [
        // total, tipo,                 esperado
        [50m,  TipoCliente.Estandar, 0.00m],
        [200m, TipoCliente.Premium,  0.08m],   // 5% volumen + 3% premium
        [800m, TipoCliente.Vip,      0.15m],   // tope (BR-05)
    ];

    // === 3) TheoryData<>: lo mismo, pero TIPADO (el compilador te cubre). La opcion por defecto. ===
    [Theory]
    [MemberData(nameof(CasosDescuentoTipado))]
    public void CalcularTasaDescuento_ConTheoryData(decimal total, TipoCliente tipo, decimal esperado)
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(total, tipo);

        Assert.Equal(esperado, tasa);
    }

    public static TheoryData<decimal, TipoCliente, decimal> CasosDescuentoTipado => new()
    {
        { 50m,  TipoCliente.Estandar, 0.00m },
        { 200m, TipoCliente.Premium,  0.08m },
        { 800m, TipoCliente.Vip,      0.15m },
    };

    // === 4) [ClassData]: los datos viven en su propia clase (reutilizable entre clases de test). ===
    [Theory]
    [ClassData(typeof(CasosFronteraDescuento))]
    public void CalcularTasaDescuento_ConClassData(decimal total, TipoCliente tipo, decimal esperado)
    {
        decimal tasa = _calculadora.CalcularTasaDescuento(total, tipo);

        Assert.Equal(esperado, tasa);
    }
}

/// <summary>
/// Fuente de datos para <c>[ClassData]</c>: una clase que hereda de <c>TheoryData&lt;&gt;</c>. Es la
/// cuarta forma: cuando los casos se comparten entre varias clases de test o merecen su propio archivo.
/// Aqui, los valores frontera de los tramos de descuento (analisis de valores limite, M2.2).
/// </summary>
public class CasosFronteraDescuento : TheoryData<decimal, TipoCliente, decimal>
{
    public CasosFronteraDescuento()
    {
        Add(99.99m,  TipoCliente.Estandar, 0.00m);   // justo por debajo del primer salto
        Add(100.00m, TipoCliente.Estandar, 0.05m);   // el limite de los 100
        Add(499.99m, TipoCliente.Estandar, 0.05m);   // justo por debajo del segundo salto
        Add(500.00m, TipoCliente.Estandar, 0.10m);   // el limite de los 500
    }
}
