CREATE DATABASE GatoHotelero;

USE GatoHotelero;


CREATE TABLE Cliente(
	RFC VARCHAR (15) PRIMARY KEY,
	Nombre VARCHAR (50) NOT NULL,
	ApellidoPaterno VARCHAR (50) NOT NULL,
	ApellidoMaterno VARCHAR (50) NOT NULL,
	Ciudad VARCHAR (30) NOT NULL,
	Estado VARCHAR (30) NOT NULL,
	Pais VARCHAR (30) NOT NULL,
	CorreoElectronico VARCHAR (40) NOT NULL,
	TelCelular VARCHAR (10) NOT NULL,
	TelCasa VARCHAR (10) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	EstadoCivil VARCHAR (10) NOT NULL,
);

CREATE TABLE Servicio(
	CodServicio INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR (50) NOT NULL,
	Precio MONEY NOT NULL,
	Descripcion VARCHAR (200) NOT NULL
);

CREATE TABLE Hotel(
	CodHotel INT PRIMARY KEY IDENTITY(1,1),
	NombreHotel VARCHAR (100) NOT NULL,
	Ciudad VARCHAR (30) NOT NULL,
	Estado VARCHAR (30) NOT NULL,
	Pais VARCHAR (30) NOT NULL,
	Amenidades VARCHAR (200) NOT NULL,
	ZonaTuristica VARCHAR (200) NOT NULL,
	Locacion VARCHAR (60) NOT NULL,
	NoPisos INT CHECK(NoPisos > 0)
);

CREATE TABLE TiposHabitacion(
	CodTDH INT PRIMARY KEY IDENTITY(1,1),
	NivelHabitacion VARCHAR(20) NOT NULL,
	NoCamas INT CHECK(NoCamas > 0),
	TipoCama VARCHAR (30) NOT NULL,
	PrecioNoche MONEY NULL,
	CantPersonasMax INT NULL,
	Locacion VARCHAR (60) NOT NULL,
	Amenidades VARCHAR (100) NOT NULL,
	idHotel INT NOT NULL,
	FOREIGN KEY (idHotel) REFERENCES Hotel(CodHotel)
);

CREATE TABLE HotelesServicio(
	idHotelesServicio INT PRIMARY KEY IDENTITY(1,1),
	Hotel INT NOT NULL,
	Servicio INT NOT NULL,
	CostoExtra MONEY NOT NULL,
	FOREIGN KEY (Servicio) REFERENCES Servicio(CodServicio),
	FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel)
);

CREATE TABLE Habitacion(
	NoHabitacion INT PRIMARY KEY,
	Estatus VARCHAR (50) NOT NULL,
	Piso INT CHECK(Piso > 0),
	TipoHabitacion INT NOT NULL,
	FOREIGN KEY (TipoHabitacion) REFERENCES TiposHabitacion(CodTDH),
);

CREATE TABLE Reservacion(
	CodReservacion UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Cliente VARCHAR (15) NOT NULL,
	Ciudad VARCHAR (30) NOT NULL,
	Hotel INT NOT NULL,
	CantHabitaciones INT CHECK(CantHabitaciones > 0),
	CantPersonas INT CHECK(CantPersonas > 0),
	Entrada DATE NULL,
	Salida DATE NULL,
	Estatus VARCHAR(11) NULL,
	idCheckIn INT NULL,
	idCheckOut INT NULL,
	FOREIGN KEY (Cliente) REFERENCES Cliente(RFC),
	FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel)
);

CREATE TABLE Checks(
	idCheck INT PRIMARY KEY IDENTITY(1,1),
	FechaCheck DATE,
	Tipo VARCHAR (10) NOT NULL,
	idReservacion UNIQUEIDENTIFIER NOT NULL,
	FOREIGN KEY (idReservacion) REFERENCES Reservacion(CodReservacion)
);

CREATE TABLE Factura(
	NoFactura INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PrecioTotal MONEY NOT NULL,
	NombreDescuento VARCHAR (50) NOT NULL,
	Descuento MONEY NOT NULL,
	Anticipo MONEY NOT NULL,
	FechaCreacion DATE DEFAULT GETDATE(),
	FormaPago VARCHAR(50) NOT NULL,
	Entrada INT NOT NULL,
	Salida INT NOT NULL,
	Cliente VARCHAR (15) NOT NULL,
	Hotel INT NOT NULL,
	Servicios INT NOT NULL,
	FOREIGN KEY (Servicios) REFERENCES HotelesServicio(idHotelesServicio),
	FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel),
	FOREIGN KEY (Cliente) REFERENCES Cliente(RFC),
	FOREIGN KEY (Salida) REFERENCES Checks(idCheck),
	FOREIGN KEY (Entrada) REFERENCES Checks(idCheck)
);


CREATE TABLE Usuario (
	NoNomina INT PRIMARY KEY,
	Nombre VARCHAR (50) NOT NULL,
	ApellidoPaterno VARCHAR (50) NOT NULL,
	ApellidoMaterno VARCHAR (50) NOT NULL,
	CorreoElectronico VARCHAR (40) NOT NULL,
	Contrasenna INT NOT NULL,
	TelCelular VARCHAR (10) NOT NULL,
	TelCasa VARCHAR (10) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	TipoUsuario VARCHAR(15) NOT NULL,
	Estado BIT NOT NULL Default 0
);

