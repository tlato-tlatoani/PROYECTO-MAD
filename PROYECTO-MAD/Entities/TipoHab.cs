using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class TipoHab
    {
        public int CodTDH;
        public string NivelHabitacion;
        public int NoCamas;
        public string TipoCama;
        public int CantPersonasMax;
        public string Locacion;
        public string Amenidades;
        public int idHotel;
        public decimal PrecioNoche;

        public string nombreHotel;
        public int Habitaciones;

        public TipoHab(

        int _CodTDH,
        string _NivelHabitacion,
        int _NoCamas,
        string _TipoCama,
        int _CantPersonasMax,
        string _Locacion,
        string _Amenidades,
        string _idHotel,
        decimal _PrecioNoche
        )
        {


            CodTDH = _CodTDH;
            NivelHabitacion = _NivelHabitacion;
            NoCamas = _NoCamas;
            TipoCama = _TipoCama;
            CantPersonasMax = _CantPersonasMax;
            Locacion = _Locacion;
            Amenidades = _Amenidades;
            nombreHotel = _idHotel;
            PrecioNoche = _PrecioNoche;

        }


    }
}