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

---

## ✅ Checklist de Requerimientos

### Backend
- [x] API en .NET 9 con MongoDB
- [x] DTOs con `IdOwner`, `Name`, `AddressProperty`, `PriceProperty`, `Image`
- [x] Endpoints:
  - `GET /api/property/getfiltered`
  - `GET /api/property/{id}`
- [x] Filtros por nombre, dirección y rango de precio
- [x] Paginación y ordenamiento
- [ ] Tests de repositorio (filtros + validaciones)
- [ ] Documentación Swagger validada

### Frontend
- [x] Next.js 15 (App Router)
- [x] Listado de propiedades con filtros
- [x] Modal de detalle con propietario e historial
- [x] Responsive con Tailwind
- [x] Ordenamiento por precio y nombre
- [ ] Tests adicionales (sin datos, render condicional)

### Infraestructura / Extras
- [ ] Docker Compose (`api`, `frontend`, `mongo`)
- [ ] GitHub Actions con build + tests
- [ ] Índices en MongoDB (`Name`, `Address`)

### Documentación
- [ ] **README.md** con instrucciones
- [ ] Backup MongoDB en `/db/backup`
- [ ] Variables de entorno (`MONGO_URI`, `PORT`, etc.)

---

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

# Restaurar backup
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