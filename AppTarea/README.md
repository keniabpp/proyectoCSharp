📌 AppTarea - Backend

Este es el **backend** de la aplicación **AppTarea**, desarrollado en **.NET 8** con una arquitectura en capas (Onion Architecture).  
Expone una **API REST** para la gestión de usuarios, tableros, columnas y tareas, documentada con **Swagger**.



🚀 Tecnologías utilizadas
- **C# .NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server** 
- **Docker & Docker Compose**
- **Swagger** (para documentación interactiva de la API)



📂 Estructura del proyecto
AppTarea/
AppTarea.sln # Solución principal .NET
docker-compose.yml # Orquestación con contenedores
src/ # Código fuente del backend
Domain/ # Entities, Enums, Interfaces
 Application/ # DependencyInjection, MappingProfiles,
  # Features, Handlers, Commands, Queries, DTOs
  # (lógica de negocio)
Infrastructure/ # DependencyInjection, Persistence, Repositorios
 Presentation/ # Controladores, Program, Properties, Dockerfile, appsettings.json
 Tests/ # Pruebas unitarias (Application)
  frontend/ # (futuro) cliente web


 ▶️ Cómo ejecutar el backend


- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) o Docker
- [Docker](https://www.docker.com/) (opcional)

 Ejecutar con .NET CLI
```bash
cd AppTarea / cd src/Presentation
dotnet run

👉 http://localhost:5207
👉 Swagger en: http://localhost:5207/swagger



docker-compose up --build
Esto levantará:

SQL Server en localhost:1433

Backend (API) en http://localhost:5208

Frontend (Angular) en http://localhost:4200

⚡ La API estará disponible en Swagger en:
👉 http://localhost:5208/swagger


---

Endpoints principales
 


## 📖 Endpoints principales

- **Usuarios**
  - `POST /api/usuarios/register`
  - `POST /api/usuarios/login`
  - `GET /api/usuarios`
  - `GET /api/usuarios/{id}`
  - `POST /api/usuarios`
  - `PUT /api/usuarios/{id}`
  - `DELETE /api/usuarios/{id}`

- **Tableros**
  - `GET /api/tableros`
  - `GET /api/tableros/{id}`
  - `POST /api/tableros`
  - `PUT /api/tableros/{id}`
  - `DELETE /api/tableros/{id}`

- **Columnas**
  - `GET /api/columnas`
  - `GET /api/columnas/{id}`
  

- **Tareas**

  - `GET /api/tareas/tareasAsignadas`
  - `GET /api/tareas`
  - `GET /api/tareas/{id}`
  - `POST /api/tareas`
  - `PUT /api/tareas/{id}`
  - `DELETE /api/tareas/{id}`
  - `PUT /api/tareas/{id_tarea}/moverTarea



👉 Documentación completa en Swagger:  
`http://localhost:5207/swagger`


🏛️ Arquitectura

El proyecto sigue el patrón **Onion Architecture**:

- **Domain** → Entidades, enums e interfaces base.  
- **Application** → Lógica de negocio, CQRS (Commands/Queries), DTOs y mapeos.  
- **Infrastructure** → Implementaciones de persistencia y repositorios.  
- **Presentation** → API con controladores, configuración y dependencias.  
- **Tests** → Pruebas unitarias.  



## 🧪 Pruebas unitarias (Backend)

Para ejecutar los tests unitarios del backend, usa el siguiente comando desde la carpeta src/Tests:

```bash
dotnet test
```

Esto ejecutará todas las pruebas del backend y mostrará el resultado en la terminal

