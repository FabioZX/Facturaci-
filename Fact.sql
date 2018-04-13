CREATE DATABASE SPfacturacion;

CREATE TABLE Articulos_Facturables(
IdArticulo INT IDENTITY(1,1) PRIMARY KEY,
Descripcion VARCHAR(20),
Precio INT,
Estado BIT
);

CREATE TABLE Clientes(
IdCliente INT IDENTITY(1,1) PRIMARY KEY,
Nombre_Comercial VARCHAR(20),
Cedula CHAR(11),
Estado BIT
);

CREATE TABLE Condiciones_Pagos(
IdCondicion INT IDENTITY(1,1) PRIMARY KEY,
Descripcion VARCHAR(20),
Cantidad_Dias INT,
Estado BIT
);

CREATE TABLE Vendedores(
IdVendedor INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(25),
Porciento_Comision FLOAT,
Estado BIT
);

CREATE TABLE Facturacion(
IdFacuracion INT IDENTITY(1,1) PRIMARY KEY,
Forma_Pago VARCHAR(20),
Fecha DATE,
Comentario VARCHAR(30),
Cantidad INT, 
Precio_Unitario INT,
IdArticulo INT FOREIGN KEY (IdArticulo) REFERENCES Articulos_Facturables(IdArticulo),
IdCliente INT FOREIGN KEY (IdCliente) REFERENCES Clientes (IdCliente),
IdCondicion INT FOREIGN KEY (IdCondicion) REFERENCES Condiciones_Pagos (IdCondicion),
IdVendedor INT FOREIGN KEY (IdVendedor) REFERENCES Vendedores (IdVendedor)
);