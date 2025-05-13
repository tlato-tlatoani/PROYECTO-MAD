/*
Autor: Alejandro Villarreal

LMAD

PARA EL PROYECTO ES OBLIGATORIO EL USO DE ESTA CLASE, 
EN EL SENTIDO DE QUE LOS DATOS DE CONEXION AL SERVIDOR ESTAN DEFINIDOS EN EL App.Config
Y NO TENER ESOS DATOS EN CODIGO DURO DEL PROYECTO.

NO SE PERMITE HARDCODE.

LOS MÉTODOS QUE SE DEFINEN EN ESTA CLASE SON EJEMPLOS, PARA QUE SE BASEN Y USTEDES HAGAN LOS SUYOS PROPIOS
Y DEFINAN Y PROGRAMEN TODOS LOS MÉTODOS QUE SEAN NECESARIOS PARA SU PROYECTO.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using PROYECTO_MAD.Entities;

/*
Se tiene que cambiar el namespace para el que usen en su proyecto
*/
namespace PROYECTO_MAD
{
    public class EnlaceDB
    {
        static private string _aux { set; get; }
        static private SqlConnection _conexion;
        static private SqlDataAdapter _adaptador = new SqlDataAdapter();
        static private SqlCommand _comandosql = new SqlCommand();
        static private DataTable _tabla = new DataTable();
        static private DataSet _DS = new DataSet();

        public DataTable obtenertabla
        {
            get
            {
                return _tabla;
            }
        }

        private static void conectar()
        {
            /*
			Para que funcione el ConfigurationManager
			en la sección de "Referencias" de su proyecto, en el "Solution Explorer"
			dar clic al botón derecho del mouse y dar clic a "Add Reference"
			Luego elegir la opción System.Configuration
			
			tal como lo vimos en clase.
			*/
            string cnn = ConfigurationManager.ConnectionStrings["Grupo03"].ToString(); 
			// Cambiar Grupo01 por el que ustedes hayan definido en el App.Config
            _conexion = new SqlConnection(cnn);
            _conexion.Open();
        }
        private static void desconectar()
        {
            _conexion.Close();
        }

        public Usuario Autentificar(string us, string ps, bool _tipousuario)
        {
            Usuario l_usuario = null;
            try {
                conectar();
                string qry = "Validar";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@email", SqlDbType.NVarChar, 40);
                parametro1.Value = us;
                var parametro2 = _comandosql.Parameters.Add("@contrasenna", SqlDbType.NVarChar, 20);
                parametro2.Value = ps;
                var parametro3 = _comandosql.Parameters.Add("@tipousuario", SqlDbType.Bit);
                parametro3.Value = _tipousuario;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(_tabla);

                if (_tabla.Rows.Count > 0) {
                    l_usuario = new Usuario();
                    l_usuario.NoNomina = int.Parse(_tabla.Rows[0].ItemArray[0].ToString());
                    l_usuario.TipoUsuario = (bool) _tabla.Rows[0].ItemArray[1];
                }
            }
            catch(SqlException e)
            {
            }
            finally
            {
                desconectar();
            }

            return l_usuario;
        }

