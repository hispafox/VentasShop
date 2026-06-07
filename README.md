# VentasShop · M1.1 — Conceptos clave de testing

> Rama `module-01.1/conceptos-clave`. Checkpoint del curso **TESTNET**. Submódulo **conceptual**:
> aquí todavía no se escribe ningún test (la herramienta, xUnit, llega en el Módulo 4). El
> entregable de esta rama es **didáctico**: el manual y el laboratorio de decisión.

## Qué hay en esta rama

- **[`MANUAL.md`](MANUAL.md)** — el manual del alumno: la analogía del seguro, automatizado vs.
  manual, los tres usos del testing, la curva del coste del bug y el ciclo Red-Green-Refactor. Con
  **3 diagramas** (Mermaid, se ven en GitHub y en el preview del IDE):
  - flowchart **¿automatizar o a mano?**
  - la **curva del coste** del bug por fase
  - el ciclo **Red-Green-Refactor**
- **[`labs/M1.1-automatizar-o-no.md`](labs/M1.1-automatizar-o-no.md)** — laboratorio resuelto:
  clasificar diez comprobaciones en *automatizar* / *a mano*, con su justificación y las dos trampas.
- El **código de producción de VentasShop** (heredado de `starter`): aquí se mira, no se testea
  todavía. Compila.

## Cómo se compila

```bash
dotnet build VentasShop.slnx
```

No hay tests en esta rama (es conceptual), así que `dotnet test` no ejecuta ninguno todavía. Los
tests empiezan a crecer a partir del Módulo 2.

## Dónde estás en el curso

`starter` → **`module-01.1/conceptos-clave`** ← estás aquí → `module-01.2/piramide` → …

## Notas

- Código y material **en castellano** (regla del repo). Proyecto **neutro**: sin nombres de cliente.
- Visión general del repo y mapa completo de ramas: rama `main`.
