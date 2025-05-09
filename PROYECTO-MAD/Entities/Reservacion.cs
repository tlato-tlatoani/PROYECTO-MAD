using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class Reservacion
    {
        public int CodReservacion;
        public string Cliente;
        public string Ciudad;
        public int Hotel;
        public int CantHabitaciones;
        public int CantPersonas;
        public DateTime Entrada;
        public DateTime Salida;
        public string Estatus;
        public int idCheckIn;
        public int idCheckOut;

        public Reservacion(
            int _CodReservacion,
        string _Cliente,
        string _Ciudad,
        int _Hotel,
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
            Hotel = _Hotel;
            CantHabitaciones = _CantHabitaciones;
            CantPersonas = _CantPersonas;
            Entrada = _Entrada;
            Salida = _Salida;
            Estatus = _Estatus;
            idCheckIn = _idCheckIn;
            idCheckOut = _idCheckOut;
        }
        public Reservacion() { }
    }

}