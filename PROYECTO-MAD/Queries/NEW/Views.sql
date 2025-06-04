USE GatoHotelero2;
GO;

CREATE OR ALTER VIEW ViewUsuarios AS
SELECT 
	NoNomina, 
	Nombre, 
	ApellidoPaterno, 
	ApellidoMaterno, 
	CorreoElectronico, 
	Contrasenna, 
	TelCelular, 
	TelCasa, 
	FechaNacimiento, 
	TipoUsuario,
	Estado
FROM Usuario;
GO;

CREATE OR ALTER VIEW ViewClientes
AS SELECT 
	RFC, 
	Nombre, 
	ApellidoPaterno, 
	ApellidoMaterno, 
	Ciudad, 
	Estado, 
	Pais, 
	CorreoElectronico, 
	TelCelular, 
	TelCasa,
	FechaNacimiento,
	EstadoCivil
FROM Cliente;
GO;

CREATE OR ALTER VIEW ViewHoteles
AS
SELECT 
	CodHotel,
	NombreHotel,
	Ciudad,
	Estado,
	Pais,
	ZonaTuristica,
	Locacion,
	NoPisos,
	FechaInicio,
	ISNULL(STUFF(
		(
			SELECT ' - ' + s.Nombre + ': ' + CONVERT(VARCHAR(50), Precio) + '$\n'
			FROM Servicio s WHERE s.Hotel = CodHotel
			FOR XML PATH(''), 
			TYPE
		).value('.', 'VARCHAR(1000)'), 
		1, 
		1, 
		''
	), '') AS Servicios
FROM Hotel;
GO;

CREATE OR ALTER VIEW ViewServicios
AS
SELECT 
	Nombre,
	Descripcion,
	Precio,
	CodServicio,
	Hotel
FROM Servicio;
GO;

CREATE OR ALTER VIEW ViewTipoHabitaciones AS
SELECT 
	CodTDH,
	NivelHabitacion,
	NoCamas,
	TipoCama,
	PrecioNoche,
	CantPersonasMax,
	t.Locacion,
	t.Amenidades,
	idHotel,
	NombreHotel
FROM TiposHabitacion t JOIN Hotel h on CodHotel = idHotel;
GO;

CREATE OR ALTER VIEW ViewHabitaciones AS
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
			SELECT 
				COUNT(rh.Id) 
			FROM ReservacionHabitaciones rh 
			JOIN Reservacion rv ON 
				rh.Reservacion = rv.CodReservacion 
			WHERE 
				rv.Estatus != 'Cancelado' AND
				rh.Habitacion = Codigo AND 
				GETDATE() > rv.CheckIn AND 
				GETDATE() < rv.CheckOut
		) >= 1 THEN 'Ocupado'
		WHEN (
			SELECT 
				COUNT(rh.Id) 
			FROM ReservacionHabitaciones rh 
			JOIN Reservacion rv ON 
				rh.Reservacion = rv.CodReservacion 
			WHERE 
				rv.Estatus != 'Cancelado' AND
				rh.Habitacion = Codigo AND 
				GETDATE() < rv.Salida
		) >= 1 THEN 'Reservado'
		ELSE 'Desocupado'
	END AS Estatus,
	(
		SELECT 
			rv.CodReservacion
		FROM ReservacionHabitaciones rh 
		JOIN Reservacion rv ON 
			rh.Reservacion = rv.CodReservacion 
		WHERE 
			rv.Estatus != 'Cancelado' AND
			rh.Habitacion = Codigo AND 
			GETDATE() < rv.Salida
	) AS Reservacion,
	(
		SELECT TOP 1
			rh.Personas
		FROM ReservacionHabitaciones rh 
		JOIN Reservacion rv ON 
			rh.Reservacion = rv.CodReservacion 
		WHERE 
			rv.Estatus != 'Cancelado' AND
			rh.Habitacion = Codigo AND 
			GETDATE() < rv.Salida
	) AS Hospedaje
FROM Habitacion JOIN TiposHabitacion ON CodTDH = TipoHabitacion JOIN Hotel ON CodHotel = idHotel;
GO;