# Manual del alumno — M8.3 · Taller aplicado (cierre del curso)

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué* del taller: aplicar todo lo
aprendido sobre código real, de una sentada, y salir con la primera red puesta y un plan para seguir.

Tiempo de lectura: ~10 min. Submódulo M8.3 — cierra el Módulo 8 y el curso.

---

## 1. Entrenar no es jugar

Durante el curso has entrenado con VentasShop: AAA mil veces, leer una cobertura, no fiarte de un verde,
abrir una costura. Entrenar es indispensable, pero el primer combate de verdad es otra cosa: el balón llega
como llega y decides con lo interiorizado. El taller es ese primer partido: código real, y le aplicas en
orden todo lo que sabes. No tienes que ganar la liga hoy; juegas un partido digno y sales con la lista de lo
que toca seguir.

---

## 2. El método, en 6 pasos

Sobre una clase (en el material, el legacy neutro `NotificadorPedidos` de M8.1; en la sala, una real):

1. **Triaje** (M8.1/M2.1): elige una clase que cruce riesgo alto y cambio frecuente.
2. **Diagnóstico + refactor mínimo** (M8.1): ¿se deja testear? Si tiene los clavos, abre las costuras con
   inyección de dependencias antes de un solo test.
3. **Decidir qué testear** (M2.1) y los casos, incluido el borde (M2.2): aquí, envía de día, no de noche
   (la frontera de las 22:00), registra siempre.
4. **Escribir los tests** con todo el oficio: dobles de NSubstitute (M5.2), reloj fijo (M7.2), AAA (M3.1),
   nombre que canta (M3.2), un comportamiento por test (M7.3). Mira
   [`Legacy/NotificadorPedidosTallerTests.cs`](tests/VentasShop.TestsUnitarios/Legacy/NotificadorPedidosTallerTests.cs).
5. **Medir** (M7.1/M7.2): `dotnet test -- --coverage` (Microsoft Code Coverage) + ReportGenerator; mira el
   amarillo; y con Stryker, si un mutante del `< 22` sobrevive, el verde no vigilaba.
6. **Gaps y próximos pasos** (M8.2): anota qué falta y cuál es la siguiente clase. Esa lista es el resultado
   más valioso del taller.

---

## 3. Lo que el taller NO es

No es **llenar el proyecto de tests** (son 10-15 sobre lo crítico). No es **dejar las clases perfectas**
(costura mínima, tests que importan, y seguir). No es **un examen** (lo que se atasca es la lista de lo que
seguirás puliendo). Aquí no se juzga; se practica con red.

---

## 4. Errores comunes

Atascarse en la clase más fea por orgullo (anótala como «candidata a rediseño» y pasa). Saltarse el
diagnóstico por las prisas (el orden hace que fluya). Escribir tests débiles por terminar (mejor tres de
verdad que diez vacíos). E irse sin la lista (el puente con el lunes).

---

## 5. La idea que cierra el curso

Empezamos con una idea y te la devuelvo con todo el peso detrás: **un test no demuestra que tu código
funciona; demuestra que sigue haciendo lo que tú decidiste que hiciera.** No has venido a perseguir un
porcentaje ni a coleccionar verdes: has venido a construir una red que avise, a ti y a tu equipo, el día que
alguien cambie sin querer lo que el sistema tenía que hacer. Esa red ya sabes tejerla. Ahora ve a ponerla
donde más importa.

---

## 6. Para seguir (que es lo mismo que practicar)

Coge la siguiente clase crítica de tu proyecto y haz tú solo la secuencia completa, de triaje a lista de
gaps ([`material/labs/M8.3-taller.md`](material/labs/M8.3-taller.md)). Si te sale, el curso ha hecho su
trabajo. Si te atascas en algún paso, ya sabes a qué módulo volver — y eso también es haber aprendido.
