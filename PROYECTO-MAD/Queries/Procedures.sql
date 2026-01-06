USE GatoHotelero;
select * from Usuario;
select*from Contrasenna

insert into Usuario (NoNomina, Nombre, ApellidoPaterno, ApellidoMaterno, CorreoElectronico, Contrasenna, TelCelular, TelCasa, FechaNacimiento, TipoUsuario) values (1023, 'Laura', 'Mart�nez', 'Perez', 'martinezperez@gmail.com', 1, 8180101836, 111111111,'2004-06-04',1);
insert into Contrasenna(FechaCreacion, Contrasenna, idUsuario) values ('08-05-2025', 'Flordecerezo02*', 1023);


GO
CREATE OR ALTER PROCEDURE Validar
(
    @email nvarchar(40) = NULL,
    @contrasenna nvarchar(20) = NULL,
	@tipousuario bit = 0
)
AS
BEGIN
    SET NOCOUNT ON
    SELECT 
		u.NoNomina, 
		u.Nombre, 
		u.ApellidoPaterno, 
		u.ApellidoMaterno, 
		u.CorreoElectronico, 
		u.Contrasenna, 
		u.TelCelular, 
		u.TelCasa, 
		u.FechaNacimiento, 
		u.TipoUsuario,
		u.Estado,
		c.Contrasenna AS ContrasennaReal
	FROM Usuario u
	JOIN Contrasenna c ON c.idUsuario = u.NoNomina
	WHERE u.CorreoElectronico = @email AND c.Contrasenna = @contrasenna AND u.Contrasenna = c.idContrasenna AND u.TipoUsuario = @tipousuario AND u.Estado = 1;
END
GO

CREATE OR ALTER PROCEDURE Registrar
(
	@NoNomina INT,
	@Nombre NVARCHAR (50),
	@ApellidoPaterno NVARCHAR (50),
	@ApellidoMaterno NVARCHAR (50),
	@CorreoElectronico NVARCHAR (40),
	@Contrasenna NVARCHAR (20),
	@TelCelular NVARCHAR (10),
	@TelCasa NVARCHAR (10) ,
	@FechaNacimiento DATE ,
	@TipoUsuario NVARCHAR(15),
	@EsAdmin BIT
)
AS
BEGIN
	INSERT INTO Usuario(
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
	) VALUES (
		@NoNomina, 
		@Nombre, 
		@ApellidoPaterno, 
		@ApellidoMaterno, 
		@CorreoElectronico, 
		1, 
		@TelCelular, 
		@TelCasa, 
		@FechaNacimiento, 
		@TipoUsuario,
		@TipoUsuario
	);

	INSERT INTO Contrasenna (
		Contrasenna,
		idUsuario
	) VALUES (
		@Contrasenna,
		@NoNomina
	);

	UPDATE Usuario SET Contrasenna = (SELECT TOP 1 idContrasenna FROM Contrasenna WHERE idUsuario = @NoNomina ORDER BY FechaCreacion DESC) WHERE NoNomina = @NoNomina;
	
	IF @EsAdmin = 1 BEGIN
		INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Usuario [Administrador]', 'Administrador ha Registrado un Usuario', @NoNomina);
	END ELSE BEGIN
		INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Usuario [Usuario]', 'Un Usuario se ha Registrado', @NoNomina);
	END
END
GO

CREATE OR ALTER PROCEDURE Editar
(
	@NoNomina INT,
	@Nombre NVARCHAR (50),
	@ApellidoPaterno NVARCHAR (50),
	@ApellidoMaterno NVARCHAR (50),
	@CorreoElectronico NVARCHAR (40),
	@Contrasenna NVARCHAR (20),
	@TelCelular NVARCHAR (10),
	@TelCasa NVARCHAR (10) ,
	@FechaNacimiento DATE ,
	@TipoUsuario NVARCHAR(15),
	@Estado BiT
)
AS
BEGIN
	DECLARE @ContrasIguales AS INT;
	DECLARE @TotalContras AS INT;

	SELECT @ContrasIguales = Count(c.Contrasenna) FROM Contrasenna c WHERE c.Contrasenna = @Contrasenna AND c.idUsuario = @NoNomina;
	IF @ContrasIguales <= 0 BEGIN
		INSERT INTO Contrasenna (idUsuario, Contrasenna) VALUES (@NoNomina, @Contrasenna);

		SELECT @TotalContras = Count(c.Contrasenna) FROM Contrasenna c WHERE c.idUsuario = @NoNomina;
		IF @TotalContras > 2 BEGIN
			DELETE FROM Contrasenna WHERE idUsuario = @NoNomina AND idContrasenna = (SELECT TOP 1 idContrasenna FROM Contrasenna WHERE idUsuario = @NoNomina ORDER BY idContrasenna ASC);
		END
	END
	

	UPDATE Usuario SET
		Nombre = @Nombre, 
		ApellidoPaterno = @ApellidoPaterno, 
		ApellidoMaterno = @ApellidoMaterno, 
		CorreoElectronico = @CorreoElectronico,
		Contrasenna = (SELECT TOP 1 idContrasenna FROM Contrasenna WHERE idUsuario = @NoNomina ORDER BY idContrasenna DESC),
		TelCelular = @TelCelular, 
		TelCasa = @TelCasa, 
		FechaNacimiento = @FechaNacimiento, 
		TipoUsuario = @TipoUsuario,
		Estado = @Estado
	WHERE NoNomina = @NoNomina;
	
	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Usuario [Administrador]', 'Administrador ha Editado un Usuario', @NoNomina);
