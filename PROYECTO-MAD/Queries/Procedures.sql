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
    SELECT u.NoNomina, u.TipoUsuario
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
	@TipoUsuario NVARCHAR(15)
)
AS
BEGIN
	DECLARE @ContrasIguales AS INT;
	DECLARE @TotalContras AS INT;

	SELECT @ContrasIguales = Count(c.Contrasenna) FROM Contrasenna c WHERE c.Contrasenna = @Contrasenna AND c.idUsuario = @NoNomina;
	IF @ContrasIguales > 0 BEGIN RETURN; END
	
	INSERT INTO Contrasenna (idUsuario, Contrasenna) VALUES (@NoNomina, @Contrasenna);

	SELECT @TotalContras = Count(c.Contrasenna) FROM Contrasenna c WHERE c.idUsuario = @NoNomina;
	IF @TotalContras > 2 BEGIN
		DELETE FROM Contrasenna WHERE idUsuario = @NoNomina AND idContrasenna = (SELECT TOP 1 idContrasenna FROM Contrasenna WHERE idUsuario = @NoNomina ORDER BY idContrasenna ASC);
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
		TipoUsuario = @TipoUsuario
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

GO
CREATE OR ALTER PROCEDURE GetUsuarios
AS
BEGIN
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
END
GO

EXEC GetUsuarios;
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
	FROM Cliente;
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
		FechaInicio
	FROM Hotel;
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

Truncate table TiposHabitacion;

Select * from Hotel;
Select * from TiposHabitacion;

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

	WHERE RFC= @RFC;
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

	WHERE ApellidoPaterno = @ApellidoMaterno AND ApellidoMaterno = @ApellidoMaterno;
END