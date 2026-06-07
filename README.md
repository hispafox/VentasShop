# VentasShop

Proyecto-ejemplo del curso **TESTNET — Testing Automatizado en .NET**. Es un dominio neutro de
**pedidos y descuentos**, pensado para tener dónde aplicar cada técnica del curso: lógica pura que
testear, una máquina de estados, un servicio con dependencias y acceso a datos.

Este repositorio es la **fuente de verdad ejecutable**: el código compila y los tests pasan. Los
capítulos del curso **enlazan aquí** en vez de llevar fragmentos escritos a mano. Si algo aparece en
el libro, sale de una rama de este repo.

> Todo el código está **en castellano** (identificadores, namespaces y proyectos). Es un curso en
> español y el código es material que el alumno lee. Los identificadores van en ASCII (sin ñ ni
> tildes); el vocabulario propio del testing (test, suite, mock, bug…) se mantiene.

## Arquitectura

```
VentasShop.slnx
├── src/
│   ├── VentasShop.Dominio          // entidades, objetos de valor, lógica pura, interfaces
│   ├── VentasShop.Aplicacion       // servicios que orquestan dominio + dependencias
│   └── VentasShop.Infraestructura  // EF Core, repositorios, pasarela, reloj, legacy
└── tests/
    ├── VentasShop.TestsUnitarios     // M1–M5
    └── VentasShop.TestsIntegracion   // M6
```

Capas mínimas, suficientes para enseñar inyección de dependencias y aislamiento sin abrumar. Sin
CQRS, mediator ni event sourcing: sería ruido para un grupo de iniciación.

**Stack:** .NET 10 · C# 14 · xUnit v3. A lo largo del curso entran NSubstitute (M5.2),
AwesomeAssertions (M5.3), Coverlet/ReportGenerator (M7) y Testcontainers (M6.3). Hasta M5.3 las
aserciones son las nativas de xUnit (`Assert`).

## Cómo se ejecuta

```bash
dotnet build VentasShop.slnx
dotnet test  VentasShop.slnx
```

Requisitos: SDK de .NET 10. Para los tests de integración con base de datos real (M6.3) hace falta
Docker (Testcontainers).

## Las ramas: una por submódulo

El repo sigue el patrón **checkpoint progresivo**: cada rama es el estado del código al terminar un
submódulo del temario, y cada una **construye sobre la anterior**.

- **`starter`** — el punto de partida: el código de producción de VentasShop **entero**, con los
  proyectos de test vacíos. Aquí no hay tests todavía; los vas escribiendo a medida que avanza el
  curso.
- **`module-MM.S/<slug>`** — una por submódulo (`module-01.1/conceptos-clave` … `module-08.3/taller`).
  Hereda el código de producción de `starter` y **añade los tests** de ese submódulo (acumulativos).
- **`module-MM.S-demo-<slug>`** — las demos y laboratorios, cada uno en su rama, pegado a su
  submódulo para no romper el orden del temario.
- **`main`** — el estado final, con todo.

Cada rama es **autocontenida**: además del código trae un **`MANUAL.md`** (el porqué pedagógico, la
historia detrás del ejemplo) y un **`README.md`** (la ficha técnica: qué hay, cómo se ejecuta, qué
reglas de negocio cubre).

> El código de producción no cambia entre ramas: vive completo en `starter`. Lo que crece son los
> tests. La excepción es el Módulo 8, que parte de un legacy neutro (`NotificadorPedidos`) para
> enseñar a abrirle costuras.

## Reglas de negocio (trazabilidad regla → test)

Cada test referencia la regla que cubre, etiquetada `BR-XX`. El núcleo está en `CalculadoraDescuentos`.

| Código | Regla |
|---|---|
| BR-01 | Pedido < 100 € → 0 % por volumen |
| BR-02 | 100 € ≤ pedido < 500 € → 5 % |
| BR-03 | Pedido ≥ 500 € → 10 % |
| BR-04 | `Premium` +3 %, `Vip` +5 % |
| BR-05 | El descuento total nunca supera el 15 % (tope) |
| BR-06 | Cupón válido y no caducado → 10 % extra (depende de la fecha → reloj inyectable) |
| BR-07 | No se puede pagar un pedido sin líneas |
| BR-08 | `Cantidad` siempre > 0 |
| BR-09 | Si la pasarela falla, el pedido no avanza ni se guarda |
| BR-10 | Al pagar se registra un apunte de auditoría |

## Aviso

Proyecto **neutro y ficticio**: no contiene nombres ni datos de ningún cliente. Es material de
formación.
