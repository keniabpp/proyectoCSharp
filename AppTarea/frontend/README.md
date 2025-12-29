
# 📌 AppTarea - Frontend

Este es el **frontend** de la aplicación **AppTarea**, desarrollado en **Angular**.  
Se conecta con el backend en **.NET 8** para gestionar usuarios, tableros, columnas y tareas en un tablero estilo **Kanban**.  

---

## ✨ Funcionalidades principales

- 🔐 **Usuarios**
  - Registro de nuevos usuarios
  - Inicio y cierre de sesión
  - Edición de perfil


- 📊 **Dashboard**
  - Visualización general de los tableros
  - Selección de un tablero específico
  - Visualización de todas las tareas existentes en el tablero elegido

- 📋 **Gestión de tareas**
  - Crear tareas
  - Asignar tareas a otros usuarios
  - Ver las tareas que me han sido asignadas
  - Mover tareas entre columnas del tablero correspondiente (**solo el usuario asignado puede moverlas**)
  - Agregar detalles a las tareas que me han sido asignadas
  - El creador de la tarea puede **editarla o eliminarla**
  - El usuario asignado **no puede editar ni eliminar** la tarea
  - Las tareas completadas **no se pueden mover** a otra columna

- 📂 **Gestión de tableros**
  - Ver tableros disponibles

---

## 🛠️ Tecnologías utilizadas

- **Angular 20.2.2**
- **TypeScript**
- **Bootstrap** (para estilos)
- **RxJS** (manejo de estados y observables)
- **REST API** (consumo del backend en .NET 8)

---

## 🚀 Servidor de desarrollo

Para iniciar un servidor local, ejecutar:

```bash
ng serve





# Frontend

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 20.2.2.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests, the project currently uses [Jest](https://jestjs.io/). Use the following command:

```bash
npx jest
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.
