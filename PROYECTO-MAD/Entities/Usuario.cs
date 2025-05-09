using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class Usuario
    {
        public int NoNomina;
        public string Nombre;
        public string ApellidoPaterno;
        public string ApellidoMaterno;
        public string CorreoElectronico;
        public int Contrasenna;
        public string TelCelular;
        public string TelCasa;
        public DateTime FechaNacimiento;
	    public bool TipoUsuario;
        public bool Estado;

        public string RealContrasenna;

        public Usuario(
            int _NoNomina, 
            string _Nombre, 
            string _ApellidoPaterno, 
            string _ApellidoMaterno, 
            string _CorreoElectronico, 
            string _Contrasenna,
            string _TelCelular,
            string _TelCasa,
            DateTime _FechaNacimiento,
            bool _TipoUsuario
        ) { 
            NoNomina = _NoNomina;
            Nombre = _Nombre;
            ApellidoPaterno = _ApellidoPaterno;
            ApellidoMaterno = _ApellidoMaterno;
            CorreoElectronico = _CorreoElectronico;
            RealContrasenna = _Contrasenna;
            TelCelular = _TelCelular;
            TelCasa = _TelCasa;
            FechaNacimiento = _FechaNacimiento;
            TipoUsuario = _TipoUsuario;
        }
    }
}
