# VentasShop · M3.2 — Convenciones de nombrado de tests

> Rama `module-03.2/nombrado`. Checkpoint del curso **TESTNET**. Aquí aprendes a **nombrar** los tests
> para que, al fallar, te digan qué se rompió sin abrir el código. Formato `Metodo_Escenario_ResultadoEsperado`.

## Qué hay en esta rama

- **[`material/labs/M3.2-renombrar-tests.md`](material/labs/M3.2-renombrar-tests.md)** — el lab del
  submódulo: una batería de cinco tests con nombres malos (vago, describe la implementación, uno que
  miente) para renombrar al formato y leer la lista resultante como especificación.
- **[`material/tarjetas/M3.2-nombrado.md`](material/tarjetas/M3.2-nombrado.md)** — tarjeta de 1 página.
- **[`MANUAL.md`](MANUAL.md)** — el nombre como titular, el formato y su conexión con AAA, el test como
  documentación viva, convenciones alternativas y los antipatrones de nombre.
- Los tests reales del repo ya siguen el formato y son el **modelo** del lab: `CalculadoraDescuentosTests.cs`,
  `PedidoEstadosTests.cs`, `EstructuraAaaTests.cs`. Léelos como spec.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (27/27). Este submódulo es **conceptual** (sobre nombres): no añade
tests nuevos al proyecto — el material es el lab de renombrado y la suite existente como ejemplo de
buenos nombres.

## Qué cubre (BR)

No añade reglas de negocio ni tests nuevos: usa la suite existente (descuento BR-01..BR-05, ciclo del
pedido) como ejemplo de nombres que se leen como especificación. El SUT no cambia.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `material/` → todo lo didáctico (tarjetas, labs).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-03.1/patron-aaa` → **`module-03.2/nombrado`** ← estás aquí → `module-03.3/builders-object-mother` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- El método del nombre arranca con su nombre real del código (`CalcularTasaDescuento`, `Pagar`, `Enviar`).
  El ejemplo `Should_…` del temario se deja en inglés a propósito (ilustra la convención inglesa ajena).
- Construir los datos sin que el Arrange ahogue el test (Builders / Object Mother) es **M3.3**, que cierra
  el módulo.
