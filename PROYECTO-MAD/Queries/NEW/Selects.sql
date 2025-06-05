USE GatoHotelero2;

SELECT * FROM Usuario;
SELECT * FROM Habitacion;
SELECT * FROM Hotel;

SELECT * FROM Reservacion;
SELECT * FROM ReservacionHabitaciones;
SELECT * FROM Factura;

SELECT * FROM Cliente;
SELECT * FROM Reservacion;

TRUNCATE TABLE Factura
TRUNCATE TABLE Usuario;
TRUNCATE TABLE Reservacion;
TRUNCATE TABLE ReservacionHabitaciones;
TRUNCATE TABLE ServiciosAdicionales;

UPDATE Usuario SET TipoUsuario = 0, Estado = 1 WHERE NoNomina=1235;