END
GO

CREATE OR ALTER PROCEDURE RegistrarCliente
(
	@RFC NVARCHAR (15),
	@Nombre NVARCHAR (50),
	@ApellidoPaterno NVARCHAR (50),
	@ApellidoMaterno NVARCHAR (50),
	@Ciudad NVARCHAR (30),
	@Estado NVARCHAR (30),
	@Pais NVARCHAR (30),
	@CorreoElectronico NVARCHAR (40),
	@TelCelular NVARCHAR (10),
	@TelCasa NVARCHAR (10),
	@FechaNacimiento DATE,
	@EstadoCivil NVARCHAR (10),
	@NoNomina INT
)
AS
BEGIN
	INSERT INTO Cliente(
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
	) VALUES (
		@RFC, 
		@Nombre, 
		@ApellidoPaterno, 
		@ApellidoMaterno, 
		@Ciudad, 
		@Estado, 
		@Pais, 
		@CorreoElectronico, 
		@TelCelular, 
		@TelCasa, 
		@FechaNacimiento, 
		@EstadoCivil
	);
	
	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Cliente [Administrador]', 'Administrador ha Registrado un Cliente', @NoNomina);
END
GO

CREATE OR ALTER PROCEDURE EditarCliente
(
	@RFC NVARCHAR (15),
	@Nombre NVARCHAR (50),
	@ApellidoPaterno NVARCHAR (50),
	@ApellidoMaterno NVARCHAR (50),
	@Ciudad NVARCHAR (30),
	@Estado NVARCHAR (30),
	@Pais NVARCHAR (30),
	@CorreoElectronico NVARCHAR (40),
	@TelCelular NVARCHAR (10),
	@TelCasa NVARCHAR (10),
	@FechaNacimiento DATE,
	@EstadoCivil NVARCHAR (10),
	@NoNomina INT
)
AS
BEGIN
	UPDATE Cliente SET
		Nombre = @Nombre, 
		ApellidoPaterno = @ApellidoPaterno, 
		ApellidoMaterno = @ApellidoMaterno, 
		Ciudad = @Ciudad, 
		Estado = @Estado, 
		Pais = @Pais, 
		CorreoElectronico = @CorreoElectronico, 
		TelCelular = @TelCelular, 
		TelCasa = @TelCasa, 
		FechaNacimiento = @FechaNacimiento, 
		EstadoCivil = @EstadoCivil
	WHERE RFC = @RFC;
	
	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Cliente [Administrador]', 'Administrador ha Editado un Cliente', @NoNomina);
END
GO

insert into Contrasenna(FechaCreacion, Contrasenna, idUsuario) values ('07-05-2025', 'Flordecerezo01*', 1023);
GO;

CREATE OR ALTER VIEW ViewUsuarios
AS
SELECT 
	NoNomina, 
	Nombre, 
	ApellidoPaterno, 
	ApellidoMaterno, 
	CorreoElectronico, 
	u.Contrasenna, 
	TelCelular, 
	TelCasa, 
	FechaNacimiento, 
	TipoUsuario,
	Estado,
	c.Contrasenna AS ContrasennaReal
FROM Usuario u JOIN Contrasenna c ON c.idUsuario = NoNomina AND c.idContrasenna = u.Contrasenna;
GO;

CREATE OR ALTER FUNCTION FGetUsuarios ()
RETURNS TABLE
AS
RETURN
(
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
		Estado,
		ContrasennaReal
	FROM ViewUsuarios
);
GO


GO

EXEC GetUsuarios;
GO

CREATE OR ALTER VIEW ViewClientes
AS
SELECT 
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

CREATE OR ALTER  FGetClientes ()
RETURNS TABLE
AS
RETURN
(
    SELECT 
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
	FROM Cliente
);
GO

