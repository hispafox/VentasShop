# VentasShop · M4.3 — Gestión de excepciones en tests

> Rama `module-04.3/excepciones`. Checkpoint del curso **TESTNET**. Aquí testeas que el código *falla bien*:
> `Assert.Throws`, verificar mensaje/propiedades, `Assert.ThrowsAsync` (y el `await` olvidado) y
> `Record.Exception`. **Cierra el Módulo 4.**

## Qué hay en esta rama

- **`tests/.../ExcepcionesTests.cs`** — 7 tests sobre las excepciones de dominio reales:
  `Assert.Throws<PedidoSinLineasException>`, `Assert.Throws<TransicionPedidoInvalidaException>` (con
  verificación de mensaje), la `ArgumentException` de `Cantidad` (`ParamName "valor"` + `Contains`), la
  `InvalidOperationException` de `Dinero`, `Assert.ThrowsAny`, `Record.Exception` (no lanza) y un
  `Assert.ThrowsAsync` autocontenido (la forma del async; el dominio es síncrono).
- **[`MANUAL.md`](MANUAL.md)** — el airbag/crash test, `Assert.Throws` (lambda + tipo exacto), verificar
  mensaje (`Contains` no `Equal`), el `await` olvidado, `Record.Exception` y qué excepciones testear.
- **[`material/tarjetas/M4.3-excepciones.md`](material/tarjetas/M4.3-excepciones.md)** — tarjeta de 1 página.
- **[`material/labs/M4.3-testear-excepciones.md`](material/labs/M4.3-testear-excepciones.md)** — lab:
  cubrir los caminos de error + la demo en vivo del `await` olvidado.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (59/59). `ExcepcionesTests.cs` añade 7 tests; el código de producción no
cambia.

## Las excepciones de dominio (lo que se testea)

En [`src/VentasShop.Dominio/Excepciones/`](src/VentasShop.Dominio/Excepciones/): `PedidoSinLineasException`
(BR-07) y `TransicionPedidoInvalidaException` (máquina de estados). Más la `ArgumentException` de `Cantidad`
(invariante > 0) y la `InvalidOperationException` de `Dinero` (no mezclar monedas). Son contrato: airbags
que el código dispara a propósito. Las de un bug interno no se testean.

## El `await` olvidado (la demo del módulo)

El test async del fichero usa `Assert.ThrowsAsync` con su `await`. En el lab lo quitas y compruebas que el
test pasa en verde sin comprobar nada (falso positivo silencioso), lo vuelves a poner y se pone rojo.
Regla: `await` siempre, `async Task` nunca `async void`. Analizador: **xUnit2021**.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `material/` → todo lo didáctico (tarjetas, labs).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-04.2/tests-parametrizados` → **`module-04.3/excepciones`** ← estás aquí (cierra el Módulo 4) → `module-05.1/test-doubles` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- El ejemplo async es **autocontenido** (operación local que falla): el dominio de VentasShop es síncrono;
  el `ServicioPedidos` async con pasarela se construye en M5.
- Solo se testean las **excepciones de dominio** (contrato), no las de un bug interno.
