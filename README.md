# SAT Recruitment

El objetivo de esta prueba es refactorizar el código de este proyecto.
Se puede realizar cualquier cambio que considere necesario en el código y en los test.


## Requisitos 

- Todos los test deben pasar.
- El código debe seguir los principios de la programación orientada a objetos (SOLID, DRY, etc...).
- El código resultante debe ser mantenible y extensible.


## Refactoring

Se separo la aplicacion en capas de Controlador, BusinessLogic y Datos

* IDataService se encarga de persistir los usuarios
* IApplictaionBL la logica de aplicacion
* Controller gestiona las peticiones

Se desacoplaron intefaces e implementaciones y se inyectan por dependencia

Se agregaron excepciones especificas visibles y no visibles a la vista de usuario

Se utilizo el patron Repository para gestionar los datos de manera es muy sencillo intercambiarlo por un Storage Relacional o No SQL.

### APi
Se siguio el starndar OpenApi para la creacion de los endpoints, y los codigos de respuesta

Endpoints:
* GET /users
* GET /users/:email
* POST /users

### Logica de Gift
La Logica de asignacion de premio monetario se ubico en la capa de logica de negocio y se ordeno para que sea facil de entender y mantner. 

Esta logica se podria hacer configurable por aplicacion y utilizar algun patron como Command + Composite para aplicar las reglas de negocio.

### Tests
Se agregaron tests para corroborar las validacioens requeridas al crear un nuevo usuario y las reglas de Gifts


### Otras mejoras posibles

Este es un caso de prueba basico y no se ve el alcance total del proycto.
Dependiendo de la complejidad, o como crece la aplicacion en caracteristicas se  podrian utlizar otras formas de organizacion e incorporar patrones especificos o herramientas como las siguientes:

* Utilizar la libreria MediatR que implementa el patron CQRS si fuera necesario desacoplar lecturas y escrituras

* Agregar Logs en la aplicacion para poder realziar analisis

* Utilizar DTOs u alguna libreria como Automapper para independizar las reqspuestas del API del modelo

* Utilizar metodos asincronicos



