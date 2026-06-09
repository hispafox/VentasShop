# VentasShop · M8.2 — Definir la estrategia

> Rama `module-08.2/estrategia`. Checkpoint del curso **TESTNET**. Submódulo de **estrategia** (no de
> código): cómo meterle red a un sistema que está en producción y no se puede parar, con un plan por fases.

## Qué hay en esta rama

No añade tests (es estrategia). Añade el material del plan:

- **[`material/labs/M8.2-hoja-de-ruta.md`](material/labs/M8.2-hoja-de-ruta.md)** — el lab: convertir el
  diagnóstico de M8.1 en una hoja de ruta F1-F2-F3 en una página.
- **[`material/ci-ejemplo.yml`](material/ci-ejemplo.yml)** — un gate de CI **neutro y de referencia**
  (GitHub Actions): restaurar → compilar → `dotnet test` que bloquea el merge si falla. **No está en
  `.github/workflows/`** a propósito (es didáctico, no un workflow activo).
- **[`material/tarjetas/M8.2-estrategia.md`](material/tarjetas/M8.2-estrategia.md)** — la chuleta.

## El plan por fases

Reparar el barco sin dejar de navegar: asegura primero lo que te hunde.

1. **F1 — la línea de flotación**: tests **unitarios** del núcleo de negocio crítico. Mayor retorno; usa las
   costuras de M8.1 + dobles (M5).
2. **F2 — los sistemas de maniobra**: tests de **integración** en las fronteras (BD, servicios externos).
   Más lenta; va después. Hay fallos que solo caza el motor real (M6.3).
3. **F3 — el automatismo**: el **gate de CI**. Se monta aquí (M7.3 solo lo nombró): `dotnet test` bloquea el
   merge; cobertura en la misma pasada con `dotnet test -- --coverage` (Microsoft Code Coverage / MTP, **no
   Coverlet**); reparto push / merge / nocturno.

Objetivo de cobertura **por zona y creciente** (cobertura sobre código nuevo, M7.1), no global con fecha.
Y **confirmar el stack** con el equipo antes de empezar.

## Cómo se compila y se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  tests/VentasShop.TestsUnitarios     # 87 verdes + 1 skip (sin cambios: es estrategia)
dotnet test  tests/VentasShop.TestsIntegracion   # 11/11
```

## Dónde estás en el curso

… → `module-08.1/diagnostico` → **`module-08.2/estrategia`** ← estás aquí → `module-08.3/...` (el taller, cierra el curso) → …

## Notas

- Material **en castellano**. **R1**: el plan se hace sobre el legacy neutro; el proyecto real del cliente
  solo en el taller en directo de M8.3, nunca en el material.
- Cobertura sobre **MTP** (no Coverlet); el gate corre `dotnet test`. Sin Docker.
