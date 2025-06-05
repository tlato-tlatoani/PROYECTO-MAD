USE GatoHotelero2;

GO;

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
		@Contrasenna, 
		@TelCelular, 
		@TelCasa, 
		@FechaNacimiento, 
		@TipoUsuario,
		@TipoUsuario
	);

	IF @EsAdmin = 1 BEGIN
		INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Usuario [Administrador]', 'Administrador ha Registrado un Usuario', @NoNomina);
	END ELSE BEGIN
		INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Registro de Usuario [Usuario]', 'Un Usuario se ha Registrado', @NoNomina);
	END
END
GO

CREATE OR ALTER PROCEDURE Validar
(
    @email nvarchar(40) = NULL,
    @contrasenna nvarchar(20) = NULL,
	@tipousuario bit = 0
)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Usuario WHERE CorreoElectronico = @email AND Contrasenna = @contrasenna AND TipoUsuario = @tipousuario) BEGIN
		THROW 50001, 'No se Encontro un Usuario con tus Datos...', 1;
	END
	IF EXISTS (SELECT 1 FROM Usuario WHERE CorreoElectronico = @email AND Contrasenna = @contrasenna AND TipoUsuario = @tipousuario AND Estado = 0) BEGIN
		THROW 50001, 'Tu Usuario todavia no a Sido Activado...', 1;
	END

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
		u.Estado
	FROM Usuario u
	WHERE u.CorreoElectronico = @email AND u.Contrasenna = @contrasenna AND u.TipoUsuario = @tipousuario AND u.Estado = 1;
END;
GO;

CREATE OR ALTER PROCEDURE Editar
(
	@NoNomina INT,
	@Nombre NVARCHAR (50),
	@ApellidoPaterno NVARCHAR (50),
	@ApellidoMaterno NVARCHAR (50),
	@CorreoElectronico NVARCHAR (40),
	@TelCelular NVARCHAR (10),
	@TelCasa NVARCHAR (10) ,
	@FechaNacimiento DATE 
)
AS
BEGIN
	UPDATE Usuario SET
		Nombre = @Nombre, 
		ApellidoPaterno = @ApellidoPaterno, 
		ApellidoMaterno = @ApellidoMaterno, 
		CorreoElectronico = @CorreoElectronico,
		TelCelular = @TelCelular, 
		TelCasa = @TelCasa, 
		FechaNacimiento = @FechaNacimiento
	WHERE NoNomina = @NoNomina;
	
	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Usuario [Administrador]', 'Administrador ha Editado un Usuario', @NoNomina);
END;
GO;

CREATE OR ALTER PROCEDURE ActualizarContra
(
	@NoNomina INT,
	@Contrasenna NVARCHAR (20)
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM Usuario WHERE NoNomina = @NoNomina AND OldContrasenna1 = @contrasenna) BEGIN
		THROW 50001, 'Ya Utilizaste esta Contraseña...', 1;
	END
	IF EXISTS (SELECT 1 FROM Usuario WHERE NoNomina = @NoNomina AND OldContrasenna2 = @contrasenna) BEGIN
		THROW 50001, 'Ya Utilizaste esta Contraseña...', 1;
	END
	IF EXISTS (SELECT 1 FROM Usuario WHERE NoNomina = @NoNomina AND Contrasenna = @contrasenna) BEGIN
		THROW 50001, 'Ya Utilizaste esta Contraseña...', 1;
	END

	UPDATE Usuario SET
		OldContrasenna2 = OldContrasenna1,
		OldContrasenna1 = Contrasenna,
		Contrasenna = @Contrasenna
	WHERE NoNomina = @NoNomina;
	
	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Contraseña Actualizada [Operativo]', 'Administrador ha Editado un Usuario', @NoNomina);
END;
GO;

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

