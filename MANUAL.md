# Manual del alumno — M6.4 · Testing de repositorios y acceso a datos

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué el patrón Repository
hace tu código testeable, cómo se prueba el CRUD completo contra una base de verdad y cuál es el problema
más espinoso de los tests de integración — que no se pisen entre ellos.

Tiempo de lectura: ~12 min. Submódulo M6.4 (cierra el Módulo 6).

---

## 1. La idea en una frase

El patrón Repository pone un mostrador delante de la base de datos: en un test unitario lo doblas con un
mock (M5), en uno de integración lo pruebas de verdad contra la base (SQLite in-memory, M6.3), y en los dos
casos tu lógica de negocio se queda limpia, sin saber nada de SQL ni de EF Core.

---

## 2. El mostrador del almacén

Imagina un almacén grande. Sin mostrador, cada empleado entra, busca entre las estanterías y se lleva lo
que encuentra como puede: un caos donde, si reorganizas el almacén, todos se pierden. Con mostrador, tú
llegas y pides «el pedido 42»; el empleado lo busca por ti y te lo trae. No entras al almacén ni necesitas
saber cómo está ordenado por dentro, y si mañana mueven las estanterías, a ti te da igual.

El repositorio es ese mostrador delante de tu base de datos. Y como es una interfaz, en un test unitario
pones un mostrador de pega (un doble) y en uno de integración pruebas el mostrador real contra el almacén
de verdad. El mismo mostrador, doblado o real según el nivel de test. Ese aislamiento entre tu lógica y el
almacén es justo lo que la hace testeable.

---

## 3. El contraste que importa: `ObtenerPorId` vs `ObtenerConLineas`

El repositorio de VentasShop tiene dos formas de leer un pedido, y la diferencia es una trampa clásica:

- `ObtenerPorId` usa `Find`: trae el pedido por su clave, pero **no** carga sus navegaciones. Las líneas
  vendrán vacías.
- `ObtenerConLineas` lleva `Include(p => p.Lineas)` (y el cliente): te trae el grafo cargado.

Ese es justo el tipo de detalle que solo un test de integración real caza. Si lees por la vía sin `Include`
y esperas las líneas, te las encuentras vacías; y un test con un doble nunca lo notaría, porque el doble te
devuelve el pedido entero que tú mismo le diste. El test `ObtenerPorId_UsaFind_YNoCargaLasLineas` lo deja
a la vista. Es la costura del M6.1 otra vez.

---

## 4. El CRUD completo

Con el repositorio real y SQLite in-memory pruebas el ciclo entero, y cada operación es un test de
integración. El estrella: guardar un pedido con sus líneas y recuperarlo. Lo guardas con `Agregar` y, para
comprobar, **lees en un contexto nuevo** (no en el que usaste para guardar). Si leyeras del mismo, EF te
devolvería el objeto de su caché de seguimiento sin tocar la base, y el test pasaría aunque el guardado
real estuviera mal (el gotcha de 6.2). Recuperas con `ObtenerConLineas` para que vengan las líneas.

El resto del CRUD sigue el mismo patrón: una **actualización** (recuperar, cambiar el estado con
`Confirmar`, `Guardar`, volver a recuperar y comprobar que persistió), un **borrado** (`Eliminar` y
comprobar que ya no está), y una **consulta** por cliente (`ObtenerPorCliente`, comprobar que devuelve los
correctos y solo esos). Como son lentos y reales, la regla del 6.1 manda: pocos y bien elegidos.

---

## 5. La integridad referencial es de verdad (el borrado en cascada)

Al implementar el borrado apareció una lección que el provider in-memory te habría ocultado: SQLite, motor
real, **exige la integridad referencial**. Borrar un pedido que tiene líneas, sin más, viola la clave
foránea de las líneas. La solución correcta no es un truco de test, es modelar bien el dominio: el pedido
es **dueño** de sus líneas, así que al borrarlo se borran ellas. Eso se configura una vez en el contexto
(`OnDelete(DeleteBehavior.Cascade)`) y el motor se encarga. Contra el provider in-memory esto nunca habría
saltado, y te habrías llevado un borrado a medias a producción.

---

## 6. El problema espinoso: el aislamiento entre tests

Esto es lo que separa una suite de integración que aguanta de una que se pudre: que cada test no se ensucie
con los datos de los demás. Si compartes la misma base y un test deja un pedido, el siguiente que cuente
«cuántos pedidos hay» ve la basura del primero y falla según el orden (M1.3, la independencia rota). Hay
tres estrategias clásicas, de menos a más fina:

- **Recrear la base entre tests** — borrar y volver a crear el esquema. Simple y seguro, pero lento;
  vale para suites pequeñas.
- **Limpiar los datos (respawn)** — vaciar las tablas conservando la estructura. Más rápido; en .NET la
  herramienta de referencia es **Respawn**, que borra respetando las relaciones. Buen equilibrio.
- **Transacción con rollback** — cada test abre una transacción y la deshace al final. Rápida y elegante,
  pero con letra pequeña: no pruebas bien el comportamiento transaccional de tu propio código si el test ya
  está dentro de una transacción.

En esta rama usamos la más simple posible: **una conexión SQLite in-memory por test**, que es una base
nueva cada vez. La independencia sale sola. Lo importante no es cuál uses, sino tener **una** y aplicarla a
todos los tests por igual.

---

## 7. Errores comunes

**No tener estrategia de aislamiento** y rezar para que los tests no se pisen (funciona hasta que el runner
cambia el orden). **Leer de la caché de EF**, del mismo contexto con el que escribiste (el test pasa aunque
el guardado esté mal). **Testear el repositorio con el provider in-memory** creyendo que pruebas el acceso a
datos (el `Include`, las consultas traducidas y las restricciones solo los valida un motor real). Y, de
diseño, **meter lógica de negocio en el repositorio**: el mostrador trae y lleva, no decide reglas; eso va
al dominio, donde lo testeas con unitarios rápidos.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M6.4-crud-y-aislamiento.md`](material/labs/M6.4-crud-y-aislamiento.md))
cierra el módulo con el CRUD completo del repositorio y la estrategia de aislamiento en el fixture. La
tarjeta ([`material/tarjetas/M6.4-repositorios.md`](material/tarjetas/M6.4-repositorios.md)) lo resume.

Con esto cierras el Módulo 6: sabes testear las costuras con la base de datos —cuándo (6.1), con el
provider in-memory y sus límites (6.2), con SQLite in-memory (6.3) y los repositorios con su aislamiento
(6.4)—. Lo que viene en el Módulo 7 es aprender a *leer* lo que esa suite te dice: cobertura sin engañarte,
tests que fallan al azar y los hábitos que la mantienen sana.
