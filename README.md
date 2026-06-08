# VentasShop · M3.1 — El patrón Arrange-Act-Assert

> Rama `module-03.1/patron-aaa`. Checkpoint del curso **TESTNET**. Aquí aprendes a **estructurar** los
> tests con Arrange-Act-Assert para que se lean de un vistazo y se mantengan. Abre el Módulo 3.

## Qué hay en esta rama

- **`EstructuraAaaTests.cs`** — el ejemplo del submódulo: el **AAA canónico** (con los comentarios
  `// Arrange / // Act / // Assert` y nombre que habla) y el caso de **"un concepto, dos asserts"**
  (`CalcularTotal`, que comprueba importe y moneda como facetas de lo mismo).
- **`PedidoEstadosTests.cs`** sigue aquí y es el modelo del refactor: el ciclo de un pedido **partido**
  en tests AAA, uno por transición, en vez de un `TestPedido` de cuatro-en-uno.
- Los demás tests de M2 siguen aquí (`CalculadoraDescuentosTests.cs`, `CantidadTests.cs`,
  `CoberturaFalsoPositivoTests.cs`): el código de producción no cambia entre ramas, solo crecen los tests.
- **[`MANUAL.md`](MANUAL.md)** — las tres fases, el SUT, "un assert por test" con su matiz y los cuatro
  errores típicos.
- **[`material/tarjetas/M3.1-aaa.md`](material/tarjetas/M3.1-aaa.md)** — tarjeta de 1 página.
- **[`material/labs/M3.1-refactor-a-aaa.md`](material/labs/M3.1-refactor-a-aaa.md)** — lab: refactorizar
  un `TestPedido` de cuatro-en-uno y un test con la fórmula en el Assert a AAA.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde. `EstructuraAaaTests.cs` añade dos tests AAA sobre el código que ya
existe; no hace falta cobertura para este submódulo (eso fue M2.3).

## Qué cubre (BR)

No añade reglas de negocio nuevas: reusa el descuento (BR-01..BR-05) y el cálculo del total del pedido
para enseñar **estructura de tests**, no comportamiento. El SUT no cambia respecto a M2.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `material/` → todo lo didáctico (tarjetas, labs).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-02.3/cobertura` → **`module-03.1/patron-aaa`** ← estás aquí (abre el Módulo 3) → `module-03.2/nombrado` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Convención: `Assert` nativo de xUnit (las aserciones fluidas llegan en M5.3; el mocking, en M5.2).
- El `PedidoBuilder` que limpia el Arrange aparece en el temario como anticipo: se construye en **M3.3**.
  Aquí los tests montan el escenario a mano o reutilizan los helpers existentes.
