<a name="0.0.1"></a>
# 0.0.8 (2022-05-28)
## Se  dio extensibilidad al origen de datos de usuarios.
### Se implemento la lectura desde un archivo
### Pero es escalable a cualquier tipo de origen de datos (db por ejemplo)

# 0.0.7 (2022-05-27)
## Refactoreo masivo y general.
### Cambiaron de nombres muchas clases
### Se crearon conjuntos de objetos para atender responsabilidades unicas.
### Se hizo una division inicial en layers. Pero a nivel carpetas.

# 0.0.6 (2022-05-26)
## Se refactorean parametros
### Se agruparon parametros sueltos en una misma class.

# 0.0.5 (2022-05-26)
## cambios en configuracion
### Swagger
- Se corrigio su visiblidad
 
# 0.0.4 (2022-05-26)
## refactoreo
- Se elimino partial class. No es responsabilidad del controller leer un source data.

# 0.0.3 (2022-05-25)
## Se cambio seteo de project

# 0.0.2 (2022-05-25)
## Se agrego inyeccion de dependencias

# 0.0.1 (2022-05-25)
## Cambios estructurales
### Carpetas
- Se creo carpeta ApiModels para guardar objetos de intercambio (DTO) entre Cliente y Apis.
- Se movieron clases Result y User a ApiModels
