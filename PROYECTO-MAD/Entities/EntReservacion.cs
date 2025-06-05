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
        public DateTime CheckIn;
        public DateTime CheckOut;

        public string HotelNombre;
        public string TipoHabitacionNombre;
        public int Dias;
        public decimal PrecioNoche;
        public int NoHabitacion;

        public decimal Anticipo;
        public decimal Monto;
        public decimal Servicios;
        public decimal Total;

        public static EntClientes m_cliente = null;
        public static Hotel m_hotel = null;


        public EntReservacion(
            Guid _CodReservacion,
            string _Cliente,
            string _Ciudad,
            string _Hotel,
            DateTime _Entrada,
            DateTime _Salida,
            string _Estatus,
            decimal _anticipo
        )
        {
            CodReservacion = _CodReservacion;
            Cliente = _Cliente;
            Ciudad = _Ciudad;
            HotelNombre = _Hotel;
            Entrada = _Entrada;
            Salida = _Salida;
            Estatus = _Estatus;
            Anticipo = _anticipo;
        }
        public EntReservacion() { }
    }

}