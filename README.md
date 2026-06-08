# VentasShop · M3.3 — Fixtures, Builders y Object Mother

> Rama `module-03.3/builders-object-mother`. Checkpoint del curso **TESTNET**. Aquí construyes el
> **atrezzo de test** —Object Mother y Test Data Builder— para que el Arrange deje de ahogar los tests.
> Cierra el Módulo 3 y la base conceptual del curso.

## Qué hay en esta rama

- **`tests/.../Builders/PedidoBuilder.cs`** — el Test Data Builder de `Pedido`: interfaz fluida
  (`ParaVip()`, `ConLinea()`, `SinLineas()`, `Confirmado()`, `Pagado()`, `Build()`) con valores por
  defecto razonables. Es el `PedidoBuilder` que M3.1 y M3.2 anticipaban; aquí existe de verdad.
- **`tests/.../Mothers/ClienteMother.cs`** — Object Mother de `Cliente` (arquetipos `Estandar`/`Premium`/`Vip`).
- **`tests/.../Mothers/PedidoMother.cs`** — Object Mother de `Pedido` que por dentro usa el `PedidoBuilder`.
- **`tests/.../ConstruccionDatosTests.cs`** — los mismos comportamientos del ciclo del pedido, ahora con
  el Arrange limpio gracias al atrezzo. Compara con `PedidoEstadosTests.cs` (montaje a mano).
- **[`MANUAL.md`](MANUAL.md)** — el atrezzo del teatro, Object Mother vs Builder, setup/teardown en xUnit
  y el peligro del estado compartido.
- **[`material/tarjetas/M3.3-builders.md`](material/tarjetas/M3.3-builders.md)** — tarjeta de 1 página.
- **[`material/labs/M3.3-object-mother-builder.md`](material/labs/M3.3-object-mother-builder.md)** — lab:
  construir el atrezzo y reescribir un Arrange enredado con él.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (32/32). `ConstruccionDatosTests.cs` añade 5 tests que usan el atrezzo;
el código de producción no cambia.

## Qué cubre (BR)

No añade reglas de negocio: reusa el ciclo del pedido (confirmar/pagar/enviar, BR-07) y el cálculo del
total para enseñar **construcción de datos de test**. El SUT no cambia; lo que crece es el atrezzo de la
suite, reutilizable en M4-M6.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `tests/Builders/` y `tests/Mothers/` → el atrezzo de test.
  `material/` → todo lo didáctico (tarjetas, labs).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-03.2/nombrado` → **`module-03.3/builders-object-mother`** ← estás aquí (cierra el Módulo 3) → `module-04.1/introduccion-xunit` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- El `PedidoBuilder`/`ClienteMother`/`PedidoMother` son **plantillas reutilizables**: se usan sin
  re-explicar en los Módulos 4, 5 y 6.
- El detalle del estado compartido (`IClassFixture` con BD real) se cosecha en **M6**; aquí queda sembrado.