CREATE TABLE Contrasenna(
	idContrasenna INT PRIMARY KEY IDENTITY(1,1),
	FechaCreacion DATETIME DEFAULT GETDATE(),
	Contrasenna VARCHAR(20) NOT NULL,
	idUsuario INT NOT NULL,
	FOREIGN KEY (idUsuario) REFERENCES Usuario(NoNomina)
);


CREATE TABLE Operacion(
	idRegistroAfectado INT PRIMARY KEY IDENTITY(1,1),
	Accion Varchar(100) NOT NULL,
	Descripcion Varchar(200) NOT NULL,
	Usuario INT NOT NULL,
	Fecha DATETIME DEFAULT GETDATE()
);

use GatoHotelero;

ALTER TABLE Usuario
DROP COLUMN TipoUsuario;

ALTER TABLE Usuario
ADD TipoUsuario BIT NOT NULL;

ALTER TABLE Usuario
DROP COLUMN Contrasenna

ALTER TABLE Usuario
ADD Contrasenna VARCHAR (20) NOT NULL;

ALTER TABLE Usuario
ADD Estado BIT NOT NULL Default 0;

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Contrase�a de del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'Contrasenna';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Administrador (0) u Operativo(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'TipoUsuario';

select * from Usuario;
select*from Contrasenna

insert into Usuario (NoNomina, Nombre, ApellidoPaterno, ApellidoMaterno, CorreoElectronico, Contrasenna, TelCelular, TelCasa, FechaNacimiento, TipoUsuario) values (1023, 'Laura', 'Mart�nez', 'Perez', 'martinezperez@gmail.com', 1, 8180101836, 111111111,'2004-06-04',1);
insert into Contrasenna(FechaCreacion, Contrasenna, idUsuario) values ('07-05-2025', 'Flordecerezo01*', 1023);
insert into Contrasenna(FechaCreacion, Contrasenna, idUsuario) values ('08-05-2025', 'Flordecerezo02*', 1023);


GO
CREATE PROCEDURE Validar
(
    -- Add the parameters for the stored procedure here
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

CREATE PROCEDURE Registrar
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

CREATE PROCEDURE Editar
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
	UPDATE Usuario SET
		Nombre = @Nombre, 
		ApellidoPaterno = @ApellidoPaterno, 
		ApellidoMaterno = @ApellidoMaterno, 
		CorreoElectronico = @CorreoElectronico,
		TelCelular = @TelCelular, 
		TelCasa = @TelCasa, 
		FechaNacimiento = @FechaNacimiento, 
		TipoUsuario = @TipoUsuario
	WHERE NoNomina = @NoNomina;
	
	INSERT INTO Operacion(Accion, Descripcion, Usuario) VALUES ('Edicion de Usuario [Administrador]', 'Administrador ha Editado un Usuario', @NoNomina);
END
GO

update Usuario set estado=1 
EXEC Validar @email='martinezperez@gmail.com', @contrasenna='Flordecerezo01*', @tipousuario = 1;
GO


GO
CREATE PROCEDURE GetUsuarios
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


ALTER TABLE TiposHabitacion
drop column NivelHabitacion;

ALTER TABLE TiposHabitacion
add NivelHabitacion varchar(20) not null;

SELECT * FROM Usuario;
SELECT * FROM Contrasenna;
SELECT * FROM Cliente;
SELECT * FROM Reservacion;
SELECT * FROM TiposHabitacion;
SELECT * FROM Habitacion;
SELECT * FROM Factura;
SELECT * FROM Servicio;
SELECT * FROM Hotel;
SELECT * FROM HotelesServicio;
SELECT * FROM Operacion;
SELECT * FROM Checks;

UPDATE Usuario SET Estado = 1 WHERE NoNomina = 2004;
TRUNCATE TABLE Contrasenna;
DELETE FROM USUARIO WHERE NoNomina = 2004;

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'El RFC �nico del cliente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE',  @level1name = N'Cliente',  @level2type = N'COLUMN', @level2name = N'RFC';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o nombres sin apellidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'Nombre';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Correo en cualquier formato, ya sea gmail, hotmail, etc�', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'CorreoElectronico';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono celular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'TelCelular';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono de casa fijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'TelCasa';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Casado, soltero o divociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'EstadoCivil';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Servicio', @level2type = N'COLUMN', @level2name = N'CodServicio';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Descripci�n del servicio que se provee', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Servicio', @level2type = N'COLUMN', @level2name = N'Descripcion';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo �nico del hotel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hotel', @level2type = N'COLUMN', @level2name = N'CodHotel';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del hotel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hotel', @level2type = N'COLUMN', @level2name = N'NombreHotel';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cortesias del hotel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hotel', @level2type = N'COLUMN', @level2name = N'Amenidades';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Si la zona del hotel es en alguna zona de alta demanda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hotel', @level2type = N'COLUMN', @level2name = N'ZonaTuristica';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'El n�mero de pisos del hotel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Hotel', @level2type = N'COLUMN', @level2name = N'NoPisos';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo �nico del tipo de habitaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TiposHabitacion', @level2type = N'COLUMN', @level2name = N'CodTDH';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'El nivel/nombre del tipo de habitaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TiposHabitacion', @level2type = N'COLUMN', @level2name = N'NivelHabitacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de camas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TiposHabitacion', @level2type = N'COLUMN', @level2name = N'NoCamas';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de personas m�xima que maneja este tipo de habitaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TiposHabitacion', @level2type = N'COLUMN', @level2name = N'CantPersonasMax';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cortes�as', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TiposHabitacion', @level2type = N'COLUMN', @level2name = N'Amenidades';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo del hotel donde se encuentra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TiposHabitacion', @level2type = N'COLUMN', @level2name = N'idHotel';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo identificador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HotelesServicio', @level2type = N'COLUMN', @level2name = N'idHotelesServicio';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Costo que el hotel correspondiente aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HotelesServicio', @level2type = N'COLUMN', @level2name = N'CostoExtra';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'N�mero de habitaci�n �nico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Habitacion', @level2type = N'COLUMN', @level2name = N'NoHabitacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Disponible u ocupada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Habitacion', @level2type = N'COLUMN', @level2name = N'Estatus';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo �nico de la reservaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'CodReservacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de habitaciones a reservar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'CantHabitaciones';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de personas en la reservaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'CantPersonas';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Comienzo de la reservaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'Entrada';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Final de la reservaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'Salida';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cancelado o Activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'Estatus';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de llegada de los huespedes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'idCheckIn';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de salida de los huespedes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Reservacion', @level2type = N'COLUMN', @level2name = N'idCheckOut';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo �nico del chequeo de horario en reservaci�n', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Checks', @level2type = N'COLUMN', @level2name = N'idCheck';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en la que se hace el chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Checks', @level2type = N'COLUMN', @level2name = N'FechaCheck';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo �nico de la reservaci�n en la que se hace ese chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Checks', @level2type = N'COLUMN', @level2name = N'idReservacion';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'N�mero �nico de factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'NoFactura';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de descuento aplicado en la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'NombreDescuento';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creaci�n de la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'FechaCreacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'D�bito, Efectivo, Cr�dito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'FormaPago';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo �nico de n�mina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'NoNomina';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Contrase�a de del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'Contrasenna';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Tel�fono celular del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'TelCelular';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Tel�fono de casa fijo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'TelCasa';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Administrador u Operativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'TipoUsuario';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo de contrase�a �nico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contrasenna', @level2type = N'COLUMN', @level2name = N'idContrasenna';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo de usuario �nico del usuario al que pertenece la contrase�a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contrasenna', @level2type = N'COLUMN', @level2name = N'idUsuario';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo �nico del registro afectado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Operacion', @level2type = N'COLUMN', @level2name = N'idRegistroAfectado';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla con la que se relaciona el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Operacion', @level2type = N'COLUMN', @level2name = N'TablaAfectada';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Que operaci�n se realiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Operacion', @level2type = N'COLUMN', @level2name = N'TipoOperacion';

SELECT S.NAME AS [SCHEMA_TABLE], 
       T.NAME AS [TABLE NAME], 
       C.NAME AS [COLUMN NAME], 
       P.NAME AS [DATA TYPE], 
       CASE C.max_length 
         WHEN -1 THEN 'MAX' 
         ELSE CONVERT(VARCHAR, C.max_length) 
       END AS [SIZE], 
       CASE c.is_nullable 
         WHEN 0 THEN 'No Nulo' 
         ELSE 'Nulo' 
       END AS [Nullable], 
       CASE c.is_identity 
         WHEN 0 THEN '' 
         ELSE 'PK' 
       END AS [PK], 
       CASE 
         WHEN ( fk.object_id IS NULL ) THEN '' 
         ELSE 'FK' 
       END AS [FK], 
       Isnull(sep.value, '') [Description] 
FROM   sys.objects AS T 
       JOIN sys.columns AS C ON T.object_id = C.object_id 
       JOIN sys.schemas AS S ON T.schema_id = S.schema_id 
       JOIN sys.types AS P ON C.system_type_id = P.system_type_id 
       LEFT JOIN sys.extended_properties sep ON C.object_id = sep.major_id AND C.column_id = sep.minor_id AND sep.NAME = 'MS_DescriptiON' 
       LEFT JOIN (sys.foreign_keys fk 
                  INNER JOIN sys.foreign_key_columns fc ON ( fk.object_id = fc.constraint_object_id )) 
              ON ( ( fk.parent_object_id = C.object_id ) 
                   AND ( fc.parent_column_id = C.column_id ) ) 
WHERE  T.type_desc = 'USER_TABLE' 
       --AND s.NAME <> 'dbo' 
ORDER  BY s.NAME, 
          T.NAME, 
          c.column_id; 

