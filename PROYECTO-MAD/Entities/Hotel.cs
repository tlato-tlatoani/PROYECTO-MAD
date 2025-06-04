using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class Hotel
    {
        public int CodHotel;
        public string NombreHotel;
        public string Ciudad;
        public string Estado;
        public string Pais;
        public bool ZonaTuristica;
        public string Locacion;
        public int NoPisos;
        public DateTime FechaInicio;

        public string Servicios;

        public Hotel(
            
            int    _CodHotel, 
            string _NombreHotel, 
            string _Ciudad, 
            string _Estado, 
            string _Pais, 
            bool _ZonaTuristica, 
            string _Locacion,
            int    _NoPisos,
            DateTime _FechaInicio

            )
        {
            CodHotel = _CodHotel;
            NombreHotel = _NombreHotel;
            Ciudad = _Ciudad;
            Estado = _Estado;
            Pais = _Pais;
            ZonaTuristica = _ZonaTuristica;
            Locacion = _Locacion;
            NoPisos = _NoPisos;
            FechaInicio = _FechaInicio;
        }

        public Hotel() { }
    }
}
