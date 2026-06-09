# Manual del alumno — M6.2 · Testing con EF Core in-memory

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué la base de datos en
memoria es tan tentadora, qué hace bien y —sobre todo— qué **no comprueba**, para que no te dé una falsa
sensación de seguridad.

Tiempo de lectura: ~12 min. Submódulo M6.2 (Tests de Integración y Base de Datos). Estrena el proyecto
`VentasShop.TestsIntegracion`.

---

## 1. La idea en una frase

La base de datos in-memory es rápida y cómoda, pero no es una base de datos relacional: úsala sabiendo
exactamente qué no comprueba. De hecho, el propio equipo de EF Core la desaconseja para testear
comportamiento relacional — lo abordamos de frente, no lo ocultamos.

---

## 2. El simulador de conducción

La base in-memory es un simulador de conducción. Para practicar la lógica —cuándo cambias de marcha, por
dónde va el trazado— es fantástico: barato, instantáneo, sin consecuencias. Pero no es la carretera: no
tiene el agarre del asfalto mojado ni cómo responde tu coche al frenar de verdad. Imita el «guardar y leer
objetos con EF Core» lo justo para practicar la lógica de tu repositorio, pero no es un motor relacional.

---

## 3. Cómo se usa

Necesitas el paquete `Microsoft.EntityFrameworkCore.InMemory` y configuras el contexto con
`UseInMemoryDatabase(nombreBd)`. El nombre es la clave de la independencia: un `Guid` único por test, para
que cada uno tenga su base aislada (M1.3). Dos contextos con el mismo nombre comparten el almacén, y eso es
lo que te permite guardar con uno y leer con otro. Mira
[`tests/.../RepositorioPedidosInMemoryTests.cs`](tests/VentasShop.TestsIntegracion/RepositorioPedidosInMemoryTests.cs).

Un detalle que da bugs sutiles: lee en un contexto **nuevo**, no en el que usaste para guardar. Si lees del
mismo, EF te devuelve el objeto de su caché de seguimiento sin tocar la «base de datos», y el test pasa
aunque el guardado real estuviera mal. Y usa `Include(p => p.Lineas)` para traer las líneas, que `Find` por
sí solo no carga.

---

## 4. Qué no comprueba (la letra pequeña)

El provider in-memory no es un motor relacional, así que deja pasar cosas que tu base real no permitiría:

- **El índice único** — guarda dos productos con el mismo código sin rechistar.
- **La integridad referencial de una clave foránea** y los `CHECK`.
- **La traducción a SQL** — ejecuta las consultas en memoria, así que una que el motor real rechazaría aquí pasa.
- **Las transacciones reales, los índices y los triggers.**

Un matiz que importa: **desde EF Core 6, el in-memory sí valida las propiedades obligatorias (`NOT NULL`)** y
lanza si dejas en nulo algo requerido. Lo que no comprueba es el resto de esta lista.

---

## 5. El ejemplo que lo demuestra

VentasShop estrena un campo `Producto.Codigo` con **índice único** en `ContextoVentasShop`
(`HasIndex(p => p.Codigo).IsUnique()`). El test
`InMemory_NoRefuerzaElIndiceUnico_GuardaDosProductosConElMismoCodigo` añade dos productos con el mismo
código y comprueba que `SaveChanges` **no lanza** en in-memory: `Record.Exception(...)` devuelve `null`. Eso
documenta la limitación. Contra SQL Server real (M6.3), ese mismo `SaveChanges` lanzaría `DbUpdateException`.
El simulador y la carretera, en una pantalla.

---

## 6. Cuándo sí usarlo

El in-memory no es basura: tiene su nicho. Sirve cuando pruebas la lógica de un repositorio o una consulta
donde las restricciones no son lo que verificas, quieres tests rápidos sin Docker, y eres consciente de que
no validas el comportamiento relacional. Si empiezas de cero, plantéate **SQLite en modo in-memory** como
punto medio: es un motor relacional de verdad (respeta restricciones, traduce a SQL) y sigue siendo rápido y
sin Docker. Para fidelidad total a tu motor, Testcontainers (M6.3).

---

## 7. Errores comunes

**Confiar en el in-memory para validar restricciones** (es justo lo que no hace). **Leer del mismo contexto
con el que escribiste** (te devuelve la caché de EF). **Compartir sin querer el nombre de la base** entre
tests (se pisan según el orden). Y **creer que tienes tests de tu capa de datos** cuando solo tienes tests
de su lógica, no de su comportamiento real contra el motor.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M6.2-in-memory-y-su-limite.md`](material/labs/M6.2-in-memory-y-su-limite.md))
tiene dos partes: la cómoda (CRUD y consultas del repositorio con in-memory) y la reveladora (ver que la
unicidad **no** salta). La tarjeta ([`material/tarjetas/M6.2-ef-in-memory.md`](material/tarjetas/M6.2-ef-in-memory.md))
lo resume.

En M6.3 nos bajamos del simulador y nos subimos a la carretera: una base de datos **real** (tu mismo motor,
SQL Server) pero efímera, levantada en un contenedor Docker para el test con Testcontainers. Ahí el test de
la unicidad sí cazará la restricción.