CREATE OR ALTER PROCEDURE GetClientes
AS
BEGIN
    SELECT 
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
	FROM FGetClientes();
END
GO

CREATE OR ALTER PROCEDURE GetClientesAp
(
	@ApPat NVARCHAR(50),
	@ApMat NVARCHAR(50)
)
AS
BEGIN
    SELECT 
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
	FROM Cliente WHERE ApellidoPaterno = @ApPat AND ApellidoMaterno = @ApMat;
END
GO

CREATE OR ALTER PROCEDURE GetVentas
(
	@Pais NVARCHAR(100),
	@Year INT,
	@Ciudad NVARCHAR(100),
	@Hotel NVARCHAR(100)
) 
AS
BEGIN
    SELECT 
		r.Ciudad,
		h.NombreHotel,
		YEAR(r.CheckIn) AS Anio,
		MONTH(r.CheckIn) AS Mes,
		f.PrecioInicial,
		f.PrecioServicios,
		f.PrecioTotal
	FROM Factura f JOIN Reservacion r on f.Reservacion = r.CodReservacion JOIN Hotel h ON r.Hotel = h.CodHotel
	WHERE 
		h.Pais = @Pais AND
		YEAR(r.Entrada) = @Year AND
		r.Ciudad = @Ciudad AND
		h.NombreHotel = @Hotel
	GROUP BY
		r.Ciudad,
		h.NombreHotel,
		YEAR(r.CheckIn),
		MONTH(r.CheckIn),
		f.PrecioInicial,
		f.PrecioServicios,
		f.PrecioTotal;
END
GO

EXEC GetVentas @Pais = 'Wakanda', @Year = 2025, @Ciudad = 'Monterrey', @Hotel = 'Pepillotel';

CREATE OR ALTER PROCEDURE GetOcupaciones
(
	@Pais NVARCHAR(100),
	@Year INT,
	@Ciudad NVARCHAR(100),
	@Hotel NVARCHAR(100)
) 
AS
BEGIN	
    SELECT 
		r.Ciudad,
		h.NombreHotel,
		YEAR(r.Entrada) AS Anio,
		MONTH(r.Entrada) AS Mes,
		th.NivelHabitacion,
		COUNT(hb.NoHabitacion) AS Habitaciones,
		COUNT(hba.NoHabitacion) / COUNT(hb.NoHabitacion) * 100 AS Porcentaje,
		r.CantPersonas AS Personas
	FROM Reservacion r 
		JOIN Hotel h ON r.Hotel = h.CodHotel 
		JOIN TiposHabitacion th ON r.TipoHabitacion = th.CodTDH 
		JOIN Habitacion hb ON hb.TipoHabitacion = th.CodTDH
		LEFT JOIN Habitacion hba ON hba.TipoHabitacion = th.CodTDH AND hba.Estatus != 'Desocupado'
	WHERE 
		h.Pais = @Pais AND
		YEAR(r.Entrada) = @Year AND
		h.Ciudad = @Ciudad AND
		h.NombreHotel = @Hotel
	GROUP BY
		r.Ciudad,
		h.NombreHotel,
		r.CantPersonas,
		YEAR(r.Entrada),
		MONTH(r.Entrada),
		th.NivelHabitacion;
END
GO

EXEC GetOcupaciones @Pais = 'Wakanda', @Year = 2025, @Ciudad = 'Monterrey', @Hotel = 'Pepillotel';
SELECT * FROM Hotel;
SELECT * FROM Reservacion;

CREATE OR ALTER PROCEDURE GetOcupaciones2
(
	@Pais NVARCHAR(100),
	@Year INT,
	@Ciudad NVARCHAR(100),
	@Hotel NVARCHAR(100)
) 
AS
BEGIN	
    SELECT 
		r.Ciudad AS Ciudad,
		h.NombreHotel AS NombreHotel,
		YEAR(r.Entrada) AS Anio,
		MONTH(r.Entrada) AS Mes,
		COUNT(hba.NoHabitacion) / COUNT(hb.NoHabitacion) * 100 AS Porcentaje
	FROM Reservacion r 
		JOIN Hotel h ON r.Hotel = h.CodHotel 
		JOIN TiposHabitacion th ON h.CodHotel = th.idHotel 
		JOIN Habitacion hb ON hb.TipoHabitacion = th.CodTDH
		LEFT JOIN Habitacion hba ON hba.TipoHabitacion = th.CodTDH AND hba.Estatus != 'Desocupado'
	WHERE 
		h.Pais = @Pais AND
		YEAR(r.Entrada) = @Year AND
		r.Ciudad = @Ciudad AND
		h.NombreHotel = @Hotel
	GROUP BY
		r.Ciudad,
		h.NombreHotel,
		r.CantPersonas,
		YEAR(r.Entrada),
		MONTH(r.Entrada),
		th.NivelHabitacion;
