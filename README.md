# Prueba Técnica – Desarrollador Fullstack Senior

Este proyecto implementa una aplicación **Fullstack** para una empresa inmobiliaria.  
Incluye una **API en .NET 9 con MongoDB** y un **frontend en Next.js 15** para mostrar propiedades con filtros y detalles.  

---

## Arquitectura del Proyecto

### Backend (.NET 9, C#, MongoDB)
- **Clean Architecture**:
  - `Domain` → Entidades (Property, Owner, PropertyTrace).
  - `Application` → Casos de uso, DTOs, validaciones.
  - `Infrastructure` → Repositorios MongoDB (Repository Pattern).
  - `API` → Endpoints minimal APIs en .NET 9.
- **Filtros soportados**:
  - `name`
  - `address`
  - `priceMin`
  - `priceMax`
- **Paginación y ordenamiento** por `name` o `price`.
- **Swagger** disponible en `/swagger`.

### Frontend (Next.js 15 + TailwindCSS)
- **App Router** (`/app`).
- **Páginas**:
  - `/properties` → listado de propiedades con filtros.
  - Modal de detalle con información del propietario y transacciones.
- **UI Responsive** con TailwindCSS.
- **Filtros y ordenamiento** en la interfaz.

---

## Tecnologías
- **Backend**: .NET 9, C#, MongoDB, Swagger
- **Frontend**: Next.js 15, React, TailwindCSS
- **Pruebas**:
  - Backend → NUnit
  - Frontend → Jest + React Testing Library


## Cómo Ejecutar el Proyecto

### 1. Backend (.NET 9)
  1. Ir a la carpeta del backend:
    cd Million.RealEstate.Backend\Million.RealEstate.Backend.Api
  2. Restaurar y ejecutar la API:
    dotnet restore
    dotnet build
    dotnet run
  3. La API estará disponible en:
     API en: https://localhost:7177
     Swagger: https://localhost:7177/swagger

### 2. Frontend
  1. Ir a la carpeta del frontend:
    cd million-realestate-app
  2. Instalar dependencias:
    npm install
  3. Ejecutar el servidor de desarrollo:
    npm run dev
  4. Abrir en el navegador:
    App en: http://localhost:3000

### 3. MongoDB
  1. Restaurar backup
    mongorestore --db MillionRealEstateDb ./db-mongo/mongo-backup/MillionRealEstateDb

### Testing

### Backend (NUnit)

cd Million.RealEstate.Backend.Tests
dotnet test

### Frontend (Jest)

cd million-realestate-app
npm test

## Docker 

docker-compose up --build
