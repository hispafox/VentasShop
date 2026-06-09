# VentasShop · M5.3 — Aserciones fluidas con AwesomeAssertions

> Rama `module-05.3/aserciones-fluidas`. Checkpoint del curso **TESTNET**. El salto de `Assert.Equal` a
> `Should().Be(...)` con **AwesomeAssertions**: la ganancia real es el mensaje cuando el test falla.
> **Cierra el Módulo 5.**

## Qué hay en esta rama

- **AwesomeAssertions 9.4.0** en [`tests/.../VentasShop.TestsUnitarios.csproj`](tests/VentasShop.TestsUnitarios/VentasShop.TestsUnitarios.csproj) (`using AwesomeAssertions;`).
- **`tests/.../AsercionesFluidasTests.cs`** — 5 tests fluidos: valor exacto (`tasa.Should().Be(0.10m)`),
  colecciones (`HaveCount`/`Contain`/`AllSatisfy` sobre las líneas), `BeEquivalentTo` con `Excluding` de los
  Id autogenerados, excepción fluida (`Invoking(...).Should().Throw<PedidoSinLineasException>().WithMessage("*sin líneas*")`)
  y `.Because()` sobre el estado del pedido pagado.
- **[`MANUAL.md`](MANUAL.md)** — el análisis de sangre, el mensaje de fallo, el repertorio, `BeEquivalentTo`,
  excepciones fluidas, `.Because()`, por qué AwesomeAssertions y los errores comunes.
- **[`material/tarjetas/M5.3-awesomeassertions.md`](material/tarjetas/M5.3-awesomeassertions.md)** — tarjeta de 1 página.
- **[`material/labs/M5.3-aserciones-fluidas.md`](material/labs/M5.3-aserciones-fluidas.md)** — lab: reescribir a fluidas y comparar mensajes de fallo.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (73/73). `AsercionesFluidasTests.cs` añade 5 tests; el código de
producción no cambia.

## Lo que tienes que llevarte

- **El valor exacto:** `tasa.Should().Be(0.10m)`, no `BeGreaterThan(0)`.
- **Colecciones:** `pedido.Lineas.Should().AllSatisfy(l => l.Subtotal.Importe.Should().BePositive())`.
- **Objeto entero:** `real.Should().BeEquivalentTo(esperado, opc => opc.Excluding(m => m.Name == "Id"))`.
- **Excepción + mensaje:** `pedido.Invoking(p => p.Pagar()).Should().Throw<...>().WithMessage("*sin líneas*")`.

## El detalle de `BeEquivalentTo` con VentasShop

El `Pedido`, sus `LineaPedido` y el `Cliente` llevan un `Id` (`Guid.NewGuid()`) por instancia. Comparar dos
pedidos construidos por separado **fallaría** por esos Id. Por eso el test los excluye con
`Excluding(m => m.Name == "Id")` — justo el aviso de «abusar de `BeEquivalentTo`» del MANUAL, hecho código.

## Por qué AwesomeAssertions

FluentAssertions v8 (enero 2025) pasó a licencia de pago. AwesomeAssertions es el fork libre de la v7, con
API idéntico: el namespace es `AwesomeAssertions`, y todo lo demás es igual. Shouldly es la otra alternativa
gratuita; los conceptos se transfieren.

## Dónde estás en el curso

… → `module-05.2/mocking-nsubstitute` → **`module-05.3/aserciones-fluidas`** ← estás aquí (cierra el Módulo 5) → `module-06.1/...` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Cierra el Módulo 5 (dobles + NSubstitute + aserciones fluidas). El Módulo 6 es integración con piezas reales.
- `Invoking` es API válida; la forma alternativa es `Action acto = () => pedido.Pagar(); acto.Should().Throw<...>()`.
