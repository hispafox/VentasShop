# VentasShop · M1.3 — Propiedades F.I.R.S.T.

> Rama `module-01.3/first`. Checkpoint del curso **TESTNET**, cierra el Módulo 1. Submódulo
> **conceptual**: aún no se escribe código de test (eso empieza en M2); el entregable es **didáctico**.

## Qué hay en esta rama

- **[`MANUAL.md`](MANUAL.md)** — el manual del alumno: «ese test falla a veces, dale otra vez», las
  cinco propiedades (Fast, Independent, Repeatable, Self-validating, Timely) con su olor y su cura, y
  el test de VentasShop pasado por el filtro. Con **1 diagrama Mermaid** (FIRST como mapa).
- **[`material/tarjetas/M1.3-first.md`](material/tarjetas/M1.3-first.md)** — tarjeta de 1 página: las
  cinco letras, su olor, su cura, y cuáles se negocian (imprimible).
- **[`material/labs/M1.3-diagnostico-first.md`](material/labs/M1.3-diagnostico-first.md)** — lab
  resuelto: por cada test, diagnosticar qué letra de FIRST incumple y cómo se cura.
- El **código de producción de VentasShop** (heredado de `starter`): aquí se mira, no se testea aún.

## Organización del repo

- `src/` y `tests/` → **solo** la solución .NET (lo que compila).
- `material/` → todo el material didáctico, separado del código: `material/tarjetas/` (cheat-sheets),
  `material/labs/` (laboratorios) y, más adelante, interactivos (HTML/JSX) e imágenes.
- `MANUAL.md` + `README.md` en la raíz = el manual y la ficha de **este** checkpoint.

## Cómo se compila

```bash
dotnet build VentasShop.slnx
```

Sin tests en esta rama (conceptual); `dotnet test` no ejecuta ninguno todavía. Los tests empiezan en M2.

## Dónde estás en el curso

… → `module-01.2/piramide` → **`module-01.3/first`** ← estás aquí (cierra M1) → `module-02.1/que-testear` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Visión general del repo y mapa completo de ramas: rama `main`.
