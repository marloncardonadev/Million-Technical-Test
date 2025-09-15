// Definir base de datos y usuario de aplicación
const dbName = 'MillionRealEstateDb';
const appUser = 'appuser';
const appPwd = 'apppassword';

// Cambiamos a la base de datos principal
db = db.getSiblingDB(dbName);

// Crear usuario de aplicación con permisos de lectura/escritura
db.createUser({
  user: appUser,
  pwd: appPwd,
  roles: [{ role: 'readWrite', db: dbName }]
});

// --------------------
// 1) Colección: Owner
// --------------------
db.createCollection('Owner');
db.Owner.insertMany([
  { 
    _id: ObjectId(), 
    Name: "Carlos López", 
    Address: "Calle 10 #5-55", 
    Photo: "https://example.com/owners/carlos.jpg",
	Birthday: new Date("1980-03-15")
  },
  { 
    _id: ObjectId(), 
    Name: "María Pérez", 
    Address: "Carrera 50 #22-11",
    Photo: "https://example.com/owners/maria.jpg",
	Birthday: new Date("1990-08-25")
  }
]);

// --------------------
// 2) Colección: Property
// --------------------
db.createCollection('Property');
db.Property.insertMany([
  {
    _id: ObjectId(),
    IdOwner: db.Owner.findOne({ Name: "Carlos López" })._id,
    Name: "Apartamento Centro",
    Address: "Calle 123 #45-67, Medellín",
    Price: 250000.0,
    CodeInternal: "APT001",
    Year: 2021
  },
  {
    _id: ObjectId(),
    IdOwner: db.Owner.findOne({ Name: "María Pérez" })._id,
    Name: "Casa Jardín",
    Address: "Carrera 10 #12-34, Envigado",
    Price: 420000.0,
    CodeInternal: "CASA001",
    Year: 2019
  }
]);

// --------------------
// 3) Colección: PropertyImage
// --------------------
db.createCollection('PropertyImage');
db.PropertyImage.insertMany([
  {
    _id: ObjectId(),
    IdProperty: db.Property.findOne({ Name: "Apartamento Centro" })._id,
    File: "https://example.com/imgs/apto-centro-1.jpg",
    Enabled: true
  },
  {
    _id: ObjectId(),
    IdProperty: db.Property.findOne({ Name: "Casa Jardín" })._id,
    File: "https://example.com/imgs/casa-jardin-1.jpg",
    Enabled: true
  }
]);

// --------------------
// 4) Colección: PropertyTrace
// --------------------
db.createCollection('PropertyTrace');
db.PropertyTrace.insertMany([
  {
    _id: ObjectId(),
    IdProperty: db.Property.findOne({ Name: "Apartamento Centro" })._id,
    DateSale: new Date("2023-01-15"),
    Name: "Venta inicial",
    Value: 250000.0,
    Tax: 25000.0
  },
  {
    _id: ObjectId(),
    IdProperty: db.Property.findOne({ Name: "Casa Jardín" })._id,
    DateSale: new Date("2022-07-10"),
    Name: "Venta inicial",
    Value: 420000.0,
    Tax: 42000.0
  }
]);

// --------------------
// Índices recomendados
// --------------------
db.Property.createIndex({ Name: "text", Address: "text" });
db.Property.createIndex({ Price: 1 });