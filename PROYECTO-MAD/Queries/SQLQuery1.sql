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
	Descripcion VARCHAR (200) NOT NULL,
	Hotel INT NOT NULL,
	FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel)
);
ALTER TABLE Servicio ADD Hotel INT;
ALTER TABLE Servicio
ADD CONSTRAINT FK_Servicio_Hotel
FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel);

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
ALTER TABLE Hotel ADD FechaInicio Date Not Null;
ALTER TABLE Hotel DROP COLUMN Amenidades;
ALTER TABLE Hotel DROP COLUMN ZonaTuristica;
ALTER TABLE Hotel ADD ZonaTuristica Bit Not Null;

DROP TABLE TiposHabitacion;
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
ALTER TABLE TiposHabitacion DROP COLUMN TipoCama;
ALTER TABLE TiposHabitacion ADD TipoCama VARCHAR(300);

CREATE TABLE HotelesServicio(
	idHotelesServicio INT PRIMARY KEY IDENTITY(1,1),
	Hotel INT NOT NULL,
	Servicio INT NOT NULL,
	CostoExtra MONEY NOT NULL,
	FOREIGN KEY (Servicio) REFERENCES Servicio(CodServicio),
	FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel)
);
DROP TABLE HotelesServicio;

CREATE TABLE Habitacion(
	NoHabitacion INT PRIMARY KEY,
	Estatus VARCHAR (50) NOT NULL,
	Piso INT CHECK(Piso > 0),
	TipoHabitacion INT NOT NULL,
	FOREIGN KEY (TipoHabitacion) REFERENCES TiposHabitacion(CodTDH),
);
ALTER TABLE Habitacion DROP COLUMN Reservacion;
ALTER TABLE Habitacion ADD Reservacion UNIQUEIDENTIFIER NULL;
ALTER TABLE Habitacion ADD CONSTRAINT FK_Reservacion_Usuario FOREIGN KEY (Reservacion) REFERENCES Reservacion(CodReservacion);
ALTER TABLE Habitacion DROP CONSTRAINT FK_Reservacion_Usuario;

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
	CheckIn DATE NULL,
	CheckOut DATE NULL,
	FOREIGN KEY (Cliente) REFERENCES Cliente(RFC),
	FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel)
);
ALTER TABLE Reservacion ADD TipoHabitacion INT DEFAULT (4);
ALTER TABLE Reservacion ADD Anticipo MONEY DEFAULT (0);
ALTER TABLE Reservacion DROP COLUMN CheckIn;
ALTER TABLE Reservacion DROP COLUMN CheckOut;
ALTER TABLE Reservacion ADD CheckIn DATE DEFAULT (NULL);
ALTER TABLE Reservacion ADD CheckOut DATE DEFAULT (NULL);

CREATE TABLE Checks(
	idCheck INT PRIMARY KEY IDENTITY(1,1),
	FechaCheck DATE,
	Tipo VARCHAR (10) NOT NULL,
	idReservacion UNIQUEIDENTIFIER NOT NULL,
	FOREIGN KEY (idReservacion) REFERENCES Reservacion(CodReservacion)
);
DROP TABLE Checks;

DROP TABLE Factura;
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
	FOREIGN KEY (Hotel) REFERENCES Hotel(CodHotel),
	FOREIGN KEY (Cliente) REFERENCES Cliente(RFC)
);
ALTER TABLE  Factura DROP COLUMN Servicios;
ALTER TABLE  Factura DROP COLUMN Entrada;
ALTER TABLE  Factura DROP COLUMN Salida;
ALTER TABLE  Factura ADD Entrada INT NULL;
ALTER TABLE  Factura ADD Salida INT NULL;
ALTER TABLE  Factura ADD Reservacion UNIQUEIDENTIFIER NULL;
ALTER TABLE  Factura ADD PrecioInicial Money Default(0);
ALTER TABLE  Factura ADD PrecioServicios Money Default(0);

CREATE TABLE ServiciosAdicionales(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Reservacion UNIQUEIDENTIFIER NOT NULL,
	Servicio INT NOT NULL,
	FOREIGN KEY (Reservacion) REFERENCES Reservacion(CodReservacion)
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

UPDATE Usuario SET Estado = 1 WHERE NoNomina = 2004;

ALTER TABLE Servicio
ADD idHotel INT FOREIGN KEY (idHotel) REFERENCES Hotel(CodHotel);