        public bool Registrar(Usuario _usuario, bool _isAdmin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "Registrar";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro1.Value = _usuario.NoNomina;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.NChar, 50);
                parametro2.Value = _usuario.Nombre;
                var parametro3 = _comandosql.Parameters.Add("@ApellidoPaterno", SqlDbType.NChar, 50);
                parametro3.Value = _usuario.ApellidoPaterno;
                var parametro4 = _comandosql.Parameters.Add("@ApellidoMaterno", SqlDbType.NChar, 50);
                parametro4.Value = _usuario.ApellidoMaterno;
                var parametro5 = _comandosql.Parameters.Add("@CorreoElectronico", SqlDbType.NChar, 40);
                parametro5.Value = _usuario.CorreoElectronico;
                var parametro6 = _comandosql.Parameters.Add("@Contrasenna", SqlDbType.NChar, 20);
                parametro6.Value = _usuario.RealContrasenna;
                var parametro7 = _comandosql.Parameters.Add("@TelCelular", SqlDbType.NChar, 10);
                parametro7.Value = _usuario.TelCelular;
                var parametro8 = _comandosql.Parameters.Add("@TelCasa", SqlDbType.NChar, 10);
                parametro8.Value = _usuario.TelCasa;
                var parametro9 = _comandosql.Parameters.Add("@FechaNacimiento", SqlDbType.Date);
                parametro9.Value = _usuario.FechaNacimiento;
                var parametro10 = _comandosql.Parameters.Add("@TipoUsuario", SqlDbType.Bit);
                parametro10.Value = _usuario.TipoUsuario;
                var parametro11 = _comandosql.Parameters.Add("@EsAdmin", SqlDbType.Bit);
                parametro11.Value = _isAdmin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }

