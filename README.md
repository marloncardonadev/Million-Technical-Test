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

### Backend
cd Million.RealEstate.Backend
dotnet restore
dotnet run

API en: https://localhost:7177
Swagger: https://localhost:7177/swagger

### Frontend
cd million-realestate-app
npm install
npm run dev

App en: http://localhost:3000

### MongoDB

Restaurar backup
mongorestore --db RealEstate ./db/backup

## Testing

### Backend (NUnit)

cd Million.RealEstate.Backend.Tests
dotnet test

### Frontend (Jest)

cd million-realestate-app
npm test

## Docker 

docker-compose up --build
