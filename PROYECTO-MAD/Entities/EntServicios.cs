using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class EntServicios
    {
        public int CodServicio;
        public string Nombre;
        public decimal Precio;
        public string Descripcion;
        public int Hotel;

        public EntServicios(
            int _CodServicio,
            string _Nombre,
            decimal _Precio,
            string _Descripcion,
            int _Hotel
        ) {
            CodServicio = _CodServicio;
            Nombre = _Nombre;
            Precio = _Precio;
            Descripcion = _Descripcion;
            Hotel = _Hotel;
        }
        public EntServicios() { }
    }
}