        public bool RegistrarCliente(EntClientes _cliente, int _admin) {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "RegistrarCliente";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@RFC", SqlDbType.NVarChar, 15);
                parametro1.Value = _cliente.RFC;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.NChar, 50);
                parametro2.Value = _cliente.Nombre;
                var parametro3 = _comandosql.Parameters.Add("@ApellidoPaterno", SqlDbType.NChar, 50);
                parametro3.Value = _cliente.ApellidoPaterno;
                var parametro4 = _comandosql.Parameters.Add("@ApellidoMaterno", SqlDbType.NChar, 50);
                parametro4.Value = _cliente.ApellidoMaterno;
                var parametro5 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NChar, 30);
                parametro5.Value = _cliente.Ciudad;
                var parametro6 = _comandosql.Parameters.Add("@Estado", SqlDbType.NChar, 30);
                parametro6.Value = _cliente.Estado;
                var parametro7 = _comandosql.Parameters.Add("@Pais", SqlDbType.NChar, 30);
                parametro7.Value = _cliente.Pais;
                var parametro8 = _comandosql.Parameters.Add("@CorreoElectronico", SqlDbType.NChar, 40);
                parametro8.Value = _cliente.CorreoElectronico;
                var parametro9 = _comandosql.Parameters.Add("@TelCelular", SqlDbType.NVarChar, 10);
                parametro9.Value = _cliente.TelCelular;
                var parametro10 = _comandosql.Parameters.Add("@TelCasa", SqlDbType.NVarChar, 10);
                parametro10.Value = _cliente.TelCasa;
                var parametro11 = _comandosql.Parameters.Add("@FechaNacimiento", SqlDbType.Date);
                parametro11.Value = _cliente.FechaNacimiento;
                var parametro12 = _comandosql.Parameters.Add("@EstadoCivil", SqlDbType.NVarChar, 10);
                parametro12.Value = _cliente.EstadoCivil;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool RegistrarHotel(Hotel _hotel, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "RegistrarHoteles";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@NombreHotel", SqlDbType.NVarChar, 100);
                parametro2.Value = _hotel.NombreHotel;
                var parametro3 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 30);
                parametro3.Value = _hotel.Ciudad;
                var parametro4 = _comandosql.Parameters.Add("@Estado", SqlDbType.NVarChar, 30);
                parametro4.Value = _hotel.Estado;
                var parametro5 = _comandosql.Parameters.Add("@Pais", SqlDbType.NVarChar, 30);
                parametro5.Value = _hotel.Pais;
                var parametro6 = _comandosql.Parameters.Add("@ZonaTuristica", SqlDbType.Bit);
                parametro6.Value = _hotel.ZonaTuristica;
                var parametro7 = _comandosql.Parameters.Add("@Locacion", SqlDbType.NVarChar, 60);
                parametro7.Value = _hotel.Locacion;
                var parametro8 = _comandosql.Parameters.Add("@NoPisos", SqlDbType.Int);
                parametro8.Value = _hotel.NoPisos;
                var parametro9 = _comandosql.Parameters.Add("@FechaInicio", SqlDbType.Date);
                parametro9.Value = _hotel.FechaInicio;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool RegistrarTipoHabitacion(TipoHab _tipoHab, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "RegistrarTipoHabitacion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@NivelHabitacion", SqlDbType.NVarChar, 20);
                parametro2.Value = _tipoHab.NivelHabitacion;
                var parametro3 = _comandosql.Parameters.Add("@NoCamas", SqlDbType.Int);
                parametro3.Value = _tipoHab.NoCamas;
                var parametro4 = _comandosql.Parameters.Add("@TipoCama", SqlDbType.NVarChar, 300);
                parametro4.Value = _tipoHab.TipoCama;
                var parametro5 = _comandosql.Parameters.Add("@PrecioNoche", SqlDbType.Money);
                parametro5.Value = _tipoHab.PrecioNoche;
                var parametro6 = _comandosql.Parameters.Add("@CantPersonasMax", SqlDbType.Int);
                parametro6.Value = _tipoHab.CantPersonasMax;
                var parametro7 = _comandosql.Parameters.Add("@Locacion", SqlDbType.NVarChar, 60);
                parametro7.Value = _tipoHab.Locacion;
                var parametro8 = _comandosql.Parameters.Add("@Amenidades", SqlDbType.NVarChar, 100);
                parametro8.Value = _tipoHab.Amenidades;
                var parametro9 = _comandosql.Parameters.Add("@NombreHotel", SqlDbType.NVarChar, 100);
                parametro9.Value = _tipoHab.nombreHotel;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool Editar(Usuario _usuario) {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "Editar";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro1.Value = _usuario.NoNomina;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50);
                parametro2.Value = _usuario.Nombre;
                var parametro3 = _comandosql.Parameters.Add("@ApellidoPaterno", SqlDbType.NVarChar, 50);
                parametro3.Value = _usuario.ApellidoPaterno;
                var parametro4 = _comandosql.Parameters.Add("@ApellidoMaterno", SqlDbType.NVarChar, 50);
                parametro4.Value = _usuario.ApellidoMaterno;
                var parametro5 = _comandosql.Parameters.Add("@CorreoElectronico", SqlDbType.NVarChar, 40);
                parametro5.Value = _usuario.CorreoElectronico;
                var parametro6 = _comandosql.Parameters.Add("@Contrasenna", SqlDbType.NVarChar, 20);
                parametro6.Value = _usuario.RealContrasenna;
                var parametro7 = _comandosql.Parameters.Add("@TelCelular", SqlDbType.NVarChar, 10);
                parametro7.Value = _usuario.TelCelular;
                var parametro8 = _comandosql.Parameters.Add("@TelCasa", SqlDbType.NVarChar, 10);
                parametro8.Value = _usuario.TelCasa;
                var parametro9 = _comandosql.Parameters.Add("@FechaNacimiento", SqlDbType.Date);
                parametro9.Value = _usuario.FechaNacimiento;
                var parametro10 = _comandosql.Parameters.Add("@TipoUsuario", SqlDbType.Bit);
                parametro10.Value = _usuario.TipoUsuario;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool EditarCliente(EntClientes _cliente, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "EditarCliente";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@RFC", SqlDbType.NVarChar, 15);
                parametro1.Value = _cliente.RFC;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.NChar, 50);
                parametro2.Value = _cliente.Nombre;
                var parametro3 = _comandosql.Parameters.Add("@ApellidoPaterno", SqlDbType.NChar, 50);
                parametro3.Value = _cliente.ApellidoPaterno;
                var parametro4 = _comandosql.Parameters.Add("@ApellidoMaterno", SqlDbType.NChar, 50);
                parametro4.Value = _cliente.ApellidoMaterno;
                var parametro5 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NChar, 30);
                parametro5.Value = _cliente.Ciudad;
                var parametro6 = _comandosql.Parameters.Add("@Estado", SqlDbType.NChar, 30);
                parametro6.Value = _cliente.Estado;
                var parametro7 = _comandosql.Parameters.Add("@Pais", SqlDbType.NChar, 30);
                parametro7.Value = _cliente.Pais;
                var parametro8 = _comandosql.Parameters.Add("@CorreoElectronico", SqlDbType.NChar, 40);
                parametro8.Value = _cliente.CorreoElectronico;
                var parametro9 = _comandosql.Parameters.Add("@TelCelular", SqlDbType.NVarChar, 10);
                parametro9.Value = _cliente.TelCelular;
                var parametro10 = _comandosql.Parameters.Add("@TelCasa", SqlDbType.NVarChar, 10);
                parametro10.Value = _cliente.TelCasa;
                var parametro11 = _comandosql.Parameters.Add("@FechaNacimiento", SqlDbType.Date);
                parametro11.Value = _cliente.FechaNacimiento;
                var parametro12 = _comandosql.Parameters.Add("@EstadoCivil", SqlDbType.NVarChar, 10);
                parametro12.Value = _cliente.EstadoCivil;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool EditarHotel(Hotel _hotel, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "EditarHotel";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@NombreHotel", SqlDbType.NVarChar, 100);
                parametro2.Value = _hotel.NombreHotel;
                var parametro3 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 30);
                parametro3.Value = _hotel.Ciudad;
                var parametro4 = _comandosql.Parameters.Add("@Estado", SqlDbType.NVarChar, 30);
                parametro4.Value = _hotel.Estado;
                var parametro5 = _comandosql.Parameters.Add("@Pais", SqlDbType.NVarChar, 30);
                parametro5.Value = _hotel.Pais;
                var parametro6 = _comandosql.Parameters.Add("@ZonaTuristica", SqlDbType.Bit);
                parametro6.Value = _hotel.ZonaTuristica;
                var parametro7 = _comandosql.Parameters.Add("@Locacion", SqlDbType.NVarChar, 60);
                parametro7.Value = _hotel.Locacion;
                var parametro8 = _comandosql.Parameters.Add("@NoPisos", SqlDbType.Int);
                parametro8.Value = _hotel.NoPisos;
                var parametro9 = _comandosql.Parameters.Add("@FechaInicio", SqlDbType.Date);
                parametro9.Value = _hotel.FechaInicio;
                var parametro12 = _comandosql.Parameters.Add("@CodHotel", SqlDbType.Int);
                parametro12.Value = _hotel.CodHotel;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool EditarTipoHabitacion(TipoHab _tipoHab, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "EditarTipoHabitacion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@NivelHabitacion", SqlDbType.NVarChar, 20);
                parametro2.Value = _tipoHab.NivelHabitacion;
                var parametro3 = _comandosql.Parameters.Add("@NoCamas", SqlDbType.Int);
                parametro3.Value = _tipoHab.NoCamas;
                var parametro4 = _comandosql.Parameters.Add("@TipoCama", SqlDbType.NVarChar, 300);
                parametro4.Value = _tipoHab.TipoCama;
                var parametro5 = _comandosql.Parameters.Add("@PrecioNoche", SqlDbType.Money);
                parametro5.Value = _tipoHab.PrecioNoche;
                var parametro6 = _comandosql.Parameters.Add("@CantPersonasMax", SqlDbType.Int);
                parametro6.Value = _tipoHab.CantPersonasMax;
                var parametro7 = _comandosql.Parameters.Add("@Locacion", SqlDbType.NVarChar, 60);
                parametro7.Value = _tipoHab.Locacion;
                var parametro8 = _comandosql.Parameters.Add("@Amenidades", SqlDbType.NVarChar, 100);
                parametro8.Value = _tipoHab.Amenidades;
                var parametro9 = _comandosql.Parameters.Add("@NombreHotel", SqlDbType.NVarChar, 100);
                parametro9.Value = _tipoHab.nombreHotel;
                var parametro10 = _comandosql.Parameters.Add("@CodTDH", SqlDbType.Int);
                parametro10.Value = _tipoHab.CodTDH;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool RegistrarHabitacion(EntHabitacion _habitacion, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "RegistrarHabitacion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@NoHabitacion", SqlDbType.Int);
                parametro2.Value = _habitacion.NoHabitacion;
                var parametro3 = _comandosql.Parameters.Add("@Estatus", SqlDbType.NVarChar, 50);
                parametro3.Value = _habitacion.Estatus;
                var parametro4 = _comandosql.Parameters.Add("@Piso", SqlDbType.Int);
                parametro4.Value = _habitacion.Piso;
                var parametro5 = _comandosql.Parameters.Add("@TipoHabitacion", SqlDbType.NVarChar, 20);
                parametro5.Value = _habitacion.TipoHabitacionNombre;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool EditarHabitacion(EntHabitacion _habitacion, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "EditarHabitacion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@NoHabitacion", SqlDbType.Int);
                parametro2.Value = _habitacion.NoHabitacion;
                var parametro3 = _comandosql.Parameters.Add("@Estatus", SqlDbType.NVarChar, 50);
                parametro3.Value = _habitacion.Estatus;
                var parametro4 = _comandosql.Parameters.Add("@Piso", SqlDbType.Int);
                parametro4.Value = _habitacion.Piso;
                var parametro5 = _comandosql.Parameters.Add("@TipoHabitacion", SqlDbType.NVarChar, 20);
                parametro5.Value = _habitacion.TipoHabitacionNombre;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool RegistrarServicio(EntServicios _servicio, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "RegistrarServicio";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@CodServicio", SqlDbType.Int);
                parametro2.Value = _servicio.CodServicio;
                var parametro3 = _comandosql.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50);
                parametro3.Value = _servicio.Nombre;
                var parametro4 = _comandosql.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 200);
                parametro4.Value = _servicio.Descripcion;
                var parametro5 = _comandosql.Parameters.Add("@Precio", SqlDbType.Money);
                parametro5.Value = _servicio.Precio;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }
        public bool EditarServicio(EntServicios _servicio, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "EditarServicio";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@CodServicio", SqlDbType.Int);
                parametro2.Value = _servicio.CodServicio;
                var parametro3 = _comandosql.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50);
                parametro3.Value = _servicio.Nombre;
                var parametro4 = _comandosql.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 200);
                parametro4.Value = _servicio.Descripcion;
                var parametro5 = _comandosql.Parameters.Add("@Precio", SqlDbType.Money);
                parametro5.Value = _servicio.Precio;
                var parametro13 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro13.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                isValid = true;
            }
            catch (SqlException e)
            {
                isValid = false;
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return isValid;
        }

        public List<Usuario> getUsuarios() {
            var msg = "";
            DataTable tabla = new DataTable();

            try {
                conectar();

                string qry = "GetUsuarios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<Usuario> l_usuarios = new List<Usuario>();
                foreach (DataRow _row in tabla.Rows) {
                    l_usuarios.Add(new Usuario(
                        int.Parse(_row["NoNomina"].ToString()),
                        _row["Nombre"].ToString(),
                        _row["ApellidoPaterno"].ToString(),
                        _row["ApellidoMaterno"].ToString(),
                        _row["CorreoElectronico"].ToString(),
                        "1",
                        _row["TelCelular"].ToString(),
                        _row["TelCasa"].ToString(),
                        DateTime.Parse(_row["FechaNacimiento"].ToString()),
                        (bool) _row["TipoUsuario"]
                    ));

                    l_usuarios[l_usuarios.Count() - 1].Contrasenna = int.Parse(_row["Contrasenna"].ToString());
                    l_usuarios[l_usuarios.Count() - 1].Estado = (bool) _row["Estado"];
                }

                return l_usuarios;
            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return new List<Usuario>();
        }

        // Ejemplo de método para recibir una consulta en forma de tabla
        // Cuando el SP ejecutará un SELECT

        public List<EntClientes> getClientes() {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetClientes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<EntClientes> l_clientes = new List<EntClientes>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_clientes.Add(new EntClientes(
                        _row["RFC"].ToString(),
                        _row["Nombre"].ToString(),
                        _row["ApellidoPaterno"].ToString(),
                        _row["ApellidoMaterno"].ToString(),
                        _row["Ciudad"].ToString(),
                        _row["Estado"].ToString(),
                        _row["Pais"].ToString(),
                        _row["CorreoElectronico"].ToString(),
                        _row["TelCelular"].ToString(),
                        _row["TelCasa"].ToString(),
                        DateTime.Parse(_row["FechaNacimiento"].ToString()),
                        _row["EstadoCivil"].ToString()
                    ));
                }

                return l_clientes;
            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return new List<EntClientes>();
        }
        public List<Hotel> getHoteles()
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetHoteles";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<Hotel> l_hoteles = new List<Hotel>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_hoteles.Add(new Hotel(
                        int.Parse(_row["CodHotel"].ToString()),
                        _row["NombreHotel"].ToString(),
                        _row["Ciudad"].ToString(),
                        _row["Estado"].ToString(),
                        _row["Pais"].ToString(),
                        (bool)_row["ZonaTuristica"],
                        _row["Locacion"].ToString(),
                        int.Parse(_row["NoPisos"].ToString()),
                        DateTime.Parse(_row["FechaInicio"].ToString())
                    ));
                }

                return l_hoteles;
            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return new List<Hotel>();
        }
        public List<TipoHab> getTiposHabitaciones()
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetTiposHabitaciones";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<TipoHab> l_tipoHabs = new List<TipoHab>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_tipoHabs.Add(new TipoHab(
                        int.Parse(_row["CodTDH"].ToString()),
                        _row["NivelHabitacion"].ToString(),
                        int.Parse(_row["NoCamas"].ToString()),
                        _row["TipoCama"].ToString(),
                        int.Parse(_row["CantPersonasMax"].ToString()),
                        _row["Locacion"].ToString(),
                        _row["Amenidades"].ToString(),
                        _row["NombreHotel"].ToString(),
                        decimal.Parse(_row["PrecioNoche"].ToString())
                    ));
                }

                return l_tipoHabs;
            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return new List<TipoHab>();
        }
        public List<EntHabitacion> getHabitaciones()
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetHabitaciones";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<EntHabitacion> l_Habs = new List<EntHabitacion>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_Habs.Add(new EntHabitacion(
                        int.Parse(_row["NoHabitacion"].ToString()),
                        _row["Estatus"].ToString(),
                        int.Parse(_row["Piso"].ToString()),
                        _row["NivelHabitacion"].ToString(),
                        _row["NombreHotel"].ToString()
                    ));
                }

                return l_Habs;
            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return new List<EntHabitacion>();
        }
        public List<EntServicios> getServicios()
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetServicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<EntServicios> l_Habs = new List<EntServicios>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_Habs.Add(new EntServicios(
                        int.Parse(_row["CodServicio"].ToString()),
                        _row["Nombre"].ToString(),
                        decimal.Parse(_row["Precio"].ToString()),
                        _row["Descripcion"].ToString()
                    ));
                }

                return l_Habs;
            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return new List<EntServicios>();
        }
        // Ejemplo de método para ejecutar un SP que no se espera que regrese información, 
        // solo que ejecute ya sea un INSERT, UPDATE o DELETE
        public bool Add_Deptos(string opc, string depto)
        {
            var msg = "";
            var add = true;
            try
            {
                conectar();
                string qry = "sp_Gestiona_Deptos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Opc", SqlDbType.Char, 1);
                parametro1.Value = opc;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.VarChar, 20);
                parametro2.Value = depto;

                _adaptador.InsertCommand = _comandosql;
				// También se tienen las propiedades del adaptador: UpdateCommand  y DeleteCommand
                
                _comandosql.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                add = false;
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();                
            }

            return add;
        }

    }
}
