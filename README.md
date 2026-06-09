# VentasShop · M7.3 — Buenas prácticas consolidadas

> Rama `module-07.3/buenas-practicas`. Checkpoint del curso **TESTNET**. Cierra el Módulo 7. El test es
> código de producción: una suite se cuida como un jardín o se convierte en una jungla. Aquí se ven los
> hábitos que la mantienen sana y un lab de «suite-jungla → suite-jardín».

## Qué hay en esta rama

- **`tests/.../BuenasPracticasTests.cs`** — la suite-**jardín** (el «después»): modela los hábitos sobre VentasShop.
  - **Un assert conceptual por test**: `Pagar_DePedidoConfirmado_DejaElPedidoEnPagado` y `CalcularTotal_DeDosUnidadesA50_Devuelve100` (un comportamiento, una razón para el rojo).
  - **Simplicidad**: `CalcularTasaDescuento_...` con `[Theory]` en vez de un `for` dentro del test; los números con significado, a la vista.
  - **Independencia**: cada test fabrica sus datos con el `PedidoBuilder` (M3.3), sin estado compartido.
- **[`material/labs/M7.3-jungla-a-jardin.md`](material/labs/M7.3-jungla-a-jardin.md)** — el lab con el «antes» (la suite-jungla) y su transformación.
- **[`material/tarjetas/M7.3-buenas-practicas.md`](material/tarjetas/M7.3-buenas-practicas.md)** — la chuleta de hábitos.

## Los hábitos (la jardinería)

- **El test es código de producción**: lo lees más veces de las que lo escribes; se mantiene y se refactoriza.
- **Un assert conceptual por test** = un comportamiento, no una sola línea `Assert`. La primera aserción que falla corta el test.
- **Independencia y determinismo**: nada de estado compartido, ni orden asumido, ni reloj/red/fichero (FIRST de M1.3, flaky de M7.2).
- **Simplicidad**: ni `if`/`for` en el cuerpo del test (`[Theory]` de M4.2); el número «mágico» con significado en un test es **bueno**.
- **Refactor con DAMP, no DRY**: en los tests se tolera algo de duplicación si cada test se entiende solo; se quita el ruido con un `PedidoBuilder`, se conserva la historia.
- **Borrar tests sin culpa**: el de una funcionalidad muerta es mala hierba; Git se acuerda.
- **Tests como gate de CI/CD**: el seto que bloquea lo que rompe (el montaje real es M8.2).

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 84 verdes + 1 skip (el frágil de M7.2)
dotnet test  tests/VentasShop.TestsIntegracion   # 11/11
```

## Nota sobre el ejemplo del «assert conceptual»

El capítulo imagina un anti-ejemplo `FuncionaTodo` que comprueba estado, total, líneas y **fecha de pago**.
En el repo, `Pedido.Pagar()` NO registra fecha (solo cambia `Estado`), así que el ejemplo se alinea a
comportamientos **reales**: estado, total, líneas y cliente. La lección (un comportamiento por test) es la
misma. El lab usa esos comportamientos reales.

## Dónde estás en el curso

… → `module-07.2/falsos-positivos` → **`module-07.3/buenas-practicas`** ← estás aquí (cierra M7) → `module-08.1/...` → …

## Notas

- Material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- No introduce tooling nuevo: consolida hábitos sobre lo ya visto (xUnit, `[Theory]`, builders, aserciones fluidas).