END
GO

SELECT * FROM Reservacion;

CREATE OR ALTER PROCEDURE GetCliente
( @RFC NVARCHAR(15) )
AS
BEGIN
    SELECT 
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
	FROM Cliente WHERE RFC = @RFC;
END
GO

CREATE OR ALTER PROCEDURE GetHoteles
AS
BEGIN
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
		STUFF(
			(
				SELECT ',' + Nombre
				FROM Servicio s 
				WHERE s.Hotel = CodHotel
				FOR XML PATH(''), 
				TYPE
			).value('.', 'VARCHAR(1000)'), 
			1, 
			1, 
			''
		) AS Servicios
	FROM Hotel;
END
GO

CREATE OR ALTER PROCEDURE RegistrarHoteles 
(
    @NombreHotel NVARCHAR (100), 
    @Ciudad NVARCHAR (30), 
    @Estado NVARCHAR (30), 
    @Pais NVARCHAR (30), 
    @ZonaTuristica BIT, 
    @Locacion VARCHAR (60),
    @NoPisos INT,
	@FechaInicio Date,
	@Servicios VARCHAR(1000),
    @NoNomina INT
)
AS
BEGIN
    INSERT INTO Hotel(
    NombreHotel,
    Ciudad,
    Estado,
    Pais,
    ZonaTuristica,
    Locacion,
    NoPisos,
    FechaInicio
    )
    VALUES
    (
    @NombreHotel, 
    @Ciudad,
    @Estado, 
    @Pais, 
    @ZonaTuristica, 
    @Locacion,
    @NoPisos,
    @FechaInicio
    );
	
    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Hotel', 'Administrador ha Registrado un Hotel', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE EditarHotel
(
	@NombreHotel NVARCHAR (100), 
    @Ciudad NVARCHAR (30), 
    @Estado NVARCHAR (30), 
    @Pais NVARCHAR (30), 
    @ZonaTuristica BIT, 
    @Locacion VARCHAR (60),
    @NoPisos INT,
	@FechaInicio Date,
	@Servicios VARCHAR(1000),
    @CodHotel INT,
    @NoNomina INT
)
AS
BEGIN
	UPDATE Hotel SET
		NombreHotel = @NombreHotel,
		Ciudad = @Ciudad,
		Estado = @Estado,
		Pais = @Pais,
		ZonaTuristica = @ZonaTuristica,
		Locacion = @Locacion,
		NoPisos = @NoPisos,
		FechaInicio = @FechaInicio
	WHERE CodHotel = @CodHotel;

	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Hotel [Administrador]', 'Administrador ha Editado un Hotel', @NoNomina);
END
GO

CREATE OR ALTER PROCEDURE GetTiposHabitaciones
AS
BEGIN
    SELECT 
		CodTDH,
		NivelHabitacion,
		NoCamas,
		TipoCama,
		PrecioNoche,
		CantPersonasMax,
		t.Locacion,
		Amenidades,
		idHotel,
		NombreHotel
	FROM TiposHabitacion t JOIN Hotel h on CodHotel = idHotel;
END
GO

CREATE OR ALTER PROCEDURE RegistrarTipoHabitacion
(
	@NivelHabitacion NVARCHAR(20),
	@NoCamas INT,
	@TipoCama NVARCHAR (300),
	@PrecioNoche MONEY,
	@CantPersonasMax INT,
	@Locacion NVARCHAR (60),
	@Amenidades NVARCHAR (100),
	@NombreHotel NVARCHAR (100),
    @NoNomina INT
)
AS
BEGIN
    INSERT INTO TiposHabitacion(
		NivelHabitacion,
		NoCamas,
		TipoCama,
		PrecioNoche,
		CantPersonasMax,
		Locacion,
		Amenidades,
		idHotel
    )
    VALUES
    (
		@NivelHabitacion,
		@NoCamas,
		@TipoCama,
		@PrecioNoche,
		@CantPersonasMax,
		@Locacion,
		@Amenidades,
		(SELECT CodHotel FROM Hotel WHERE NombreHotel = @NombreHotel)
    );

    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Tipo de Habitacion', 'Administrador ha Registrado un Tipo de Habitacion', @NoNomina);
END
GO;

select*from TiposHabitacion
DELETE FROM TiposHabitacion WHERE NivelHabitacion = 'Master' AND CodTDH= 2;

go

