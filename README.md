# SAT Recruitment

El objetivo de esta prueba es refactorizar el código de este proyecto.
Se puede realizar cualquier cambio que considere necesario en el código y en los test.


## Requisitos 

- Todos los test deben pasar.
- El código debe seguir los principios de la programación orientada a objetos (SOLID, DRY, etc...).
- El código resultante debe ser mantenible y extensible.

## CAMBIOS REALIZADOS (A modo de resumen general):
- Separación del proyecto en capas: API - Logica de Negocios - Acceso a datos
- Refactorización del código, clases y modelos.
- Se quitan las validaciones custom y se aplican DataAnnotations para validar campos obligatorios y de formato (email & phone).
- Implementación del patron repository.
- Se cambia en el sistema de archivos: dejamos de usar txt para usar json. Además, se encrypta de una manera sencilla (provista por el framework) el archivo.
- Se implementa inversión de dependencias.
- Se estandariza el controller:
  - Un solo controller por entidad.
  - Se quita el data annotation "Route" al controller de users. Llamamos a los metodos standard de REST, por ejemplo post para crear un usuario.
  - Se estandariza la respuesta del controller (ActionResult).
  
  <a href="mailto:andresmiretti@gmail.com" target="_blank">Andrés Miretti</a> | <a href="https://www.linkedin.com/in/amiretti/" target="_blank">LinkedIn</a>
