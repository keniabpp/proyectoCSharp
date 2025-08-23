# 📌 AppTarea - Backend

Este es el **backend** de la aplicación **AppTarea**, desarrollado en **.NET 8** con una arquitectura en capas (Onion Architecture).  
Expone una **API REST** para la gestión de usuarios, tableros y tareas, documentada con **Swagger**.

---

## 🚀 Tecnologías utilizadas
- **C# .NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server** 
- **Docker & Docker Compose**
- **Swagger** (para documentación interactiva de la API)

---

## 📂 Estructura del proyecto
AppTarea/
├── AppTarea.sln # Solución principal .NET
├── docker-compose.yml # Orquestación con contenedores
├── src/ # Código fuente del backend
│ ├── Domain/ # Entities, Enums, Interfaces
│ ├── Application/ # DependencyInjection, MappingProfiles,
│ │ # Features, Handlers, Commands, Queries, DTOs
│ │ # (lógica de negocio)
│ ├── Infrastructure/ # DependencyInjection, Persistence, Repositorios
│ ├── Presentation/ # Controladores, Program, Properties, Dockerfile, appsettings.json
│ └── Tests/ # Pruebas unitarias (Application)
└── frontend/ # (futuro) cliente web


## ▶️ Cómo ejecutar el backend

### 1. Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) o Docker
- [Docker](https://www.docker.com/) (opcional)

### 2. Ejecutar con .NET CLI
```bash
cd AppTarea / cd src/Presentation
dotnet run

👉 http://localhost:5207
👉 Swagger en: http://localhost:5207/swagger


---

### ✅ 2. **Endpoints principales**  
Un resumen rápido de lo que ofrece tu API:  

```markdown
## 📖 Endpoints principales

- **Usuarios**
  - `GET /api/usuarios`
  - `POST /api/usuarios`
  - `PUT /api/usuarios/{id}`
  - `DELETE /api/usuarios/{id}`

- **Tableros**
  - `GET /api/tableros`
  - `POST /api/tableros`
  - `PUT /api/tableros/{id}`
  - `DELETE /api/tableros/{id}`

- **Tareas**
  - `GET /api/tareas`
  - `POST /api/tareas`
  - `PUT /api/tareas/{id}`
  - `DELETE /api/tareas/{id}`

👉 Documentación completa en Swagger:  
`http://localhost:5207/swagger`


## 🏛️ Arquitectura

El proyecto sigue el patrón **Onion Architecture**:

- **Domain** → Entidades, enums e interfaces base.  
- **Application** → Lógica de negocio, CQRS (Commands/Queries), DTOs y mapeos.  
- **Infrastructure** → Implementaciones de persistencia y repositorios.  
- **Presentation** → API con controladores, configuración y dependencias.  
- **Tests** → Pruebas unitarias.  