CREATE OR ALTER PROCEDURE RegistrarHotel
(
    @NombreHotel NVARCHAR (100), 
    @Ciudad NVARCHAR (30), 
    @Estado NVARCHAR (30), 
    @Pais NVARCHAR (30), 
    @ZonaTuristica BIT, 
    @Locacion VARCHAR (60),
    @NoPisos INT,
	@FechaInicio Date,
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
GO;

CREATE OR ALTER PROCEDURE RegistrarHabitacion
(
	@NoHabitacion VARCHAR(100),
	@Piso INT,
	@TipoHabitacion VARCHAR(20),
    @NoNomina INT
)
AS
BEGIN
    DECLARE @CodTDH INT, @idHotel INT, @PisosHotel INT;

    -- Obtener CodTDH y idHotel desde TiposHabitacion
    SELECT @CodTDH = CodTDH, @idHotel = idHotel
    FROM TiposHabitacion
    WHERE NivelHabitacion = @TipoHabitacion;

    -- Obtener la cantidad de pisos del hotel
    SELECT @PisosHotel = NoPisos
    FROM Hotel
    WHERE CodHotel = @idHotel;

    -- Validar que el piso no sea mayor que la cantidad de pisos del hotel
    IF @Piso > @PisosHotel
    BEGIN
        RAISERROR('El número de piso excede la cantidad de pisos del hotel.', 16, 1);
        RETURN;
    END

    -- Insertar habitación
    INSERT INTO Habitacion(
		NoHabitacion,
		Piso,
		TipoHabitacion
    )
    VALUES
    (
		@NoHabitacion,
		@Piso,
		@CodTDH
    );

    -- Registrar operación
    INSERT INTO Operacion(Accion, Descripcion, Usuario)
    VALUES ('Registro de Habitacion', 'Administrador ha Registrado una Habitacion', @NoNomina);
END

GO;

CREATE OR ALTER PROCEDURE EditarHabitacion
(
	@NoHabitacion VARCHAR(100),
	@Piso INT,
	@TipoHabitacion VARCHAR(20),
    @NoNomina INT
)
AS
BEGIN
    DECLARE @CodTDH INT, @idHotel INT, @PisosHotel INT;

    -- Obtener CodTDH y idHotel desde TiposHabitacion
    SELECT @CodTDH = CodTDH, @idHotel = idHotel
    FROM TiposHabitacion
    WHERE NivelHabitacion = @TipoHabitacion;

    -- Obtener la cantidad de pisos del hotel
    SELECT @PisosHotel = NoPisos
    FROM Hotel
    WHERE CodHotel = @idHotel;

    -- Validar que el piso no sea mayor que la cantidad de pisos del hotel
    IF @Piso > @PisosHotel
    BEGIN
        RAISERROR('El número de piso excede la cantidad de pisos del hotel.', 16, 1);
        RETURN;
    END

    -- Actualizar la habitación
    UPDATE Habitacion
    SET
        Piso = @Piso,
        TipoHabitacion = @CodTDH
    WHERE NoHabitacion = @NoHabitacion;

    -- Registrar operación
    INSERT INTO Operacion(Accion, Descripcion, Usuario)
    VALUES ('Edición de Habitacion', 'Administrador ha editado una habitación', @NoNomina);
END;

GO;

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
		r.Entrada,
		r.Salida,
		r.Estatus,
		r.CheckIn,
		r.CheckOut,
		NombreHotel,
		f.Anticipo,
		f.PrecioInicial,
		PrecioTotal,
		f.PrecioServicios
	FROM Reservacion r JOIN Hotel h ON CodHotel = Hotel JOIN Factura f ON f.Reservacion = r.CodReservacion WHERE r.Cliente = @RFC;
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

CREATE OR ALTER PROCEDURE GetHabitacionesHotel
(
	@Hotel NVarchar (100)
)
AS
BEGIN
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
		Estatus,
		Reservacion,
		Hospedaje
	FROM ViewHabitaciones h 
	WHERE
		NombreHotel = @Hotel;
END
GO;

CREATE OR ALTER PROCEDURE ReservarHabitacion
(
	@Reservacion UNIQUEIDENTIFIER,
	@Habitacion INT,
	@Personas INT
)
AS
BEGIN
	INSERT INTO ReservacionHabitaciones (
		Reservacion,
		Habitacion,
		Personas
	) VALUES (
		@Reservacion,
		@Habitacion,
		@Personas
	);
END
GO;

CREATE OR ALTER PROCEDURE RegistrarReservacion
(
	@Cliente NVARCHAR (15),
	@Ciudad NVARCHAR (30),
	@Hotel NVARCHAR (100),
	@Entrada DATE,
	@Salida DATE,
	@Estatus NVARCHAR(11),
	@Anticipo Money,
	@NoNomina INT,
	
	@ResId UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN
	SET @ResId = NEWID();

    INSERT INTO Reservacion(
		CodReservacion,
		Cliente,
		Ciudad,
		Hotel,
		Entrada,
		Salida,
		Estatus,
		Anticipo
    )
    VALUES
    (
		@ResId,
		@Cliente,
		@Ciudad,
		(SELECT CodHotel FROM Hotel WHERE NombreHotel = @Hotel),
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
	Update Reservacion SET
		Estatus = 'Cancelado'
	WHERE CodReservacion = @Codigo;
	
    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Reservacion Cancelada', 'Usuario ha Cancelado una Reservacion', @NoNomina);
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

    INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Check In Realizado', 'Usuario ha realizado un Check In de Reservacion', @NoNomina);
END
GO;

CREATE OR ALTER PROCEDURE CheckOut
( 
	@Reservacion UNIQUEIDENTIFIER,
	@Fecha DATE,
	@Descuento MONEY,
	@NombreDescuento NVARCHAR(50),
	@NoNomina INT
)
AS
BEGIN
	-- Validación: Reservación concluida
	IF EXISTS (
		SELECT 1 
		FROM Reservacion 
		WHERE CodReservacion = @Reservacion AND Estatus = 'Concluido'
	)
	BEGIN
		RAISERROR('No se puede realizar el Check Out porque la reservación ya está concluida.', 16, 1);
		RETURN;
	END

	-- Validación: Fecha de CheckOut antes de la FechaInicio
	IF EXISTS (
		SELECT 1
		FROM Reservacion
		WHERE CodReservacion = @Reservacion AND @Fecha < Entrada
	)
	BEGIN
		RAISERROR('No se puede realizar el Check Out antes de la fecha de inicio.', 16, 1);
		RETURN;
	END

	-- Actualizar descuento en factura
	UPDATE Factura SET
		Descuento = @Descuento,
		NombreDescuento = @NombreDescuento
	WHERE Reservacion = @Reservacion;

	-- Actualizar Checkout y estatus en reservación
	UPDATE Reservacion
	SET 
		CheckOut = @Fecha,
		Estatus = 'Concluido',
		Salida = CASE WHEN @Fecha <> Salida THEN @Fecha ELSE Salida END
	WHERE CodReservacion = @Reservacion;

	-- Registrar operación
	INSERT INTO Operacion(Accion, Descripcion, Usuario) 
	VALUES ('Check Out Realizado', 'Usuario ha realizado un Check Out de Reservacion', @NoNomina);

	-- Recalcular el total en la factura
	UPDATE f SET 
		f.PrecioTotal = 
			((ISNULL(f.PrecioInicial, 0) + ISNULL(f.PrecioServicios, 0)) * (1 - (ISNULL(f.Descuento, 0) / 100.0))) 
			- ISNULL(f.Anticipo, 0)
	FROM Factura f 
	WHERE f.Reservacion = @Reservacion;
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
	FROM Factura WHERE Reservacion = @Reservacion
END
GO;

CREATE OR ALTER PROCEDURE HistorialCliente (
	@RFC VARCHAR(15)
) AS BEGIN
	SELECT
		c.Nombre,
		r.Ciudad,
		h.NombreHotel AS Hotel,
		tb.NivelHabitacion AS [Tipo de Habitacion],
		hb.NoHabitacion AS [Numero de Habitacion],
		rh.Personas AS [Numero de Personas Hospedadas],
		r.CodReservacion AS [Codigo de Reservacion],
		r.Entrada AS [Fecha de Reservacion],
		r.Estatus AS [Estatus de Reservacion],
		r.Anticipo AS [Anticipo de Reservacion],
		f.PrecioInicial AS [Monto de Hospedaje],
		f.PrecioServicios AS [Monto de Servicios Adicionales],
		f.PrecioTotal AS [Total de Factura]
	FROM Cliente c
	JOIN Reservacion r ON
		r.Cliente = c.RFC
	JOIN Hotel h ON
		r.Hotel = h.CodHotel
	JOIN ReservacionHabitaciones rh ON
		rh.Reservacion = r.CodReservacion
	JOIN Habitacion hb ON
		rh.Habitacion = hb.Codigo
	JOIN TiposHabitacion tb ON
		hb.TipoHabitacion = tb.CodTDH
	JOIN Factura f ON
		f.Reservacion = r.CodReservacion
	WHERE
		c.RFC = @RFC;
END
GO;

/*lo usa reporte ocupaciones 1ra tabla*/
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
		h.NombreHotel AS Hotel,
		@Year AS Año,
		MONTH(r.CheckIn) AS Mes,
		th.NivelHabitacion AS [Tipo de Habitacion],
		COUNT(hb.Codigo) AS [Cantidad de Habitaciones],
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
		th.NivelHabitacion;
END
GO;