CREATE OR ALTER PROCEDURE EditarTipoHabitacion
(
	@NivelHabitacion NVARCHAR(20),
	@NoCamas INT,
	@TipoCama NVARCHAR (300),
	@PrecioNoche MONEY,
	@CantPersonasMax INT,
	@Locacion NVARCHAR (60),
	@Amenidades NVARCHAR (100),
	@NombreHotel NVARCHAR (100),
	@CodTDH INT,
    @NoNomina INT
)
AS
BEGIN
	UPDATE TiposHabitacion SET
		NivelHabitacion = @NivelHabitacion,
		NoCamas = @NoCamas,
		TipoCama = @TipoCama,
		PrecioNoche = @PrecioNoche,
		CantPersonasMax = @CantPersonasMax,
		Locacion = @Locacion,
		Amenidades = @Amenidades
	WHERE CodTDH = @CodTDH;
	
	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Tipo de Habitacion [Administrador]', 'Administrador ha Editado un Tipo de Habitacion', @NoNomina);
END
GO

CREATE OR ALTER PROCEDURE BuscarCliente
(
	@CorreoElectronico NVARCHAR (40)
)
AS
BEGIN
	SELECT
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
	FROM Cliente

	WHERE CorreoElectronico= @CorreoElectronico;
END
GO

CREATE OR ALTER PROCEDURE BuscarClienteRFC
(
	@RFC NVARCHAR (15)
)
AS
BEGIN
	SELECT
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
	FROM Cliente
	WHERE RFC = @RFC;
END

GO

CREATE OR ALTER PROCEDURE BuscarClienteAp
(
	@ApellidoPaterno NVARCHAR (50),
	@ApellidoMaterno NVARCHAR (50)

)
AS
BEGIN
	SELECT
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
	FROM Cliente

	WHERE ApellidoPaterno = @ApellidoPaterno AND ApellidoMaterno = @ApellidoMaterno;
END

CREATE OR ALTER PROCEDURE RegistrarHabitacion
(
	@NoHabitacion INT,
	@Estatus VARCHAR (50),
	@Piso INT,
	@TipoHabitacion VARCHAR(20),
    @NoNomina INT
)
AS
BEGIN
    INSERT INTO Habitacion(
		NoHabitacion,
		Estatus,
		Piso,
		TipoHabitacion
    )
    VALUES
    (
		@NoHabitacion,
		@Estatus,
		@Piso,
		(SELECT CodTDH FROM TiposHabitacion WHERE NivelHabitacion = @TipoHabitacion)
    );

    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Habitacion', 'Administrador ha Registrado una Habitacion', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE EditarHabitacion
(
	@NoHabitacion INT,
	@Estatus VARCHAR (50),
	@Piso INT,
	@TipoHabitacion VARCHAR(20),
    @NoNomina INT
)
AS
BEGIN
    Update Habitacion SET
	Estatus = @Estatus,
	Piso = @Piso,
	TipoHabitacion = (SELECT CodTDH FROM TiposHabitacion WHERE NivelHabitacion = @TipoHabitacion)
	WHERE NoHabitacion = @NoHabitacion;

    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Habitacion', 'Administrador ha Edicion una Habitacion', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE RegistrarServicio
(
	@CodServicio INT,
	@Nombre VARCHAR (50),
	@Descripcion VARCHAR(200),
	@Precio MONEY,
	@Hotel INT,
    @NoNomina INT
)
AS
BEGIN
    INSERT INTO Servicio(
		Nombre,
		Descripcion,
		Precio,
		Hotel
    )
    VALUES
    (
		@Nombre,
		@Descripcion,
		@Precio,
		@Hotel
    );

    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Servicio', 'Administrador ha Registrado un Servicio', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE EditarServicio
(
	@CodServicio INT,
	@Nombre VARCHAR (50),
	@Descripcion VARCHAR(200),
	@Precio MONEY,
    @NoNomina INT
)
AS
BEGIN
    UPDATE Servicio SET
	Nombre = @Nombre,
	Descripcion = @Descripcion,
	Precio = @Precio
	WHERE CodServicio = @CodServicio;
	
    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Servicio', 'Administrador ha Editado un Servicio', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE RegistrarReservacion
