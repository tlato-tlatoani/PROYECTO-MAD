CREATE DATABASE GatoHotelero2;
USE GatoHotelero2;

DROP TABLE Usuario;
CREATE TABLE Usuario (
	NoNomina INT PRIMARY KEY,
	Nombre VARCHAR (50) NOT NULL,
	ApellidoPaterno VARCHAR (50) NOT NULL,
	ApellidoMaterno VARCHAR (50) NOT NULL,
	CorreoElectronico VARCHAR (40) UNIQUE NOT NULL,
	Contrasenna VARCHAR(20) NOT NULL,
	OldContrasenna1 VARCHAR(20),
	OldContrasenna2 VARCHAR(20),
	TelCelular VARCHAR (10) NOT NULL,
	TelCasa VARCHAR (10) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	TipoUsuario VARCHAR(15) NOT NULL,
	Estado BIT NOT NULL Default 0
);

DROP TABLE Operacion;
CREATE TABLE Operacion(
	idRegistroAfectado INT PRIMARY KEY IDENTITY(1,1),
	Accion Varchar(100) NOT NULL,
	Descripcion Varchar(200) NOT NULL,
	Usuario INT NOT NULL,
	Fecha DATETIME DEFAULT GETDATE()
);

DROP TABLE Cliente;
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

DROP TABLE Hotel;
CREATE TABLE Hotel(
	CodHotel INT PRIMARY KEY IDENTITY(1,1),
	NombreHotel VARCHAR (100) NOT NULL,
	Ciudad VARCHAR (30) NOT NULL,
	Estado VARCHAR (30) NOT NULL,
	Pais VARCHAR (30) NOT NULL,
	Amenidades VARCHAR (200) DEFAULT(''),
	ZonaTuristica BIT NOT NULL,
	Locacion VARCHAR (60) NOT NULL,
	NoPisos INT CHECK(NoPisos > 0),
	FechaInicio Date Not Null
);

DROP TABLE Servicio;
CREATE TABLE Servicio(
	CodServicio INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR (50) NOT NULL,
	Precio MONEY NOT NULL,
	Descripcion VARCHAR (200) NOT NULL,
	Hotel INT NOT NULL
);

DROP TABLE TiposHabitacion;
CREATE TABLE TiposHabitacion (
	CodTDH INT PRIMARY KEY IDENTITY(1,1),
	NivelHabitacion VARCHAR(20) NOT NULL,
	NoCamas INT CHECK(NoCamas > 0),
	TipoCama VARCHAR (300) NOT NULL,
	PrecioNoche MONEY NULL,
	CantPersonasMax INT NULL,
	Locacion VARCHAR (60) NOT NULL,
	Amenidades VARCHAR (100) NOT NULL,
	idHotel INT NOT NULL
);

DROP TABLE Habitacion;
CREATE TABLE Habitacion (
	Codigo INT IDENTITY PRIMARY KEY,
	NoHabitacion VARCHAR(100) NOT NULL,
	Piso INT CHECK(Piso > 0),
	TipoHabitacion INT NOT NULL,
);

DROP TABLE Reservacion;
CREATE TABLE Reservacion(
	CodReservacion UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Cliente VARCHAR (15) NOT NULL,
	Ciudad VARCHAR (30) NOT NULL,
	Hotel INT NOT NULL,
	Entrada DATE NULL,
	Salida DATE NULL,
	Estatus VARCHAR(11) NULL,
	CheckIn DATE NULL,
	CheckOut DATE NULL,
	Anticipo MONEY DEFAULT (0)
);

DROP TABLE Factura;
CREATE TABLE Factura(
	NoFactura INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PrecioTotal MONEY NOT NULL,
	PrecioInicial Money Default(0),
	PrecioServicios Money Default(0),
	NombreDescuento VARCHAR (50) NOT NULL,
	Descuento MONEY NOT NULL,
	Anticipo MONEY NOT NULL,
	FechaCreacion DATE DEFAULT GETDATE(),
	FormaPago VARCHAR(50) NOT NULL,
	Cliente VARCHAR (15) NOT NULL,
	Hotel INT NOT NULL,
	Reservacion UNIQUEIDENTIFIER NULL
);

DROP TABLE ReservacionHabitaciones;
CREATE TABLE ReservacionHabitaciones(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	
	Reservacion UNIQUEIDENTIFIER NOT NULL,
	Habitacion INT NOT NULL,

	Personas INT NOT NULL
);

DROP TABLE ServiciosAdicionales;
CREATE TABLE ServiciosAdicionales(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Reservacion UNIQUEIDENTIFIER NOT NULL,
	Servicio INT NOT NULL
);