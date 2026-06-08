# VentasShop · M5.1 — Test doubles (Dummy, Stub, Spy, Mock y Fake)

> Rama `module-05.1/test-doubles`. Checkpoint del curso **TESTNET**. Aquí das el salto del dominio puro al
> `ServicioPedidos` con dependencias: los **test doubles** y su taxonomía, hechos **a mano** (sin librería).
> **Abre el Módulo 5.** Es conceptual; la sintaxis con NSubstitute es M5.2.

## Qué hay en esta rama

- **`tests/.../Dobles/`** — los cuatro dobles escritos a mano, uno por dependencia del `ServicioPedidos`:
  - `RelojFijo` — **stub** de `IReloj` (hora fija, determinismo).
  - `RepositorioPedidosEnMemoria` — **fake** de `IRepositorioPedidos` (diccionario en RAM; lleva contador
    `VecesGuardado` para hacer también de spy).
  - `PasarelaPagoFalsa` — **stub + spy** de `IPasarelaPago` (resultado prefijado; registra cobros e importe).
  - `LoggerEspia<T>` — **spy** de `ILogger<T>` (captura los apuntes de auditoría).
- **`tests/.../DoblesArtesanalesTests.cs`** — 4 tests del `ServicioPedidos.Pagar` usando esos dobles y
  `Assert` nativo: pago aceptado (estado + interacción), pago rechazado (BR-09: no avanza ni guarda),
  cobro del total exacto (spy), pedido inexistente (no se cobra).
- **[`MANUAL.md`](MANUAL.md)** — los dobles de cine, los 4 motivos, la taxonomía, estado vs. interacción,
  la regla de oro y por qué verlos hechos a mano antes de NSubstitute.
- **[`material/tarjetas/M5.1-test-doubles.md`](material/tarjetas/M5.1-test-doubles.md)** — tarjeta de 1 página.
- **[`material/labs/M5.1-elegir-doble.md`](material/labs/M5.1-elegir-doble.md)** — lab **de criterio** (sin
  código): decidir el doble de cada dependencia y si verificas estado o interacción.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** salen en verde (63/63). `DoblesArtesanalesTests.cs` añade 4 tests; el código de
producción no cambia.

## El SUT del Módulo 5: `ServicioPedidos`

En [`src/VentasShop.Aplicacion/ServicioPedidos.cs`](src/VentasShop.Aplicacion/ServicioPedidos.cs): orquesta
el pago de un pedido con cuatro dependencias (`IRepositorioPedidos`, `IPasarelaPago`, `IReloj`,
`ILogger<>`). Reglas en juego: si la pasarela rechaza, el pedido no avanza ni se guarda (BR-09); al pagar se
registra la auditoría (BR-10). Es el ejemplo natural de mocking, y se arrastra por M5.2 y M5.3.

## Estado vs. interacción (lo conceptual)

- **Estado** — miras el resultado (`pedido.Estado == Pagado`). Preferible: no se acopla a *cómo*.
- **Interacción** — compruebas que se *llamó* a una dependencia (se cobró, se guardó, se auditó).
- Regla: prefiere estado; usa interacción solo cuando la llamada en sí es lo que importa.

## Hechos a mano (a propósito)

Los dobles de esta rama no usan librería. Un fake o un stub se escriben a mano y se reutilizan, como los
Builders de M3.3. Montar cada uno es trabajo: justo lo que NSubstitute reduce a dos líneas en **M5.2**.
Verlos por dentro primero es lo que hace que la librería se entienda después.

## Organización del repo

- `src/` y `tests/` → la solución .NET. `material/` → todo lo didáctico (tarjetas, labs).
- `tests/.../Dobles/` → los dobles reutilizables del Módulo 5.
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Dónde estás en el curso

… → `module-04.3/excepciones` (cierra el Módulo 4) → **`module-05.1/test-doubles`** ← estás aquí (abre el Módulo 5) → `module-05.2/mocking-nsubstitute` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Submódulo **conceptual**: los dobles a mano + el lab de criterio. La sintaxis con NSubstitute es M5.2;
  las aserciones fluidas de AwesomeAssertions, M5.3.
- Taxonomía Dummy/Stub/Spy/Mock/Fake = Meszaros (*xUnit Test Patterns*); estado vs. interacción = Fowler.
