# VideoMatrixApp

Aplicación desarrollada con Blazor Server, Entity Framework Core y MySQL para la gestión de dispositivos, conexiones y perfiles de una matriz de vídeo.

## Requisitos

* .NET 10 SDK
* MySQL Server
* Git

## Clonar el repositorio

```bash
git clone https://github.com/PDanielFG/VideoMatrixApp.git
```

## Acceder al directorio del proyecto

```bash
cd VideoMatrixApp/VideoMatrixApp
```

## Configuración de la base de datos

La aplicación utiliza MySQL mediante Entity Framework Core.

Antes de ejecutar el proyecto existen dos opciones:

### Opción 1 (Recomendada)

Revisar la cadena de conexión definida en:

```text
appsettings.json
```

y adaptarla a la configuración local de MySQL.

Ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=matrixvideoapp;user=root;password="
}
```

### Opción 2

Crear un usuario MySQL con las mismas credenciales utilizadas por defecto en el proyecto:

```text
Usuario: root
Contraseña: (vacía)
```

## Restaurar dependencias

```bash
dotnet restore
```

## Crear y actualizar la base de datos

```bash
dotnet ef database update
```

Este comando creará la base de datos y aplicará automáticamente las migraciones incluidas en el proyecto.

## Ejecutar la aplicación

```bash
dotnet run
```

## Acceder a la aplicación

Una vez iniciada, abrir el navegador y acceder a:

```text
http://localhost:5213
```

## Funcionalidades implementadas

* Gestión de dispositivos: creación, edición y eliminación de transmisores y receptores.
* Gestión de conexiones: asignación de transmisores a receptores.
* Visualización en directo de transmisores y receptores mediante imágenes estáticas.
* Gestión de perfiles para guardar configuraciones de la matriz de vídeo.
* Aplicación rápida de perfiles previamente guardados.
* Persistencia de datos mediante MySQL y Entity Framework Core.
* Carga automática de datos iniciales al iniciar la aplicación.

## Tecnologías utilizadas

* .NET 10
* C#
* Blazor Server
* Entity Framework Core
* MySQL
* LINQ
* Bootstrap
