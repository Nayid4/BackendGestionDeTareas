# Backend - Gestor de Tareas

## Descripción del Proyecto

Este es el backend del proyecto **Gestor de Tareas**, una aplicación que permite la gestión de tareas (CRUD). Se ha desarrollado utilizando **.NET Core** y **Entity Framework Core**, aplicando principios de **Domain-Driven Design (DDD)**, **CQRS**, **Event Sourcing**, e implementando patrones de diseño como **Repositorio**, **Unit of Work**, e **Inyección de Dependencias**.

El backend está desplegado en **Azure App Services** y utiliza una base de datos **SQL Server** en Azure. Además, el proyecto puede ejecutarse en un entorno local con una base de datos local de SQL Server o mediante **Docker** usando `docker-compose`.

---

## Tecnologías y Librerías Utilizadas

- **.NET Core** - Framework principal
- **Entity Framework Core** - ORM para la gestión de la base de datos
- **MediatR** - Implementación del patrón Mediator
- **FluentValidation** - Validaciones en comandos y consultas
- **ErrorOr** - Manejo de errores estructurados con **ProblemDetails**
- **CQRS** - Separación de comandos y consultas
- **Event Sourcing** - Seguimiento de cambios en el dominio
- **Patrón Repositorio** y **Unit of Work** - Capa de persistencia estructurada

---

## Arquitectura del Proyecto

El backend sigue una arquitectura **Cliente-Servidor**, implementando una estructura de **Arquitectura Limpia**, que se compone de las siguientes capas:

### 1. **GestionDeTareas.API** (Capa de Presentación)
   - Contiene los **controladores** que exponen los endpoints RESTful.
   - Configura **middlewares**, **extensiones**, **CORS** y **variables de entorno**.
   - Contiene los archivos **Program.cs**, **appsettings.json**, y **Dockerfiles**.

### 2. **Aplicacion** (Capa de Aplicación)
   - Implementa los **casos de uso** mediante **CQRS**.
   - Contiene **validaciones** con FluentValidation.
   - Define **comandos** y **consultas**.

### 3. **Infraestructura** (Capa de Persistencia)
   - Gestiona la **persistencia de datos**.
   - Configura **modelos de entidades** para la base de datos.
   - Implementa **repositorios** y **migraciones**.

### 4. **Dominio** (Capa de Dominio)
   - Contiene las **entidades de dominio**.
   - Define las **reglas de negocio** y **eventos de dominio**.

---

## Instalación y Ejecución

### **1. Ejecución en Local**

#### **Requisitos Previos**
- .NET Core SDK
- SQL Server (local, en Docker o remota)
- Docker (opcional)

#### **Pasos**
1. Clonar el repositorio:
   ```sh
   git clone <URL_DEL_REPOSITORIO>
   cd backend-gestor-tareas
   ```
2. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Database": "Server=localhost;Database=GestorTareasDB;User Id=sa;Password=tu_contraseña;"
   }
   ```
   ```json
   "ConnectionStrings": {
     "Database": "Server=localhost;Database=gestionTareasDB;User Id=localhost;Integrated Security=True; Encrypt=false"
   }
   ```
3. Aplicar migraciones y actualizar la base de datos:
   ```sh
   dotnet ef database update
   ```
4. Ejecutar la API:
   ```sh
   dotnet run
   ```
5. La API estará disponible con ejecuto en http en: `http://localhost:5243`, con https en: `https://localhost:5243` o `https://localhost:7147`, y en IIS Express en: `https://localhost:44358`,
6. La API estará disponible con docker en: `http://localhost:8080`
7. La API desplegada estará disponible en: `https://gestion-de-tareas.azurewebsites.net`

---

### **2. Ejecución con Docker**

1. Construir la imagen Docker:
   ```sh
   docker-compose build
   ```
2. Levantar los contenedores:
   ```sh
   docker-compose up -d
   ```
3. La API estará disponible en: `http://localhost:8080`

---

## Endpoints Principales

### **Lista de Tareas**
| Método | Endpoint | Descripción |
|---------|---------|-------------|
| `GET`   | `/lista-de-tareas` | Obtener todas las tareas |
| `POST`  | `/lista-de-tareas` | Crear una nueva lista de tareas |
| `PUT`   | `/lista-de-tareas/{id}` | Actualizar una lista de tareas |
| `DELETE`| `/lista-de-tareas/{id}` | Eliminar una lista de tareas |
| `POST`| `/lista-de-tareas/filtrar-por-estado/{id}` | Obtener lista de tareas con sus tareas filtradas por estado |
| `POST`| `/lista-de-tareas/agregar-tarea/{id}` | Agregar una tarea |
| `POST`| `/lista-de-tareas/eliminar-tarea/{id}` | Eliminar una tarea |
| `POST`| `/lista-de-tareas/actualizar-tarea/{id}` | Actualizar una tarea |
| `POST`| `/lista-de-tareas/actualizar-estado-de-tarea/{id}` | Actualizar estado de una tarea |

---

## Despliegue en Azure

1. **Base de Datos:** Se despliega en **Azure SQL Database**.
2. **API:** Se despliega en **Azure App Services**.
3. **CI/CD:** Se configurar con **GitHub Actions** pero también se puede con otros como **Azure DevOps**.

---

## Contribución
Si deseas contribuir al proyecto:
1. Realiza un fork del repositorio.
2. Crea una nueva rama (`feature/nueva-funcionalidad`).
3. Envía un Pull Request con tus cambios.

---

## Contacto
Para dudas o sugerencias, puedes contactarme en **nayid2004@gmail.com**.

