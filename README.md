# VentasShop · M2.2 — Técnicas de diseño de casos

> Rama `module-02.2/tecnicas-diseno`. Checkpoint del curso **TESTNET**. **Primera rama con tests
> reales**: aquí el andamiaje conceptual se convierte en una suite que se ejecuta.

## Qué hay en esta rama

- **Tests reales** en `tests/VentasShop.TestsUnitarios/` (22 tests, en verde):
  - `CalculadoraDescuentosTests.cs` — partición de equivalencia, valores límite y tabla de decisión
    (decimales en `TheoryData`, no en `[InlineData]`).
  - `PedidoEstadosTests.cs` — transiciones de estado válidas e inválidas (+ BR-07).
  - `CantidadTests.cs` — valores límite sobre una invariante (con `[InlineData]` de enteros).
- **[`MANUAL.md`](MANUAL.md)** — las cuatro técnicas (catar la sopa), con la **tabla de decisión** y un
  **diagrama de estados** del pedido (Mermaid), enlazando a cada test real.
- **[`material/tarjetas/M2.2-tecnicas.md`](material/tarjetas/M2.2-tecnicas.md)** — tarjeta: las cuatro
  técnicas, el orden de ataque y los avisos (decimales en `TheoryData`, la tabla que explota).
- **[`material/labs/M2.2-tecnicas-de-diseno.md`](material/labs/M2.2-tecnicas-de-diseno.md)** — lab
  resuelto: leer y justificar los casos, diseñar los que faltan, y el corto vs. el pasado.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (22/22). El proyecto de **integración** está vacío hasta el Módulo 6
(`dotnet test VentasShop.slnx` avisa de que ahí no hay tests todavía — es lo esperado).

## Cobertura por reglas (BR)

Esta rama cubre: BR-01..BR-05 (descuento por volumen y tipo + tope), BR-07 (pagar pedido sin líneas),
BR-08 (cantidad > 0) y las transiciones del ciclo de vida del pedido.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `material/` → todo lo didáctico (tarjetas, labs, e
  interactivos/imágenes más adelante).
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-02.1/que-testear` → **`module-02.2/tecnicas-diseno`** ← estás aquí (primeros tests) → `module-02.3/cobertura` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Convención: `Assert` nativo de xUnit (las aserciones fluidas llegan en M5.3; el mocking, en M5.2).
