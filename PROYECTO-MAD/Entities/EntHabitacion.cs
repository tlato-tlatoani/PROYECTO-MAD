using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class EntHabitacion
    {
        public int NoHabitacion;
        public string Estatus;
        public int Piso;
        public int TipoHabitacion;

        public string TipoHabitacionNombre;
        public string NombreHotel;

        public EntHabitacion (
        int     _NoHabitacion,
        string  _Estatus,
        int     _Piso,
        string     _TipoHabitacion,
        string _NombreHotel

        )
        {
        
            NoHabitacion = _NoHabitacion;
            Estatus = _Estatus;
            Piso = _Piso;
            TipoHabitacionNombre = _TipoHabitacion;
            NombreHotel = _NombreHotel;

        }
        public EntHabitacion() { }

    }
}
