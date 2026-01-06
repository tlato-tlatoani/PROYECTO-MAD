USE GatoHotelero;

SELECT * FROM Usuario;
SELECT * FROM Contrasenna;
SELECT * FROM Cliente;
SELECT * FROM Reservacion;
SELECT * FROM TiposHabitacion;
SELECT * FROM Habitacion;
SELECT * FROM Factura;
SELECT * FROM Servicio;
SELECT * FROM ServiciosAdicionales;
SELECT * FROM Hotel;
SELECT * FROM Operacion;

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

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Cliente al que se le esta facturando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'Cliente';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Hotel donde se esta generando la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'Hotel';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Anticipo que da el cliente al hacer el check out.Este se resta en la factura final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'Anticipo';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento extra aplicado a la factura final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'Descuento';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la reservacion que se esta facturando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'Reservacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Precio base de hospedaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'PrecioInicial';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Precio conjunto de los servicios adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Factura', @level2type = N'COLUMN', @level2name = N'PrecioServicios';

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

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Id de el servicio adicional aplicado a la reservación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ServiciosAdicionales', @level2type = N'COLUMN', @level2name = N'Id';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la reservación en la que aparece', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ServiciosAdicionales', @level2type = N'COLUMN', @level2name = N'Reservacion';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio que se considera adicional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ServiciosAdicionales', @level2type = N'COLUMN', @level2name = N'Servicio';

EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo de contrase�a �nico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contrasenna', @level2type = N'COLUMN', @level2name = N'idContrasenna';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'C�digo de usuario �nico del usuario al que pertenece la contrase�a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Contrasenna', @level2type = N'COLUMN', @level2name = N'idUsuario';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Contrase�a de del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'Contrasenna';
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'Administrador (0) u Operativo(1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Usuario', @level2type = N'COLUMN', @level2name = N'TipoUsuario';

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

