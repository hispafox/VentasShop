# Manual del alumno — M2.1 · Definir qué testear

Esto **no** es el [`README.md`](README.md). Este manual te cuenta el *porqué*: cómo mirar un sistema y
decidir, con criterio, qué merece un test y qué es ruido.

Tiempo de lectura: ~12 min. Submódulo M2.1 (Estrategia y Cobertura). Rama **conceptual**: aquí no se
escribe código de test todavía —las técnicas para diseñar los casos son M2.2—; aquí se decide el *qué*.

---

## 1. La idea en una frase

**Testear de más cuesta tanto como testear de menos.** El oficio está en cubrir lo que puede romperse
de verdad y dejar fuera lo que no aporta señal.

Testear de menos deja agujeros, eso es obvio. Pero cada test que no aporta señal —que nunca va a
fallar por un bug real— hay que mantenerlo, ralentiza la suite y, cuando se rompe por un cambio
legítimo, te hace perder una mañana investigando un falso problema. Una suite hinchada no es más
segura; es más lenta y más molesta, y acaba en el mismo sitio que una vacía: nadie le hace caso.

---

## 2. El gancho: el 80% que no protege

Una conversación que se repite: el jefe técnico vuelve de una charla y dice "quiero un 80% de
cobertura para final de trimestre". Tres meses después tienen su 80%… y siguen teniendo bugs en
producción.

¿Qué pasó? Que nadie hizo la pregunta de verdad. No es "¿cuánto testeamos?", es "**¿qué** testeamos?".
Para llegar al número, el equipo testeó lo fácil —getters, constructores, métodos de tres líneas— y la
regla que movía dinero (el cálculo de comisiones con sus seis casos especiales) seguía sin un test,
porque era la parte difícil. Optimizaron el número, no la seguridad. Y un número que no se corresponde
con la seguridad real es peor que no tener número: da tranquilidad sin dar protección.

---

## 3. Una casa, unos detectores de humo

Imagina que tienes que decidir dónde pones detectores de humo en una casa. No pones uno en cada metro
cuadrado —carísimo, y saltarían falsas alarmas cada vez que fríes un huevo, hasta que los desconectas
todos—. Ni pones cero. Piensas en dos cosas a la vez: **¿dónde es más probable que empiece un fuego?**
(la cocina, el cuadro eléctrico) y **¿dónde sería más catastrófico que pasara desapercibido?** (los
dormitorios). Los pones donde esas dos preguntas se cruzan.

Decidir qué testear es exactamente eso. Tu código es la casa; los tests, los detectores. No los pones
en todas partes (la suite hinchada que nadie mira) ni en ninguna (el proyecto sin red). Los pones
donde es probable que algo se rompa **y** donde, si se rompe, duele.

---

## 4. Lo que casi siempre se testea

- **Lógica de negocio.** Las reglas que definen qué hace tu aplicación y por las que alguien paga. En
  VentasShop, la cocina es el `CalculadoraDescuentos` (tramos por volumen, bonus por tipo de cliente,
  tope del 15%). Si falla, la empresa cobra de más o de menos a clientes reales todos los días.
- **Casos de borde.** Los bugs casi nunca están en el centro, están en los bordes: el valor límite
  exacto, el cero, el campo vacío, el `null`, la colección sin elementos. En VentasShop, el borde de
  `Cantidad` es el cero (¿`< 0` o `<= 0`?), y el de `Dinero` es sumar dos monedas distintas.
- **Las dos caras: happy path y error paths.** En producción los usuarios no leen el guion. El camino
  feliz (un pedido correcto se paga) y el de error (pagar un pedido sin líneas lanza una excepción de
  dominio, BR-07) se testean los dos. Los errores *de dominio* —los que tu código lanza a propósito—
  son parte del contrato tanto como el camino feliz.

La pregunta que decide: *si esto se equivoca, ¿qué pasa?* Si la respuesta es "perdemos dinero" o "el
cliente se va", tienes un candidato número uno. Si es "no se entera nadie", probablemente no.

---

## 5. Lo que NO se testea (aquí se nota el criterio)

Cuatro familias que, por defecto, no merecen un test propio:

1. **Getters/setters y código sin lógica.** Testear que C# sabe asignar una propiedad no caza ningún
   bug; solo sube el porcentaje. (Si la propiedad tiene lógica, ya es lógica de negocio y vuelve a la
   sección 4.)
2. **Detalles de implementación.** Testea el comportamiento (qué hace), no las tripas (cómo lo hace).
   Un test que sabe que usas una `List` en vez de un `Dictionary` se rompe en cada refactor aunque el
   comportamiento siga bien: has construido esposas, no una red.
3. **Código que no es tuyo.** No testees EF Core ni la librería de terceros. Sí testeas *tu
   integración* con ella —que tu repositorio guarda y recupera bien— pero eso es integración (Módulo 6).
4. **DTOs y código autogenerado** sin comportamiento.

---

## 6. El marco que lo une: riesgo × consecuencia

Ante cualquier trozo de código, dos preguntas: **¿qué pasa si esto se rompe?** (consecuencia) y
**¿cada cuánto cambia?** (volatilidad). El cruce decide:

```mermaid
quadrantChart
    title Que testear - riesgo x consecuencia
    x-axis Cambia poco --> Cambia a menudo
    y-axis Consecuencia baja --> Consecuencia alta
    quadrant-1 Maxima prioridad (la cocina)
    quadrant-2 Testear - red para cuando lo toques
    quadrant-3 Sin tests - el garaje vacio
    quadrant-4 Algun test ligero
    CalculadoraDescuentos: [0.85, 0.9]
    Regla fiscal anual: [0.2, 0.85]
    Getters / DTOs: [0.2, 0.15]
    Andamiaje volatil: [0.8, 0.25]
```

No es una fórmula, es una brújula. Te quita la ansiedad del "tengo que testearlo todo": **testea lo
que, si se rompe, te va a doler — y refuerza donde, además, se toca a menudo.**

---

## 7. Lo que te llevas

La foto de qué blindas y qué sueltas es la estrategia de testing de un proyecto en una servilleta, y
es una decisión de **criterio**, no de herramienta. Por eso este módulo va antes que cualquier xUnit:
de nada sirve saber escribir tests si los escribes para las cosas equivocadas.

El laboratorio ([`material/labs/M2.1-clasificar-que-testear.md`](material/labs/M2.1-clasificar-que-testear.md))
es justo eso: clasificar, sin escribir un test. Y la tarjeta
[`material/tarjetas/M2.1-que-testear.md`](material/tarjetas/M2.1-que-testear.md) la tienes para el día
a día.

Ya sabes *qué zonas* cubrir. Pero dentro de cada zona hay infinitos valores posibles. ¿Los pruebas
todos? Imposible. Hay cuatro técnicas que te dicen qué puñado de casos cubre de verdad el
comportamiento — y ahí empiezan los tests de verdad. Es el **M2.2**.
