# VentasShop · M2.1 — Definir qué testear

> Rama `module-02.1/que-testear`. Checkpoint del curso **TESTNET**. Submódulo **conceptual**: aquí se
> decide *qué* testear (no *cómo* — eso es M2.2). Aún no se escribe código de test; el entregable es
> **didáctico**.

## Qué hay en esta rama

- **[`MANUAL.md`](MANUAL.md)** — el manual del alumno: el 80% que no protege, la analogía de la casa y
  los detectores de humo, qué se testea siempre (lógica, bordes, las dos caras), qué NO (4 familias) y
  el marco riesgo × consecuencia. Con **1 diagrama Mermaid** (la matriz riesgo × consecuencia).
- **[`material/tarjetas/M2.1-que-testear.md`](material/tarjetas/M2.1-que-testear.md)** — tarjeta de
  decisión: ¿esto merece un test? La matriz, el «casi siempre sí» y el «por defecto no» (imprimible).
- **[`material/labs/M2.1-clasificar-que-testear.md`](material/labs/M2.1-clasificar-que-testear.md)** —
  lab resuelto: clasificar diez elementos de VentasShop (entra/no, nivel, por qué).
- El **código de producción de VentasShop** (heredado de `starter`): aquí se mira para decidir, no se
  testea aún. El `CalculadoraDescuentos` es «la cocina».

## Organización del repo

- `src/` y `tests/` → **solo** la solución .NET (lo que compila).
- `material/` → todo el material didáctico: `material/tarjetas/`, `material/labs/` e interactivos/imágenes más adelante.
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Cómo se compila

```bash
dotnet build VentasShop.slnx
```

Sin tests en esta rama (conceptual); los primeros tests reales llegan en `module-02.2`.

## Dónde estás en el curso

… → `module-01.3/first` → **`module-02.1/que-testear`** ← estás aquí → `module-02.2/tecnicas-diseno` (primeros tests) → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Visión general del repo y mapa completo de ramas: rama `main`.
