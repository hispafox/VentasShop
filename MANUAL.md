# Manual del alumno — M5.3 · Aserciones fluidas con AwesomeAssertions

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué casi todos los equipos
acaban dando el salto de `Assert.Equal` a `Should().Be(...)`, y por qué la razón de fondo no es la estética.

Tiempo de lectura: ~12 min. Submódulo M5.3. **Cierra el Módulo 5.**

---

## 1. La idea en una frase

Una aserción fluida no solo se lee mejor: cuando falla, te cuenta qué esperaba y qué obtuvo con detalle. Y
ese mensaje, multiplicado por cada fallo de la vida del proyecto, es lo que de verdad acelera el diagnóstico.

---

## 2. El análisis de sangre

Imagina dos análisis. El primero dice "anormal". El segundo, "colesterol 240; lo normal está por debajo de
200". Los dos detectan el problema; solo el segundo te da el diagnóstico en la misma línea. `Assert.True(...)`
al fallar dice "esperaba True, obtuve False" — el "anormal". La aserción fluida te da el número.

---

## 3. La diferencia real: el mensaje de fallo

Mira [`tests/.../AsercionesFluidasTests.cs`](tests/VentasShop.TestsUnitarios/AsercionesFluidasTests.cs).
`pedido.Lineas.Should().HaveCount(3)`, cuando falla, dice: "Expected pedido.Lineas to contain 3 item(s),
but found 2." Te nombra qué colección, cuántos esperaba y cuántos había, sin escribir nada extra. Ahí está
la ganancia: no en cómo se lee al escribir, sino en la calidad del diagnóstico al fallar.

---

## 4. El repertorio

Siempre empieza con `valor.Should()` y encadenas: `Be(0.10m)`, `BeGreaterThan(0)`, `NotBeNull()`,
`StartWith("Cliente")`, `HaveCount(3)`, `NotBeEmpty()`. Las de colección son donde el nativo se queda corto:
`Contain(l => l.Cantidad.Valor > 1)` y `AllSatisfy(l => l.Subtotal.Importe.Should().BePositive())` te ahorran
un bucle en el test (lógica en el test, antipatrón de M3.1) y, al fallar, te dicen qué elemento no cumplía.

---

## 5. La joya: `BeEquivalentTo`

Comparar un objeto entero campo por campo es tedioso y frágil. `real.Should().BeEquivalentTo(esperado)` lo
hace en una línea, recorriendo todo el grafo, y al fallar te dice qué propiedad de qué objeto difería. Un
detalle de VentasShop: el `Pedido`, sus líneas y el cliente llevan un `Id` que se autogenera (`Guid`), así
que si construyes el esperado y el real por separado, esos Id no coinciden. Se excluyen:
`opc => opc.Excluding(m => m.Name == "Id")`. Brilla en integración (M6); para una sola propiedad, comprueba
esa propiedad.

---

## 6. Excepciones fluidas y `.Because()`

La versión fluida de `Assert.Throws` (M4.3) encadena el mensaje:
`pedido.Invoking(p => p.Pagar()).Should().Throw<PedidoSinLineasException>().WithMessage("*sin líneas*")`. El
`*` comprueba el fragmento, no el texto exacto. Y con `.Because()` añades el motivo de una aserción, que
aparece en el fallo: `pedido.Estado.Should().Be(EstadoPedido.Pagado, "porque la pasarela aceptó el pago")`.

---

## 7. Por qué AwesomeAssertions

FluentAssertions, la librería clásica, pasó a licencia de pago en su versión 8 (enero de 2025).
**AwesomeAssertions** es un fork comunitario y gratuito de la última versión libre, con un API idéntico:
todo lo de aquí vale igual. Solo cambia el paquete NuGet y el namespace (`using AwesomeAssertions;`). Si usas
FluentAssertions o Shouldly, los conceptos se transfieren.

---

## 8. Errores comunes

**Aserciones débiles más bonitas** (`BeGreaterThan(0)` comprueba casi nada → usa el valor exacto). **Abusar
de `BeEquivalentTo`** (comparar el objeto entero cuando el test va de una propiedad). **Encadenar de más**
(si no cabe de un vistazo, pártelo). **Mezclar estilos** en el proyecto (elige uno y sé consistente).

---

## 9. Lo que te llevas (y cierre del Módulo 5)

El laboratorio ([`material/labs/M5.3-aserciones-fluidas.md`](material/labs/M5.3-aserciones-fluidas.md)) te
hace reescribir aserciones de M4/M5.2 a fluidas y **provocar fallos** para comparar los mensajes. La tarjeta
([`material/tarjetas/M5.3-awesomeassertions.md`](material/tarjetas/M5.3-awesomeassertions.md)) lo resume.

Con esto cierras el Módulo 5: testeas código con dependencias, aislándolo con dobles (M5.1), manejándolos
con NSubstitute (M5.2) y con aserciones que, al fallar, te cuentan qué pasó. Lo que falta es comprobar que
las piezas encajan de verdad contra una base de datos real: los tests de integración, el Módulo 6.
