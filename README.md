# VentasShop · M8.3 — Taller aplicado (cierre del curso)

> Rama `module-08.3/taller-aplicado`. **Último checkpoint del curso TESTNET.** El taller es el primer
> partido de verdad: aplicar de una sentada todo el curso sobre código real. En el material, el legacy
> neutro; en la sala, el código del cliente (R1: nunca en el material).

## Qué hay en esta rama

- **`tests/.../Legacy/NotificadorPedidosTallerTests.cs`** — los comportamientos del `NotificadorPedidos`
  escritos «como en el partido», con **dobles de NSubstitute** (M5.2) en vez de los fakes a mano de M8.1:
  envía de día, no de noche (el borde de las 22:00, M2.2). El curso entero junto en veinte líneas (reloj
  fijo M7.2, dobles M5.2, interacción `Received`/`DidNotReceive`, naming M3.2, AAA M3.1, un comportamiento
  por test M7.3).
- **[`material/labs/M8.3-taller.md`](material/labs/M8.3-taller.md)** — el guion de 6 pasos + la plantilla de
  «gaps y próximos pasos» para llevártela rellena.
- **[`material/tarjetas/M8.3-taller.md`](material/tarjetas/M8.3-taller.md)** — la chuleta del método.

> El test de la auditoría (el log) ya está en M8.1 con `LoggerEspia` (spy a mano): verificar `ILogger<T>.Log`
> con NSubstitute es enrevesado por su firma genérica, y es justo el caso del Módulo 5.1 —a veces un spy a
> mano es más claro que el mock con librería—.

## El método (6 pasos), todo el curso en un flujo

1. **Triaje** (M8.1/M2.1) → 2. **Diagnóstico + refactor mínimo / costuras** (M8.1) → 3. **Decidir qué
testear** (M2.1) y los casos/borde (M2.2) → 4. **Escribir los tests** (dobles M5.2, reloj M7.2, AAA M3.1,
naming M3.2, un comportamiento M7.3) → 5. **Medir** (`dotnet test -- --coverage`, Microsoft Code Coverage;
mutation M7.2) → 6. **Gaps y próximos pasos** (M8.2).

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 89 verdes + 1 skip
dotnet test  tests/VentasShop.TestsIntegracion   # 11/11
```

## El curso, completo

Esta rama cierra el recorrido del repo: **starter → module-01.1 … → module-08.3 → main**. La suite final:
**89 unitarios (+1 skip) + 11 de integración**, con todo el stack desplegado (xUnit v3 sobre MTP, NSubstitute,
AwesomeAssertions, SQLite in-memory, Microsoft Code Coverage, Stryker). Cada rama es un submódulo con su
MANUAL/README + material y ejemplos acumulativos.

## La idea que cierra el curso

**Un test no demuestra que tu código funciona; demuestra que sigue haciendo lo que tú decidiste que
hiciera.** Todo lo del repo sirve a esa frase: construir una red que avise el día que alguien cambie sin
querer lo que el sistema tenía que hacer.

## Dónde estás en el curso

… → `module-08.2/estrategia` → **`module-08.3/taller-aplicado`** ← estás aquí (cierra el curso) → `main`

## Notas

- Material **en castellano**. **R1**: el código real del cliente solo se trabaja en directo, jamás en el
  material; el ejemplo es el legacy neutro. Cobertura sobre **MTP** (no Coverlet). Sin Docker.
