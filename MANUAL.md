# Manual del alumno — M6.1 · Tests unitarios vs. tests de integración

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué los tests con dobles no
bastan, qué tipo de bug solo aparece contra una base de datos real, y cómo repartir tu suite sin penalizar
el ciclo. **Abre el Módulo 6.** Es conceptual: el código contra BD llega en 6.2, 6.3 y 6.4.

Tiempo de lectura: ~10 min. Submódulo M6.1 (Tests de Integración y Base de Datos).

---

## 1. La idea en una frase

El test unitario prueba que cada pieza funciona; el de integración prueba que las piezas encajan. No es uno o
el otro: hacen falta los dos, en la proporción de la pirámide (M1.2).

---

## 2. La espina que dejó M5

En M5 testeaste el `ServicioPedidos` sustituyendo sus dependencias por dobles. Rápido y limpio, pero con un
punto ciego: el doble del repositorio te devuelve el pedido que tú le dijiste que devolviera. Nunca compruebas
si tu `RepositorioPedidos` real guarda y recupera bien contra la base de datos. En esa costura viven bugs que
el unitario no ve.

---

## 3. Cada músico y el ensayo general

Una orquesta antes del concierto. Cada músico ensaya su parte por separado y verifica que la borda: eso es el
unitario, rápido y preciso. Pero que cada uno toque bien por su cuenta no garantiza que el conjunto suene, y
por eso existe el ensayo general, con todos en la sala real. Ahí aparece lo que el ensayo individual nunca
revela: que dos instrumentos desafinan entre sí, que una entrada llega tarde. Eso es la integración.

---

## 4. Lo que solo caza la integración

Cuatro clases de bug que el doble no puede ver, porque ni toca la base de datos:

- **Mapeo** entidad ↔ tabla: que una propiedad acabe en la columna correcta, con el tipo correcto.
- **Traducción LINQ → SQL**: una consulta que compila en memoria puede no traducirse, o dar otro resultado.
- **Restricciones**: un `NOT NULL`, una clave única, una foránea. Tu código cree que guardó; la BD lo rechaza.
- **Transacciones y concurrencia**: que un rollback deshaga de verdad, que dos operaciones no se pisen.

---

## 5. La pirámide manda

La regla es la de M1.2: muchos unitarios, pocos de integración. Los unitarios, para toda la lógica de negocio
(la base ancha, rápidos y constantes). La integración, solo para las costuras con el mundo exterior que de
verdad importan. Abusar de la integración «porque es más realista» te lleva al cono de helado: una suite lenta
que nadie ejecuta.

---

## 6. El coste, y cómo no penalizar el ciclo

La integración es lenta. Tres medidas evitan que te frene:

1. **Proyectos separados**: `VentasShop.TestsUnitarios` y `VentasShop.TestsIntegracion` (ambos ya existen en
   el repo). Ejecutas solo los rápidos mientras desarrollas.
2. **Momentos distintos**: los unitarios a cada cambio; los de integración antes de un commit importante y en
   la integración continua (M7).
3. **Comparte lo caro**: levanta la base de datos una vez por clase, no una por test (fixtures, M6.3).

---

## 7. El reparto de VentasShop

A **unitario** va toda la lógica: `CalculadoraDescuentos`, las invariantes de `Cantidad` y `Dinero`, las
transiciones del `Pedido`, el `ServicioPedidos` con dobles. A **integración** va poco y bien elegido: que el
`RepositorioPedidos` real guarda un pedido con sus líneas y lo recupera idéntico, una consulta «pedidos de un
cliente» contra el motor, y una restricción real (que no se pueda guardar un pedido sin cliente). El descuento
se queda en unitario: es lógica pura, no cruza la frontera de la base de datos.

---

## 8. Lo que te llevas

El laboratorio ([`material/labs/M6.1-clasificar-unit-integracion.md`](material/labs/M6.1-clasificar-unit-integracion.md))
es de **criterio**: clasificas cada parte de VentasShop como unitario o integración con una sola pregunta,
«¿cruza la frontera de la base de datos?». La tarjeta
([`material/tarjetas/M6.1-unit-vs-integracion.md`](material/tarjetas/M6.1-unit-vs-integracion.md)) lo resume.

Con ese reparto claro, en M6.2 escribes el primer test contra base de datos con el provider **en memoria** de
EF Core: el más rápido y el más tentador. Verás cómo se usa y en qué te engaña sobre las restricciones, antes
de pasar a bases de datos reales efímeras con Testcontainers (6.3) y al testeo de repositorios (6.4).
