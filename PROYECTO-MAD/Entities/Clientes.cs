using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_MAD.Entities
{
    public class Clientes
    {
        public string RFC;
        public string Nombre;
        public string ApellidoPaterno;
        public string ApellidoMaterno;
        public string Ciudad;
        public string Estado;
        public string Pais;
        public string CorreoElectronico;
        public string TelCelular;
        public string TelCasa;
        public DateTime FechaNacimiento;
        public string EstadoCivil;

        public Clientes(
        string _RFC,
        string _Nombre,
        string _ApellidoPaterno,
        string _ApellidoMaterno,
        string _Ciudad,
        string _Estado,
        string _Pais,
        string _CorreoElectronico,
        string _TelCelular,
        string _TelCasa,
        DateTime _FechaNacimiento,
        string _EstadoCivil
       ) {
            RFC = _RFC;
            Nombre = _Nombre;
            ApellidoPaterno = _ApellidoPaterno;
            ApellidoMaterno = _ApellidoMaterno;
            Ciudad = _Ciudad;
            Estado = _Estado;
            Pais = _Pais;
            CorreoElectronico = _CorreoElectronico;
            TelCelular = _TelCelular;
            TelCasa = _TelCasa;
            FechaNacimiento = _FechaNacimiento;
            EstadoCivil = _EstadoCivil;

        }
        public Clientes() { }
    }
}
