# VentasShop · M1.2 — La pirámide de testing

> Rama `module-01.2/piramide`. Checkpoint del curso **TESTNET**. Submódulo **conceptual**: aquí no se
> escribe código de test todavía (eso empieza en M2); el entregable es **didáctico** — el manual, los
> diagramas y la tarjeta de decisión.

## Qué hay en esta rama

- **[`MANUAL.md`](MANUAL.md)** — el manual del alumno: el desastre del cono, los tres niveles, el
  trade-off velocidad↔confianza, la proporción 70/20/10 como orientación, el antipatrón del cono de
  helado y el reparto de VentasShop. Con **2 diagramas Mermaid** (la pirámide sana y el cono de
  helado), que se ven en GitHub y en el preview del IDE.
- **[`material/tarjetas/M1.2-que-nivel.md`](material/tarjetas/M1.2-que-nivel.md)** — tarjeta de
  decisión de 1 página: ¿en qué nivel va este test? (imprimible).
- **[`material/labs/M1.2-reparto-piramide.md`](material/labs/M1.2-reparto-piramide.md)** — lab
  resuelto: colocar diez comprobaciones en su nivel y cazar el unitario disfrazado de e2e.
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

Sin tests en esta rama (conceptual); `dotnet test` no ejecuta ninguno todavía.

## Dónde estás en el curso

… → `module-01.1/conceptos-clave` → **`module-01.2/piramide`** ← estás aquí → `module-01.3/first` → …

## Notas

- Código y material **en castellano**. Proyecto **neutro**: sin nombres de cliente.
- Visión general del repo y mapa completo de ramas: rama `main`.