(
	@Cliente NVARCHAR (15),
	@Ciudad NVARCHAR (30),
	@Hotel NVARCHAR (100),
	@TipoHabitacion NVARCHAR(20),
	@CantHabitaciones INT,
	@CantPersonas INT,
	@Entrada DATE,
	@Salida DATE,
	@Estatus NVARCHAR(11),
	@Anticipo Money,
	@NoNomina INT
)
AS
BEGIN
    INSERT INTO Reservacion(
		Cliente,
		Ciudad,
		Hotel,
		TipoHabitacion,
		CantHabitaciones,
		CantPersonas,
		Entrada,
		Salida,
		Estatus,
		Anticipo
    )
    VALUES
    (
		@Cliente,
		@Ciudad,
		(SELECT CodHotel FROM Hotel WHERE NombreHotel = @Hotel),
		(SELECT CodTDH FROM TiposHabitacion WHERE NivelHabitacion = @TipoHabitacion),
		@CantHabitaciones,
		@CantPersonas,
		@Entrada,
		@Salida,
		@Estatus,
		@Anticipo
    );

    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Reservacion', 'Usuario ha creado una Reservacion', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE CancelarReservacion
(
	@Codigo UNIQUEIDENTIFIER,
	@NoNomina INT
)
AS
BEGIN
	Update Habitacion SET
		Estatus = 'Desocupado',
		Reservacion = NULL
	WHERE Reservacion = @Codigo;

	Update Reservacion SET
		Estatus = 'Cancelado'
	WHERE CodReservacion = @Codigo;
	
    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Reservacion Cancelada', 'Usuario ha Cancelado una Reservacion', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE GetCiudades
AS
BEGIN
	SELECT DISTINCT Ciudad FROM Hotel;
END
GO;

CREATE OR ALTER VIEW ViewHotel
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
	FechaInicio
FROM Hotel;

CREATE OR ALTER PROCEDURE GetHotelesCiudad
(
	@Ciudad NVarchar (30)
)
AS
BEGIN
	SELECT 
		CodHotel,
		NombreHotel,
		Ciudad,
		Estado,
		Pais,
		ZonaTuristica,
		Locacion,
		NoPisos,
		FechaInicio
	FROM ViewHotel WHERE Ciudad = @Ciudad;
END
GO;

CREATE OR ALTER PROCEDURE GetHotelNombre
(
	@Nombre NVarchar (100)
)
AS
BEGIN
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
		STUFF(
			(
				SELECT ', - ' + Nombre + ': ' + CONVERT(VARCHAR(50), Precio) + '$'
				FROM Servicio s WHERE s.Hotel = CodHotel
				FOR XML PATH(''), 
				TYPE
			).value('.', 'VARCHAR(1000)'), 
			1, 
			1, 
			''
		) AS Servicios
	FROM Hotel WHERE NombreHotel = @Nombre;
END
GO;
CREATE OR ALTER PROCEDURE GetTiposHabitacionHotel
(
	@Hotel NVarchar (100)
)
AS
BEGIN
	SELECT 
		CodTDH,
		NivelHabitacion,
		NoCamas,
		TipoCama,
		PrecioNoche,
		CantPersonasMax,
		Amenidades,
		idHotel,
		(Select Count(NoHabitacion) FROM Habitacion WHERE TipoHabitacion = CodTDH AND Estatus = 'Desocupado') AS Habitaciones
	FROM Hotel JOIN TiposHabitacion on idHotel = CodHotel WHERE NombreHotel = @Hotel;
END
GO;

CREATE OR ALTER VIEW ViewHabitaciones
AS
SELECT 
	NoHabitacion,
	Estatus,
	Piso,
	TipoHabitacion,
	NivelHabitacion,
	NombreHotel
FROM Habitacion JOIN TiposHabitacion ON CodTDH = TipoHabitacion JOIN Hotel ON CodHotel = idHotel;

CREATE OR ALTER PROCEDURE GetHabitaciones
AS
BEGIN
	SELECT 
	NoHabitacion,
	Estatus,
	Piso,
	TipoHabitacion,
	NivelHabitacion,
	NombreHotel
	FROM ViewHabitaciones;
END
GO;

CREATE OR ALTER PROCEDURE GetReservaciones
( @RFC NVARCHAR(15) )
AS
BEGIN
	SELECT DISTINCT
		CodReservacion,
		r.Cliente,
		r.Ciudad,
		r.Hotel,
		CantHabitaciones,
		CantPersonas,
		r.Entrada,
		r.Salida,
		r.Estatus,
		r.CheckIn,
		r.CheckOut,
		NombreHotel,
		th.NivelHabitacion,
		hb.NoHabitacion,
		f.Anticipo,
		f.PrecioInicial,
		PrecioTotal,
		f.PrecioServicios
	FROM Reservacion r JOIN Hotel h ON CodHotel = Hotel JOIN Habitacion hb ON hb.Reservacion = CodReservacion JOIN TiposHabitacion th ON hb.TipoHabitacion = th.CodTDH JOIN Factura f ON f.Reservacion = r.CodReservacion WHERE r.Cliente = @RFC;
END
GO;

EXEC GetReservaciones @RFC =199 ;

