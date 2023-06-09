# Ejercicio de evaluacion de candidatos .NET

## Introduccion

*Se dispone de una cadena de locales de oficinas distribuidos por toda la ciudad. Las oficinas se alquilan por 
hora individualmente. Se necesita un sistema que administre las reservas optimizando el uso de cada oficina
segun su capacidad y recursos disponibles (pizarra, proyector, acceso a internet, etc.).*


## Funcionalidad requerida

La interface *IOfficeRental* actua como punto de entrada para todas las operaciones.


**AddLocation**<br/>
Agregar un local nuevo.

**GetLocations**<br/> 
Obtener el listado de locales.

**AddOffice**<br/> 
Agregar una oficina a un local.

**GetOffices**<br/> 
Obtener todas las oficinas de un local.

**BookOffice**<br/> 
Reservar una oficina.

**GetBookings**<br/> 
Obtener un listado de reservas de una oficina.

**GetOfficeSuggestion**<br/> 
Obtener un listado de oficinas que coincidan con las especificaciones, ordenados por conveniencia.
Las sugerencias tiene que permitir la capacidad necesaria y tener todos los recursos solicitados.
Siempre es conveniente reservar una oficina en el barrio solicitado pero si no hay ninguna se pueden sugerir otros locales.
Tambien es prioridad mantener libres las oficinas mas grandes y con mas recursos de los que se requieren.


## Proyecto

**NetExam.Abstractions**

El proyecto define un conjunto de abstracciones a implementar para cumplir con la funcionalidad requerida en los tests.

**NetExam.Dto**

Los parametros de entrada se pasan mediante estos objetos.

**NetExam.Test**

Tests unitarios para validar la logica implementada.

## Resultado

Se espera que el candidato haga una implementacion de las abstracciones propuestas para cumplir con la funcionalidad requerida y que pase todos los tests.

No es necesario:
- Incluir dependencias externas al proyecto.
- Implementar una capa de persistencia de datos (todo se trabaja en memoria).
- Modificar ninguna de las clases e interfaces existentes.

Se considerara:
- Comprension de la problematica planteada
- Calidad de la solucion
- Deteccion de necesidades ocultas
- Prolijidad de los entregables
- Clean Code
- Principios SOLID
- Testeablidad
- Extensibilidad
- Manejo de errores
- Conceptos de Domain-Driven Design

La entrevista tecnica posterior consistie en la defensa de esta solucion.
