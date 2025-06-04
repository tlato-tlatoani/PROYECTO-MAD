USE GatoHotelero2;

GO;

CREATE OR ALTER TRIGGER CrearFactura
ON Reservacion
AFTER INSERT
AS
BEGIN
	INSERT INTO Factura (
		PrecioInicial,
		PrecioServicios,
		PrecioTotal,
		NombreDescuento,
		Descuento,
		Anticipo,
		FormaPago,
		Cliente,
		Hotel,
		Reservacion
	) SELECT
		0 AS PrecioInicial,
		0 AS PrecioServicios,
		0 AS PrecioTotal,
		'N/A' AS NombreDescuento,
		0 AS Descuento,
		i.Anticipo AS Anticipo,
		'Desconocido' AS FormaPago,
		i.Cliente,
		i.Hotel,
		i.CodReservacion AS Reservacion
	FROM inserted i;
END
GO;

CREATE OR ALTER TRIGGER CalcularHabitaciones
ON ReservacionHabitaciones
AFTER INSERT
AS
BEGIN
	UPDATE f SET 
		f.PrecioInicial = ISNULL(f.PrecioInicial, 0) + th.PrecioNoche * i.Personas * ABS(DATEDIFF(DAY, r.Entrada, r.Salida))
	FROM Factura f 
	JOIN inserted i ON 
		f.Reservacion = i.Reservacion 
	JOIN Reservacion r ON 
		r.CodReservacion = i.Reservacion 
	JOIN Habitacion h ON 
		i.Habitacion = h.Codigo
	JOIN TiposHabitacion th ON
		h.TipoHabitacion = th.CodTDH
	WHERE 
		f.Reservacion = i.Reservacion
	;

	UPDATE f SET 
		f.PrecioTotal = ((ISNULL(f.PrecioInicial, 0) + ISNULL(f.PrecioServicios, 0) - f.Anticipo) * (1 - (ISNULL(f.Descuento, 0) / 100))) 
	FROM Factura f 
	JOIN inserted i ON 
		f.Reservacion = i.Reservacion 
	WHERE 
		f.Reservacion = i.Reservacion
	;
END
GO;

CREATE OR ALTER TRIGGER CalcularFactura
ON ServiciosAdicionales
AFTER INSERT
AS
BEGIN
	UPDATE f SET 
		f.PrecioServicios = ISNULL(f.PrecioServicios, 0) + ISNULL(s.Precio, 0) 
	FROM Factura f 
	JOIN inserted i ON 
		f.Reservacion = i.Reservacion 
	JOIN Servicio s ON 
		s.CodServicio = i.Servicio 
	WHERE 
		f.Reservacion = i.Reservacion
	;

	UPDATE f SET 
		f.PrecioTotal = ((ISNULL(f.PrecioInicial, 0) + ISNULL(f.PrecioServicios, 0) - f.Anticipo) * (1 - (ISNULL(f.Descuento, 0) / 100))) 
	FROM Factura f 
	JOIN inserted i ON 
		f.Reservacion = i.Reservacion 
	WHERE 
		f.Reservacion = i.Reservacion
	;
END
GO;