CREATE OR ALTER PROCEDURE GetReservacion
( @Codigo UNIQUEIDENTIFIER )
AS
BEGIN
	SELECT DISTINCT
		CodReservacion,
		r.Cliente,
		r.Ciudad,
		r.Hotel,
		CantHabitaciones,
		CantPersonas,
		r.Entrada,
		r.Salida,
		r.Estatus,
		CheckIn,
		CheckOut,
		NombreHotel,
		th.NivelHabitacion,
		f.Anticipo,
		f.PrecioInicial,
		f.PrecioServicios,
		f.PrecioTotal,
		hb.NoHabitacion,
		(ABS(DATEDIFF(DAY, r.Entrada, r.Salida))) AS Dias,
		th.PrecioNoche
	FROM Reservacion r JOIN Factura f ON f.Reservacion = r.CodReservacion JOIN Hotel h ON CodHotel = r.Hotel JOIN Habitacion hb ON hb.Reservacion = r.CodReservacion JOIN TiposHabitacion th ON hb.TipoHabitacion = th.CodTDH WHERE r.CodReservacion = @Codigo;
END
GO;

CREATE OR ALTER PROCEDURE CheckIn
( 
	@Reservacion UNIQUEIDENTIFIER,
	@Fecha Date,
	@NoNomina INT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Reservacion WHERE CheckIn IS NOT NULL AND CodReservacion = @Reservacion) BEGIN RETURN; END
	
	Update Reservacion SET
		CheckIn = @Fecha
	WHERE CodReservacion = @Reservacion;

	Update Habitacion SET
		Estatus = 'Ocupado'
	WHERE Reservacion = @Reservacion;
		
    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Check In Realizado', 'Usuario ha realizado un Check In de Reservacion', @NoNomina);

	SELECT NoHabitacion FROM Habitacion WHERE Reservacion = @Reservacion;
END
GO;

CREATE OR ALTER PROCEDURE CheckOut
( 
	@Reservacion UNIQUEIDENTIFIER,
	@Fecha Date,
	@Descuento Money,
	@NombreDescuento NVarchar(50),
	@NoNomina INT
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Reservacion WHERE CheckOUT IS NOT NULL AND CodReservacion = @Reservacion) BEGIN RETURN; END
	
	UPDATE Factura SET
		Descuento = @Descuento,
		NombreDescuento = @NombreDescuento
	WHERE Reservacion = @Reservacion;
	
	Update Habitacion SET
		Estatus = 'Desocupado'
	WHERE Reservacion = @Reservacion;
	
	Update Reservacion SET
		CheckOut = @Fecha,
		Estatus = 'Concluido'
	WHERE CodReservacion = @Reservacion;
		
    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Check Out Realizado', 'Usuario ha realizado un Check Out de Reservacion', @NoNomina);
	
	UPDATE f SET f.PrecioTotal = ((ISNULL(f.PrecioInicial, 0) + ISNULL(f.PrecioServicios, 0)) * (1 - (ISNULL(f.Descuento, 0) / 100))) - f.Anticipo FROM Factura f WHERE f.Reservacion = @Reservacion;

	SELECT NoHabitacion FROM Habitacion WHERE Reservacion = @Reservacion;
END
GO;

CREATE OR ALTER PROCEDURE FacturarServicios
( 
	@Reservacion UNIQUEIDENTIFIER,
	@Lista Varchar(1000)
)
AS
BEGIN
	INSERT INTO ServiciosAdicionales (Reservacion, Servicio)
    SELECT 
		@Reservacion AS Reservacion,
		(SELECT s.CodServicio FROM Servicio s WHERE s.Nombre = LTRIM(RTRIM(value))) AS Servicio
    FROM STRING_SPLIT(@Lista, ',');
END
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

CREATE OR ALTER FUNCTION FGetServicios()
RETURNS TABLE
AS
RETURN
(
    SELECT 
		Nombre,
		Descripcion,
		Precio,
		CodServicio,
		Hotel
	FROM Servicio
);
GO

CREATE OR ALTER PROCEDURE GetServicios
AS
BEGIN
	SELECT 
		Nombre,
		Descripcion,
		Precio,
		CodServicio,
		Hotel
	FROM FGetServicios();
END
GO;

CREATE OR ALTER PROCEDURE GetServiciosHotel
( @Hotel Int )
AS
BEGIN
	SELECT 
		Nombre,
		Descripcion,
		Precio,
		CodServicio,
		Hotel
	FROM ViewServicios WHERE Hotel = @Hotel;
END
GO;

