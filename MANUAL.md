# Manual del alumno — M6.3 · Testing con SQLite in-memory

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué SQLite in-memory cierra
la espina que dejó el provider in-memory de M6.2, cuál es su clave técnica (que despista a todo el mundo la
primera vez) y dónde está su límite honesto.

Tiempo de lectura: ~12 min. Submódulo M6.3 (Tests de Integración con un motor relacional de verdad).

---

## 1. La idea en una frase

SQLite en modo in-memory es un motor relacional **de verdad** ejecutándose en RAM: respeta índices únicos,
claves foráneas y transacciones, y no necesita instalar ni levantar nada. La fidelidad relacional que el
provider in-memory no daba, sin salir de un test rápido y sin instalar ni levantar nada.

---

## 2. Dos cosas que se llaman «in-memory» (y no son lo mismo)

En M6.2 quedó una espina: el provider InMemory de EF Core dejaba pasar la unicidad de producto. Ojo al
equívoco de nombres. El provider InMemory **no es una base de datos**: es un diccionario que imita a EF, y
por eso no valida restricciones. SQLite in-memory es otra liga: el motor SQLite de verdad —el mismo que
llevan el móvil, el navegador y media industria— ejecutándose en memoria en lugar de sobre un fichero. SQL
real, restricciones reales, transacciones reales.

---

## 3. El coche de verdad en el circuito de pruebas

El provider in-memory era el simulador de conducción de M6.2: cómodo, pero no la carretera. SQLite in-memory
es el siguiente escalón, un coche de verdad en el circuito de pruebas: motor real, físicas reales, y no
tienes que traer el tuyo ni alquilar la pista, está listo en el momento. Su límite es que no es tu coche de
producción exacto: para SQLite, ese «otra marca» es el dialecto.

---

## 4. La clave técnica: la conexión que hay que mantener abierta

Es el detalle que más despista, y si no lo conoces te vuelve loco: **la base en memoria vive mientras la
conexión esté abierta**. En cuanto se cierra, la base desaparece con todo dentro. EF Core, por defecto, abre
y cierra la conexión en cada operación, lo que con SQLite in-memory significaría crear una base vacía,
usarla un instante y tirarla.

El truco: abres tú la conexión y la mantienes viva durante todo el test. Fíjate en dos cosas:

- a `UseSqlite` le pasas el **objeto** `SqliteConnection` ya abierto, **no una cadena** de texto, para que
  todos los contextos que la compartan vean la misma base;
- `EnsureCreated` crea el esquema desde tu modelo, con todas sus restricciones e índices reales (incluido el
  índice único del código de producto). **No `MigrateAsync`**: el repo no tiene migraciones.

Mira [`tests/.../RepositorioPedidosSqliteTests.cs`](tests/VentasShop.TestsIntegracion/RepositorioPedidosSqliteTests.cs):
el constructor abre la conexión y crea el esquema, `Dispose` la cierra, y como xUnit crea una instancia de la
clase por test, cada uno recibe su base limpia y propia (independencia de M1.3 sin esfuerzo).

---

## 5. El ejemplo que lo demuestra (la recompensa)

El test `Sqlite_RefuerzaElIndiceUnico_DosProductosConElMismoCodigoLanzan` añade dos productos con el mismo
`Codigo` y, al guardar, **espera que salte**: `guardar.Should().Throw<DbUpdateException>()`. Y salta. Es
justo el escenario que el provider in-memory de M6.2 dejaba pasar (`Record.Exception(...)` devolvía `null`).
El índice único existe de verdad en la base, el segundo producto lo viola, y `SaveChanges` lanza. El
simulador y el coche de verdad, en una sola suite. Y con la unicidad van también las claves foráneas, las
transacciones reales y la traducción de tus consultas a SQL.

---

## 6. El límite honesto

SQLite es un motor relacional real, pero **no es tu motor de producción**. Si en producción usas SQL Server
o PostgreSQL, hay diferencias de dialecto: algunos tipos de dato, ciertas funciones de SQL, el comportamiento
exacto de la ordenación de cadenas. La mayoría de las veces no lo notas; de vez en cuando, una consulta que
pasa en SQLite se comporta distinto contra tu motor real. SQLite te da casi toda la fidelidad relacional por
casi ningún coste; para el poco donde el dialecto exacto importa, el juez es tu motor real, donde lo tengas.

---

## 7. La estrategia: cuándo cada uno

De la pirámide (M1.2), dentro de la integración: **unitarios** para toda la lógica que no toca base de datos
(la mayoría); **provider in-memory** para tests rápidos de la lógica de una consulta donde las restricciones
no son lo que pruebas; **SQLite in-memory** para validar de verdad el comportamiento relacional sin instalar
nada (tu caballo de batalla de integración); y, para lo poquísimo donde el dialecto exacto importa, tu motor
real. No es «SQLite para todo», igual que no era «in-memory para todo».

---

## 8. Errores comunes

**Dejar que la conexión se cierre** (la base desaparece y aparecen «tablas que no existen»; mantenla abierta,
pasa el objeto). **Creer que SQLite es tu motor de producción** (para el dialecto, tu motor real es el juez).
**Compartir una conexión entre tests** (comparten base y se pisan; una conexión por test). **Olvidar
`EnsureCreated`** (la base nace vacía y sin él no hay tablas).

---

## 9. Lo que te llevas

El laboratorio ([`material/labs/M6.3-el-contraste.md`](material/labs/M6.3-el-contraste.md)) es el contraste
estrella: coge los tests de repositorio que escribiste con el provider in-memory en M6.2 y ejecútalos también
contra SQLite, comparando. Los de lógica de consulta pasan en ambos; la unicidad solo se valida de verdad
contra SQLite. La tarjeta ([`material/tarjetas/M6.3-sqlite-in-memory.md`](material/tarjetas/M6.3-sqlite-in-memory.md))
lo resume.

En M6.4 cerramos la capa de datos con el patrón que la organiza y la hace testeable: el patrón Repository, el
testing del CRUD completo y cómo manejar las transacciones en los tests.
