using VentasShop.Dominio.Precios;
using VentasShop.TestsUnitarios.Dobles;

namespace VentasShop.TestsUnitarios;

/// <summary>
/// M7.2 · Tests inestables (flaky) — EL RELOJ TRAIDOR.
///
/// La causa más frecuente de inestabilidad es el tiempo: un test que lee el reloj del sistema
/// (DateTime.Now / DateTimeOffset.Now) pasa unas veces sí y otras no según cuándo se ejecuta.
/// <see cref="Cupon"/> (BR-06) depende de la fecha, así que es el caso perfecto.
///
/// La CURA es no leer el reloj del sistema, sino fijar "ahora" con el doble <see cref="RelojFijo"/>
/// (IReloj, M5.1). El test se vuelve determinista: pasa siempre o falla siempre, por una razón.
/// </summary>
public class RelojTraidorTests
{
    // ✅ DETERMINISTA: "ahora" lo fija el test, no el sistema. Pasa hoy, mañana y dentro de un año.
    [Fact]
    public void Cupon_VigenteSiAhoraEsAntesDeExpirar_ConRelojFijo()
    {
        var cupon = new Cupon("VERANO", new DateTimeOffset(2026, 6, 30, 23, 59, 59, TimeSpan.Zero));

        var antes = new RelojFijo(new DateTimeOffset(2026, 6, 15, 12, 0, 0, TimeSpan.Zero));
        Assert.True(cupon.EsValidoEn(antes.Ahora));

        var despues = new RelojFijo(new DateTimeOffset(2026, 7, 1, 0, 0, 1, TimeSpan.Zero));
        Assert.False(cupon.EsValidoEn(despues.Ahora));
    }

    // ❌ ANTI-EJEMPLO INESTABLE (a propósito, por eso está Skip): usa el reloj REAL del sistema.
    // Hoy pasa; el día que la máquina de CI tenga la fecha posterior a ExpiraEn, se pondrá rojo
    // sin que nadie toque el código ("funciona en junio, peta en julio"). Lo dejamos Skip para no
    // pudrir la suite; el lab pide reproducirlo y curarlo pasándolo al RelojFijo de arriba.
    [Fact(Skip = "Anti-ejemplo M7.2: depende del reloj del sistema (bomba de relojería). La cura es IReloj/RelojFijo. Ver MANUAL.")]
    public void Cupon_VigenteHoy_ConRelojDelSistema_FRAGIL()
    {
        var cupon = new Cupon("VERANO", new DateTimeOffset(2026, 6, 30, 23, 59, 59, TimeSpan.Zero));
        Assert.True(cupon.EsValidoEn(DateTimeOffset.Now));   // pasa hoy, fallará tras la caducidad
    }
}
