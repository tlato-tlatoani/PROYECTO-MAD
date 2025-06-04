USE GatoHotelero2;

SELECT * FROM Usuario;
SELECT * FROM Habitacion;
SELECT * FROM Hotel;

SELECT * FROM Reservacion;
SELECT * FROM ReservacionHabitaciones;
SELECT * FROM Factura;

TRUNCATE TABLE Usuario;
TRUNCATE TABLE Reservacion;
TRUNCATE TABLE ReservacionHabitaciones;
TRUNCATE TABLE ServiciosAdicionales;

UPDATE Usuario SET TipoUsuario = 1, Estado = 1;