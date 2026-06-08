# VentasShop · M4.2 — Tests parametrizados con `[Theory]`

> Rama `module-04.2/tests-parametrizados`. Checkpoint del curso **TESTNET**. Aquí pasas del muro de
> `[Fact]` repetidos a `[Theory]` parametrizado, y ves las cuatro formas de dar datos a un test.

## Qué hay en esta rama

- **`tests/.../ParametrizacionTests.cs`** — las **cuatro fuentes de datos** una al lado de otra sobre el
  mismo descuento: `[InlineData]` (enteros), `[MemberData]` (`object[]`), `TheoryData<>` (tipado) y
  `[ClassData]` (clase `CasosFronteraDescuento`). 13 tests nuevos.
- **[`MANUAL.md`](MANUAL.md)** — la prensa y las láminas, `[Theory]`+`[InlineData]`, la limitación del
  `decimal`, las cuatro fuentes y el criterio de cuándo parametrizar vs separar.
- **[`material/tarjetas/M4.2-theory.md`](material/tarjetas/M4.2-theory.md)** — tarjeta de 1 página.
- **[`material/labs/M4.2-parametrizar-descuento.md`](material/labs/M4.2-parametrizar-descuento.md)** — lab:
  convertir los `[Fact]` del descuento en un `[Theory]` y elegir la fuente de datos.

## El "antes" y el "después" (compara)

- **El "antes":** `tests/.../PrimerasPruebasXunitTests.cs` — los tramos del descuento como `[Fact]`
  separados (M4.1).
- **El "después":** `tests/.../CalculadoraDescuentosTests.cs` — los mismos casos en `[Theory]` +
  `TheoryData<decimal>` (ya estaba desde M2.2).
- **La referencia de M4.2:** `tests/.../ParametrizacionTests.cs` — las cuatro fuentes juntas.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (52/52). `ParametrizacionTests.cs` añade 13 casos; el código de
producción no cambia.

## La regla del `decimal` (la que vertebra el submódulo)

`[InlineData]` solo admite constantes de compilación, y `decimal` no lo es: `[InlineData(0.10m, ...)]` no
compila. Por eso los importes van por `[MemberData]` / `TheoryData<decimal>` / `[ClassData]`, y `[InlineData]`
se reserva para constantes simples (enteros, enums), como en `CantidadTests.cs`.

## Qué cubre (BR)

No añade reglas de negocio: reusa la `CalculadoraDescuentos` (descuento por volumen + tipo de cliente con
tope del 15%) y la `Cantidad` para enseñar **parametrización**. El SUT no cambia.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `material/` → todo lo didáctico (tarjetas, labs).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-04.1/introduccion-xunit` → **`module-04.2/tests-parametrizados`** ← estás aquí → `module-04.3/excepciones` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- El antipatrón del `bool debeLanzar` + `if` (dos comportamientos en un `[Theory]`) se explica en el
  MANUAL; su resolución —testear la excepción aparte— es M4.3.
- ¿Cuántos casos meter? Los que marquen las técnicas de M2.2 (partición, límites, tabla de decisión).
