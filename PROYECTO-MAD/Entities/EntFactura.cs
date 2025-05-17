using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class EntFactura
    {
        public int NoFactura;
        public decimal PrecioInicial;
        public decimal PrecioServicios;
        public decimal PrecioTotal;
        public string NombreDescuento;
        public decimal Descuento;
        public decimal Anticipo;
        public DateTime FechaCreacion;
        public string FormaPago;

        public EntFactura(
            int _NoFactura,
            decimal _PrecioInicial,
            decimal _PrecioServicios,
            decimal _PrecioTotal,
            string _NombreDescuento,
            decimal _Descuento,
            decimal _Anticipo,
            DateTime _FechaCreacion,
            string _FormaPago
        ) {
            NoFactura = _NoFactura;
            PrecioInicial = _PrecioInicial;
            PrecioServicios = _PrecioServicios;
            PrecioTotal = _PrecioTotal;
            NombreDescuento = _NombreDescuento;
            Descuento = _Descuento;
            Anticipo = _Anticipo;
            FechaCreacion = _FechaCreacion;
            FormaPago = _FormaPago;
    }

        public EntFactura() { }
    }
}
