# VentasShop · M5.2 — Mocking con NSubstitute

> Rama `module-05.2/mocking-nsubstitute`. Checkpoint del curso **TESTNET**. Aquí los dobles que en M5.1
> escribías a mano se crean con **NSubstitute**: `Substitute.For` + `Returns` (configurar) + `Received`
> (verificar). Se testea el `ServicioPedidos` de punta a punta. Assert nativo (las fluidas son M5.3).

## Qué hay en esta rama

- **NSubstitute 5.3.0** en [`tests/.../VentasShop.TestsUnitarios.csproj`](tests/VentasShop.TestsUnitarios/VentasShop.TestsUnitarios.csproj).
- **`tests/.../MockingNSubstituteTests.cs`** — 5 tests del `ServicioPedidos.Pagar` con dobles de NSubstitute:
  caso feliz (estado + `Received` de `Guardar`/`Cobrar`), rechazo (BR-09: `DidNotReceive().Guardar`),
  verificación de argumento (`Arg.Is<Pedido>(p => p.Estado == Pagado)`), pedido inexistente (no se cobra) y
  pedido sin líneas (`PedidoSinLineasException`, BR-07).
- **[`MANUAL.md`](MANUAL.md)** — los dos gestos del director, `Substitute.For`, `Returns`/`Arg`, `Received`/
  `DidNotReceive`, estado vs interacción, permisivo vs estricto y los errores comunes.
- **[`material/tarjetas/M5.2-nsubstitute.md`](material/tarjetas/M5.2-nsubstitute.md)** — tarjeta de 1 página + tabla Moq→NSubstitute.
- **[`material/labs/M5.2-mockear-servicio-pedidos.md`](material/labs/M5.2-mockear-servicio-pedidos.md)** — lab: testear el servicio en sus 3 casos.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (68/68). `MockingNSubstituteTests.cs` añade 5 tests; el código de
producción no cambia.

## Los dos gestos (lo que tienes que llevarte)

- **Configurar (stub):** `repositorio.ObtenerPorId(id).Returns(pedido)` · `pasarela.Cobrar(Arg.Any<Dinero>(), Arg.Any<string>()).Returns(new ResultadoPago(true))`.
- **Verificar (mock):** `repositorio.Received(1).Guardar(pedido)` · `repositorio.DidNotReceive().Guardar(Arg.Any<Pedido>())`.
- **Argumento:** `repositorio.Received(1).Guardar(Arg.Is<Pedido>(p => p.Estado == EstadoPedido.Pagado))`.

## De M5.1 a M5.2

En M5.1 los dobles estaban escritos a mano en `tests/.../Dobles/` (sin librería). Aquí se crean con una
línea de NSubstitute. Compara los dos tests del caso feliz —`DoblesArtesanalesTests` y
`MockingNSubstituteTests`— y verás el andamiaje que la librería te quita.

## Permisivo por defecto

NSubstitute es *loose*: un método sin configurar devuelve el valor por defecto y no falla. Cómodo, pero
ojo con el `NullReferenceException` confuso si tu código esperaba algo que no configuraste. Moq tenía un
modo estricto; la tabla del MANUAL/tarjeta traduce Moq→NSubstitute casi línea a línea.

## Dónde estás en el curso

… → `module-05.1/test-doubles` → **`module-05.2/mocking-nsubstitute`** ← estás aquí → `module-05.3/aserciones-fluidas` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Assert nativo para el estado; las aserciones fluidas de AwesomeAssertions llegan en M5.3.
- `Throws` de NSubstitute vive en `using NSubstitute.ExceptionExtensions;`.
