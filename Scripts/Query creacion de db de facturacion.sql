create database FACTURACIONDB

go

use FACTURACIONDB

go

--Tabla de clientes
create table CLIENTES(
IdCliente int primary key identity(1,1),
Nombres varchar(50) not null,
Apellidos varchar(50)not null,
Cedula varchar(50) not null,
Telefono int not null,
Correo varchar(50) not null,
Direccion varchar (100)not null,
FechaDeCreacion datetime default getdate()
)

go

--Tabla de facturas
create table FACTURAS(
IdFactura int primary key identity (1,1),
IdCliente int  foreign key references CLIENTES(IdCliente),
BaseImponibleCero decimal(12,2) default 0.0,
BaseImponibleDoce decimal(12,2) default 0.0,
Subtotal decimal(12,2) default 0.0,
IVA decimal(12,2) default 0.0,
Total decimal (12, 2),
FechaDeCreacion datetime default getdate()
)

go

--Tabla Detalles Factura
create table DETALLES_FACTURA(
IdDetalleFactura int primary key identity (1,1),
IdFactura int foreign key references FACTURAS(Idfactura),
DescripcionProducto varchar(255) not null,
Cantidad int not null,
PrecioUnitario decimal (12,0) not null,
IVAProducto int, 
TotalProducto decimal (12, 2),
FechaDeCreacion datetime default getdate()
)

go


DELETE FROM DETALLES_FACTURA;
DELETE FROM FACTURAS;
DELETE FROM CLIENTES;


DBCC CHECKIDENT ('CLIENTES', RESEED, 0);
DBCC CHECKIDENT ('FACTURAS', RESEED, 0);
DBCC CHECKIDENT ('DETALLES_FACTURA', RESEED, 0);

SELECT * FROM [FACTURACIONDB].[dbo].[FACTURAS]
SELECT * FROM [FACTURACIONDB].[dbo].[DETALLES_FACTURA]
SELECT * FROM [FACTURACIONDB].[dbo].[CLIENTES]

