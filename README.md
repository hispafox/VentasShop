# VentasShop · M6.1 — Tests unitarios vs. tests de integración

> Rama `module-06.1/unit-vs-integracion`. Checkpoint del curso **TESTNET**. Submódulo **conceptual / de
> estrategia**: cuándo un test va a unitario y cuándo a integración. **Abre el Módulo 6.** No añade tests
> nuevos; el código contra base de datos llega en 6.2 (EF Core in-memory), 6.3 (Testcontainers) y 6.4.

## Qué hay en esta rama

- **[`material/labs/M6.1-clasificar-unit-integracion.md`](material/labs/M6.1-clasificar-unit-integracion.md)**
  — lab de **criterio**: clasificar cada pieza de VentasShop como unitario o integración con una sola
  pregunta, «¿cruza la frontera de la base de datos?» (incluye solución).
- **[`material/tarjetas/M6.1-unit-vs-integracion.md`](material/tarjetas/M6.1-unit-vs-integracion.md)** — tarjeta de 1 página.
- **[`MANUAL.md`](MANUAL.md)** — la espina de M5, la orquesta (unit vs ensayo general), lo que solo caza la
  integración, la pirámide, el coste y el reparto de VentasShop.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios
```

Los **unitarios** siguen en verde (73/73): este submódulo **no cambia código** ni añade tests, es de
estrategia. El proyecto `tests/VentasShop.TestsIntegracion` existe pero está vacío hasta M6.2.

## El criterio (lo que tienes que llevarte)

**¿Puedo comprobar esto de verdad sin una base de datos real?**
- **Sí** → unitario (lógica aislada / servicio con dobles). `VentasShop.TestsUnitarios`.
- **No, necesito el motor** → integración (piezas reales). `VentasShop.TestsIntegracion`.

## El reparto de VentasShop

- **Unitario:** `CalculadoraDescuentos`, invariantes de `Cantidad`/`Dinero`, transiciones del `Pedido`,
  `ServicioPedidos` con dobles. El descuento se queda aquí (lógica pura).
- **Integración (6.2/6.3/6.4):** `RepositorioPedidos` real (guardar/recuperar pedido con líneas), consulta
  «pedidos de un cliente» contra el motor, restricción «no se guarda un pedido sin cliente» (FK obligatoria).

## Por qué no hay tests aquí

M6.1 es la foto de estrategia que da sentido a los tres submódulos siguientes. Escribir tests de integración
«por levantar bases de datos» antes de tener claro el reparto es justo el error que este submódulo evita. El
proyecto `VentasShop.TestsIntegracion` ya está sembrado (desde M4.1); se llena en M6.2 en adelante.

## Dónde estás en el curso

… → `module-05.3/aserciones-fluidas` (cierra el Módulo 5) → **`module-06.1/unit-vs-integracion`** ← estás aquí (abre el Módulo 6) → `module-06.2/ef-core-inmemory` → …

## Notas

- Material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Conceptual: el código real contra BD es 6.2 (EF Core in-memory), 6.3 (Testcontainers.MsSql), 6.4 (repositorios).
- Criterio único: «¿cruza la frontera de la base de datos?». La pirámide manda (M1.2): muchos unit, pocos integración.
