# Manual del alumno — M8.2 · Definir la estrategia

Esto **no** es el [`README.md`](README.md). El manual te cuenta el *porqué*: por qué a un sistema en
producción no se le ponen tests a lo loco, sino con un plan por fases que se ejecuta sin pararlo.

Tiempo de lectura: ~11 min. Submódulo M8.2.

---

## 1. El entusiasmo no es una estrategia

En M8.1 diagnosticaste y abriste costuras. Pero sin plan, el equipo empieza con ganas, escribe tests a salto
de mata y a las tres semanas la iniciativa muere con cuarenta tests dispersos. Y hay una restricción que lo
cambia todo: **el sistema está en producción y no se puede parar.** No hay «paramos dos meses». Hay que
mejorar la red de un sistema que funciona, sin apagarlo.

---

## 2. Reparar el barco sin dejar de navegar

Un barco viejo que navega, cargado, sin puerto cerca: hay que repararlo en marcha. Aseguras primero lo que
**te hundiría** (las vías de agua en la línea de flotación), luego los sistemas de maniobra (timón, bombas)
y por último los acabados. Tabla a tabla, sin parar. Poner tests a un sistema en producción es esto.

---

## 3. El plan por fases: F1, F2, F3

**F1 — la línea de flotación.** Tests **unitarios** del núcleo de negocio crítico (lo que mueve dinero y
reglas). Es la fase de mayor retorno: partiendo de cero, los primeros tests quitan los riesgos más gordos a
coste bajísimo (la curva de M2.3, al revés). Aplica las costuras de M8.1 + dobles (M5).

**F2 — los sistemas de maniobra.** Tests de **integración** (M6) en las fronteras: base de datos, servicios
externos. Cubren fallos que el unitario no ve (una restricción de BD, un mapeo de EF mal puesto). Recuerda
M6.3: hay fallos que solo caza el motor real (SQLite in-memory). Más lenta y cara; por eso va después.

**F3 — el automatismo.** El **gate de CI** (aquí se monta; M7.3 solo lo nombró). Un servidor (GitHub Actions,
Azure Pipelines) ejecuta la suite en cada cambio y, si algo falla, ese código no entra. Sobre esa base:
cobertura en la misma pasada (`dotnet test -- --coverage`, Microsoft Code Coverage; **no Coverlet**), informe
de ReportGenerator, o SonarQube. Y no todo va en cada push: unitarios siempre, integración en cada merge, lo
pesado (mutation testing) de noche. Ver [`material/ci-ejemplo.yml`](material/ci-ejemplo.yml).

---

## 4. Un objetivo de cobertura que no sea mentira

**No** fijes un 80% global con fecha: o desmoraliza o invita a la trampa de M2.3. **Sí** un objetivo por
**zona** (la lógica crítica de F1 alto, cerca del 80-90% de ramas; el andamiaje bajo) y **creciente**:
cobertura sobre código nuevo (M7.1). La global sube sola, como consecuencia de trabajar bien.

---

## 5. Confirmar el stack

Antes del primer test, decide con el equipo qué herramientas: ¿se adopta el stack del curso (xUnit v3,
NSubstitute, AwesomeAssertions, Microsoft Code Coverage/MTP) o se respeta el que el proyecto ya arrastra? Es
una decisión de una vez, por escrito antes de empezar. Cambiarla con cincuenta tests hechos es de lo más caro
que hay.

---

## 6. Errores comunes

Querer **parar el barco** (el negocio no lo acepta). **Empezar por F2/F3** (la pintura antes que el casco).
El **objetivo global con fecha** (la trampa de M2.3 a escala de proyecto). Y **planificar para siempre**: el
plan bueno cabe en una página y se empieza.

---

## 7. Lo que te llevas

El laboratorio ([`material/labs/M8.2-hoja-de-ruta.md`](material/labs/M8.2-hoja-de-ruta.md)): conviertes el
diagnóstico de M8.1 en una hoja de ruta F1-F2-F3 en una página, con objetivo por zona y un esbozo del gate.
La entrega es el plan, no tests. En M8.3, el taller: bajamos al código real y escribimos los primeros tests
aplicando todo el curso. Cierra el curso.
