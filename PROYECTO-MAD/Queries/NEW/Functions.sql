USE GatoHotelero2;

GO;

CREATE OR ALTER FUNCTION FuncHabitaciones (@YEAR INT)
RETURNS TABLE
AS
RETURN
(
	SELECT 
		Codigo,
		NoHabitacion,
		Piso,
		TipoHabitacion,
		NivelHabitacion,
		NombreHotel,
		NoCamas,
		CantPersonasMax,
		PrecioNoche,
		CASE
			WHEN (
				SELECT COUNT(rh.Id) 
				FROM ReservacionHabitaciones rh 
				JOIN Reservacion rv ON rh.Reservacion = rv.CodReservacion 
				WHERE 
					rv.Estatus != 'Cancelado' AND
					rh.Habitacion = Codigo AND 
					@YEAR = YEAR(rv.CheckIn)
			) >= 1 THEN 'Ocupado'
			ELSE 'Desocupado'
		END AS Estatus,
		(
			SELECT TOP 1 rv.CodReservacion
			FROM ReservacionHabitaciones rh 
			JOIN Reservacion rv ON rh.Reservacion = rv.CodReservacion 
			WHERE 
				rv.Estatus != 'Cancelado' AND
				rh.Habitacion = Codigo AND 
				@YEAR = YEAR(rv.Salida)
		) AS Reservacion,
		(
			SELECT TOP 1 rh.Personas
			FROM ReservacionHabitaciones rh 
			JOIN Reservacion rv ON rh.Reservacion = rv.CodReservacion 
			WHERE 
				rv.Estatus != 'Cancelado' AND
				rh.Habitacion = Codigo AND 
				@YEAR = YEAR(rv.Salida)
		) AS Hospedaje
	FROM 
		Habitacion 
		JOIN TiposHabitacion ON CodTDH = TipoHabitacion 
		JOIN Hotel ON CodHotel = idHotel
);
GO;

/*reporte ventas*/
CREATE OR ALTER FUNCTION FuncVentas (
	@Pais NVARCHAR(100),
	@Year INT,
	@Ciudad NVARCHAR(100),
	@Hotel NVARCHAR(100)
)RETURNS TABLE AS RETURN (
	SELECT
		h.Ciudad,
		h.NombreHotel AS Hotel,
		--2025 AS Año,
		YEAR(r.Entrada) AS Año,
		MONTH(r.Entrada) AS Mes,
		SUM(f.PrecioInicial) AS Hospedaje,
		SUM(f.PrecioServicios) AS Servicios,
		SUM(f.PrecioTotal) AS Total
	FROM Hotel h
	JOIN Reservacion r ON
		r.Hotel = h.CodHotel
	JOIN Factura f ON
		f.Reservacion = r.CodReservacion
	GROUP BY
		h.Ciudad,
		h.NombreHotel,
		YEAR(r.Entrada),
		MONTH(r.Entrada)
);

go

/*reporte ocupaciones 2da tabla*/
CREATE OR ALTER FUNCTION FuncOcupaciones (
	@Pais NVARCHAR(100),
	@Year INT,
	@Ciudad NVARCHAR(100),
	@Hotel NVARCHAR(100)
)RETURNS TABLE AS RETURN (
	SELECT
		r.Ciudad,
		h.NombreHotel AS Hotel,
		@Year AS Año,
		MONTH(r.CheckIn) AS Mes,
		COUNT(hb_oc.Codigo) * 100 / COUNT(hb.Codigo) AS [Porcentaje de Ocupacion],
		ISNULL(SUM(hb_oc.Hospedaje), 0) AS [Cantidad de Personas Hospedadas]
	FROM Reservacion r
	JOIN Hotel h ON
		r.Hotel = h.CodHotel
	JOIN ReservacionHabitaciones rh ON
		rh.Reservacion = r.CodReservacion
	JOIN Habitacion hb ON
		rh.Habitacion = hb.Codigo
	LEFT JOIN FuncHabitaciones(@Year) hb_oc ON
		rh.Habitacion = hb_oc.Codigo AND
		hb_oc.Estatus = 'Ocupado'
	JOIN TiposHabitacion th ON
		hb.TipoHabitacion = th.CodTDH
	WHERE 
		h.Pais = @Pais AND
		YEAR(r.Entrada) = @Year AND
		h.Ciudad = @Ciudad AND
		h.NombreHotel = @Hotel
	GROUP BY
		r.Ciudad,
		h.NombreHotel,
		hb_oc.Hospedaje,
		YEAR(r.CheckIn),
		MONTH(r.CheckIn),
		th.NivelHabitacion
);