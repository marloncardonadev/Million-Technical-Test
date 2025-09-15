# Prueba Técnica – Desarrollador Fullstack Senior

Este proyecto implementa una aplicación **Fullstack** para la prueba técnica de una empresa inmobiliaria.  
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

# Cómo Ejecutar el Proyecto

## 1. Backend (.NET 9)
    1. Ir a la carpeta del backend:
      cd Million-Technical-Test\Million.RealEstate.Backend\Million.RealEstate.Backend.Api
    2. Restaurar y ejecutar la API:
      dotnet restore
      dotnet build
      dotnet run
    3. La API estará disponible en:
      API en: https://localhost:7177
      Swagger: https://localhost:7177/index.html

## 2. Frontend
    1. Ir a la carpeta del frontend:
      cd Million-Technical-Test\million-realestate-app
    2. Instalar dependencias:
      npm install
    3. Ejecutar el servidor de desarrollo:
      npm run dev
    4. Abrir en el navegador:
      App en: http://localhost:3000

## 3. MongoDB
    1. Restaurar backup
      mongorestore --db MillionRealEstateDb ./Million-Technical-Test\db-mongo\mongo-backup/MillionRealEstateDb

## 4. Testing
  ### 1. Backend (NUnit)
    1. Ir a la carpeta: 
      cd Million-Technical-Test\Million.RealEstate.Backend\Million.RealEstate.Backend.Tests
    2. Ejecutar pruebas:
      dotnet test

  ### 2. Frontend (Jest)
    1. Ir a la carpeta
      cd Million-Technical-Test\million-realestate-app
    2. Ejecutar pruebas: 
      npm test

## 5. Docker 
    1. Ir a la carpeta
      cd Million-Technical-Test\db-mongo
    2. Levantar contenedor de base de datos MongoDB:
      docker-compose up --build


---

## 👨‍💻 Autor

**Marlon Orlando Cardona Jaramillo**  
- 💼 Desarrollador Fullstack | .NET, Angular, React | Cloud (Azure & AWS) 
- 📧 marlon18_@hotmail.com
- 🔗 [LinkedIn](www.linkedin.com/in/marlon880215)

---

## 📄 Licencia

Este proyecto se entrega bajo la licencia **MIT**.  
Eres libre de usar, modificar y distribuir este código, siempre y cuando se mantenga la atribución al autor original.

---

## 🙌 Créditos

Este proyecto fue desarrollado como parte de la **Prueba Técnica – Desarrollador Fullstack Senior** para una empresa del sector inmobiliario.  
Incluye implementación **fullstack** con **.NET 9, MongoDB y Next.js** siguiendo principios de **Clean Architecture**, pruebas unitarias y documentación.
