using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class EntReservacion
    {
        public Guid CodReservacion;
        public string Cliente;
        public string Ciudad;
        public int Hotel;
        public int TipoHabitacion;
        public int CantHabitaciones;
        public int CantPersonas;
        public DateTime Entrada;
        public DateTime Salida;
        public string Estatus;
        public int idCheckIn;
        public int idCheckOut;

        public string HotelNombre;
        public string TipoHabitacionNombre;
        public int Dias;
        public decimal PrecioNoche;
        public int NoHabitacion;

        public decimal Anticipo;
        public decimal Monto;
        public decimal Servicios;
        public decimal Total;

        public EntReservacion(
            Guid _CodReservacion,
            string _Cliente,
            string _Ciudad,
            string _Hotel,
            string _TipoHabitacion,
            int _CantHabitaciones,
            int _CantPersonas,
            DateTime _Entrada,
            DateTime _Salida,
            string _Estatus,
            int _idCheckIn,
            int _idCheckOut
        )
        {
            CodReservacion = _CodReservacion;
            Cliente = _Cliente;
            Ciudad = _Ciudad;
            HotelNombre = _Hotel;
            TipoHabitacionNombre = _TipoHabitacion;
            CantHabitaciones = _CantHabitaciones;
            CantPersonas = _CantPersonas;
            Entrada = _Entrada;
            Salida = _Salida;
            Estatus = _Estatus;
            idCheckIn = _idCheckIn;
            idCheckOut = _idCheckOut;
        }
        public EntReservacion() { }
    }

}