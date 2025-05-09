using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class Habitacion
    {
        public int NoHabitacion;
        public string Estatus;
        public int Piso;
        public int TipoHabitacion;

        public Habitacion (
        int     _NoHabitacion,
        string  _Estatus,
        int     _Piso,
        int     _TipoHabitacion

        ){
        
            NoHabitacion = _NoHabitacion;
            Estatus = _Estatus;
            Piso = _Piso;
            TipoHabitacion= _TipoHabitacion;
        
        }
        public Habitacion() { }

    }
}