CREATE OR ALTER PROCEDURE GetFacturaServicios
( @Codigo UNIQUEIDENTIFIER )
AS
BEGIN
	SELECT 
		s.Nombre,
		s.Descripcion,
		s.Precio,
		s.CodServicio,
		s.Hotel
	FROM ServiciosAdicionales sa JOIN Servicio s ON s.CodServicio = sa.Servicio WHERE sa.Reservacion = @Codigo;
END
GO;

CREATE OR ALTER TRIGGER CrearFactura
ON Reservacion
AFTER INSERT
AS
BEGIN
	UPDATE Habitacion SET
		Estatus = 'Reservado',
		Reservacion = (SELECT i.CodReservacion FROM inserted i)
	WHERE NoHabitacion IN (
		SELECT TOP (SELECT i.CantHabitaciones FROM inserted i) NoHabitacion FROM Habitacion WHERE Estatus = 'Desocupado' AND TipoHabitacion = (SELECT i.TipoHabitacion FROM inserted i) ORDER BY NoHabitacion 
	);

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
		i.CantHabitaciones * ABS(DATEDIFF(DAY, i.Entrada, i.Salida)) * th.PrecioNoche AS PrecioInicial,
		0 AS PrecioServicios,
		(i.CantHabitaciones * ABS(DATEDIFF(DAY, i.Entrada, i.Salida)) * th.PrecioNoche - i.Anticipo) AS PrecioTotal,
		'NA' AS NombreDescuento,
		0 AS Descuento,
		i.Anticipo AS Anticipo,
		'Desconocido' AS FormaPago,
		i.Cliente,
		i.Hotel,
		i.CodReservacion AS Reservacion
	FROM inserted i
	JOIN Habitacion hb ON hb.Reservacion = i.CodReservacion
	JOIN TiposHabitacion th ON hb.TipoHabitacion = th.CodTDH;
END
GO;


CREATE OR ALTER TRIGGER CalcularFactura
ON ServiciosAdicionales
AFTER INSERT
AS
BEGIN
	UPDATE f SET f.PrecioServicios = ISNULL(f.PrecioServicios, 0) + ISNULL(s.Precio, 0) FROM Factura f JOIN inserted i ON f.Reservacion = i.Reservacion JOIN Servicio s ON s.CodServicio = i.Servicio WHERE f.Reservacion = i.Reservacion;
	UPDATE f SET f.PrecioTotal = ((ISNULL(f.PrecioInicial, 0) + ISNULL(f.PrecioServicios, 0) - f.Anticipo) * (1 - (ISNULL(f.Descuento, 0) / 100))) FROM Factura f JOIN inserted i ON f.Reservacion = i.Reservacion WHERE f.Reservacion = i.Reservacion;
END
GO;

SELECT * FROM Cliente;

CREATE OR ALTER VIEW ViewFactura
AS
SELECT 
		NoFactura,
		PrecioTotal,
		NombreDescuento,
		Descuento,
		FechaCreacion,
		FormaPago,
		PrecioInicial,
		PrecioServicios,
		Anticipo,
		Reservacion
FROM Factura;

CREATE OR ALTER PROCEDURE GetFactura
( 
	@Reservacion UNIQUEIDENTIFIER
)
AS
BEGIN
	SELECT 
		NoFactura,
		PrecioTotal,
		NombreDescuento,
		Descuento,
		FechaCreacion,
		FormaPago,
		PrecioInicial,
		PrecioServicios,
		Anticipo
	FROM ViewFactura WHERE Reservacion = @Reservacion
END
GO;

SELECT * FROM Hotel;
SELECT * FROM Cliente;
SELECT * FROM Usuario;
SELECT * FROM Factura;
SELECT * FROM Servicio;
SELECT * FROM Habitacion;
SELECT * FROM Contrasenna;
SELECT * FROM Reservacion;
SELECT * FROM HotelesServicio;
SELECT * FROM TiposHabitacion;
SELECT * FROM ServiciosAdicionales;
SELECT * FROM Cliente;

TRUNCATE TABLE Checks;
TRUNCATE TABLE Factura;
TRUNCATE TABLE Servicio;
TRUNCATE TABLE Reservacion;
TRUNCATE TABLE ServiciosAdicionales;
DELETE FROM Reservacion WHERE Hotel > 0;
DELETE FROM Factura WHERE NoFactura = 1;
DELETE FROM Checks WHERE idCheck = 8;
DELETE FROM Contrasenna WHERE idContrasenna = 9;

Update Usuario SET Contrasenna = 8 WHERE NoNomina = 1995031;

UPDATE Habitacion SET Estatus = 'Desocupado', Reservacion = NULL;

EXEC BuscarClienteRFC @RFC = '199';

UPDATE Usuario SET Estado = 1;