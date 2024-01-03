use FACTURACIONDB

go

create procedure INSERTAR_CLIENTE
	@Nombres varchar(50),
    @Apellidos varchar(50),
    @Cedula varchar(50),
    @Telefono int,
    @Correo varchar(50),
    @Direccion varchar(100)
as
begin
	insert into CLIENTES (Nombres, Apellidos, Cedula, Telefono, Correo, Direccion)
    values (@Nombres, @Apellidos, @Cedula, @Telefono, @Correo, @Direccion)
end

go

create procedure INSERTAR_FACTURA
	@IdCliente int,
    @BaseImponibleCero decimal(12,2),
    @BaseImponibleDoce decimal(12,2),
    @Subtotal decimal(12,2),
    @IVA decimal(12,2),
    @Total decimal(12,2)
as
begin
	insert into FACTURAS (IdCliente, BaseImponibleCero, BaseImponibleDoce, Subtotal, IVA, Total)
	values (@IdCliente, @BaseImponibleCero, @BaseImponibleDoce, @Subtotal, @IVA, @Total)
end

go

create procedure INSERTAR_DETALLE_FACTURA
    @IdFactura int,
    @DescripcionProducto varchar(255),
    @Cantidad int,
    @PrecioUnitario decimal(12,0),
    @IVAProducto int,
    @TotalProducto decimal(12,2)
AS
BEGIN
    INSERT INTO DETALLES_FACTURA (IdFactura, DescripcionProducto, Cantidad, PrecioUnitario, IVAProducto, TotalProducto)
    VALUES (@IdFactura, @DescripcionProducto, @Cantidad, @PrecioUnitario, @IVAProducto, @TotalProducto)
END
GO


CREATE PROCEDURE ObtenerUltimoIdFactura
AS
BEGIN
    SELECT COALESCE(MAX(IdFactura), 1) AS UltimoIdFactura FROM FACTURAS;
END
GO

EXEC ObtenerUltimoIdFactura;




