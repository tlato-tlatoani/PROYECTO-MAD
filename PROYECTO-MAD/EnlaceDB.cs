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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                    foreach (DataRow _row in _tabla.Rows)
                    {
                        l_usuario = new Usuario(
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
                        );

                        l_usuario.Contrasenna = int.Parse(_row["Contrasenna"].ToString());
                        l_usuario.Estado = (bool) _row["Estado"];
                    }
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
                var parametro10 = _comandosql.Parameters.Add("@Servicios", SqlDbType.NVarChar, 1000);
                parametro10.Value = _hotel.Servicios;
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
                var parametro14 = _comandosql.Parameters.Add("@Servicios", SqlDbType.NVarChar, 1000);
                parametro14.Value = _hotel.Servicios;
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
        public bool RegistrarReservacion(EntReservacion _reservacion, int _admin)
        {
            bool isValid = false;
            try
            {
                conectar();
                string qry = "RegistrarReservacion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@Cliente", SqlDbType.NVarChar, 15);
                parametro2.Value = _reservacion.Cliente;
                var parametro3 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 30);
                parametro3.Value = _reservacion.Ciudad;
                var parametro4 = _comandosql.Parameters.Add("@Hotel", SqlDbType.NVarChar, 100);
                parametro4.Value = _reservacion.HotelNombre;
                var parametro11 = _comandosql.Parameters.Add("@TipoHabitacion", SqlDbType.NVarChar, 20);
                parametro11.Value = _reservacion.TipoHabitacionNombre;
                var parametro5 = _comandosql.Parameters.Add("@CantHabitaciones", SqlDbType.Int);
                parametro5.Value = _reservacion.CantHabitaciones;
                var parametro6 = _comandosql.Parameters.Add("@CantPersonas", SqlDbType.Int);
                parametro6.Value = _reservacion.CantPersonas;
                var parametro7 = _comandosql.Parameters.Add("@Entrada", SqlDbType.Date);
                parametro7.Value = _reservacion.Entrada;
                var parametro8 = _comandosql.Parameters.Add("@Salida", SqlDbType.Date);
                parametro8.Value = _reservacion.Salida;
                var parametro9 = _comandosql.Parameters.Add("@Estatus", SqlDbType.NVarChar, 11);
                parametro9.Value = _reservacion.Estatus;
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

        public void CancelarReservacion(Guid _codigo, int _admin)
        {
            try
            {
                conectar();
                string qry = "CancelarReservacion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro2 = _comandosql.Parameters.Add("@Codigo", SqlDbType.UniqueIdentifier);
                parametro2.Value = _codigo;
                var parametro3 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro3.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }
        }
        public void CheckIn(Guid _codigo, DateTime _fecha, int _admin) {
            try
            {
                conectar();
                string qry = "CheckIn";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@Reservacion", SqlDbType.UniqueIdentifier);
                parametro1.Value = _codigo;
                var parametro2 = _comandosql.Parameters.Add("@Fecha", SqlDbType.Date);
                parametro2.Value = _fecha;
                var parametro3 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro3.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }
        }
        public void CheckOut(Guid _codigo, DateTime _fecha, decimal _descuento, string _nombredescuento, int _admin)
        {
            try
            {
                conectar();
                string qry = "CheckOut";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@Reservacion", SqlDbType.UniqueIdentifier);
                parametro1.Value = _codigo;
                var parametro2 = _comandosql.Parameters.Add("@Fecha", SqlDbType.Date);
                parametro2.Value = _fecha;
                var parametro5 = _comandosql.Parameters.Add("@Descuento", SqlDbType.Money);
                parametro5.Value = _descuento;
                var parametro6 = _comandosql.Parameters.Add("@NombreDescuento", SqlDbType.NVarChar, 50);
                parametro6.Value = _nombredescuento;
                var parametro3 = _comandosql.Parameters.Add("@NoNomina", SqlDbType.Int);
                parametro3.Value = _admin;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }
        }
        public void FacturarServicios(Guid _codigo, string _servicios)
        {
            try
            {
                conectar();
                string qry = "FacturarServicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@Reservacion", SqlDbType.UniqueIdentifier);
                parametro1.Value = _codigo;
                var parametro6 = _comandosql.Parameters.Add("@Lista", SqlDbType.NVarChar, 1000);
                parametro6.Value = _servicios;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                string msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }
        }
        public EntFactura GetFactura(Guid _reservacion)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetFactura";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;
                
                var parametro1 = _comandosql.Parameters.Add("@Reservacion", SqlDbType.UniqueIdentifier);
                parametro1.Value = _reservacion;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<EntFactura> l_usuarios = new List<EntFactura>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_usuarios.Add(new EntFactura(
                        int.Parse(_row["NoFactura"].ToString()),
                        decimal.Parse(_row["PrecioInicial"].ToString()),
                        decimal.Parse(_row["PrecioServicios"].ToString()),
                        decimal.Parse(_row["PrecioTotal"].ToString()),
                        _row["NombreDescuento"].ToString(),
                        decimal.Parse(_row["Descuento"].ToString()),
                        decimal.Parse(_row["Anticipo"].ToString()),
                        DateTime.Parse(_row["FechaCreacion"].ToString()),
                        _row["FormaPago"].ToString()
                    ));
                }

                desconectar();
                return l_usuarios[0];
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

            return null;
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
        public List<EntClientes> getClientesAp(string _apPat, string _apMat)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetClientesAp";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@ApPat", SqlDbType.NVarChar, 50);
                parametro1.Value = _apPat;
                var parametro2 = _comandosql.Parameters.Add("@ApMat", SqlDbType.NVarChar, 50);
                parametro2.Value = _apMat;

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
        public DataTable getVentas(string _pais, int _anio, string _ciudad, string _hotel)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetVentas";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Pais", SqlDbType.NVarChar, 100);
                parametro1.Value = _pais;
                var parametro2 = _comandosql.Parameters.Add("@Year", SqlDbType.Int);
                parametro2.Value = _anio;
                var parametro31 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 100);
                parametro31.Value = _ciudad;
                var parametro15 = _comandosql.Parameters.Add("@Hotel", SqlDbType.NVarChar, 100);
                parametro15.Value = _hotel;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                desconectar();
                return tabla;
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

            return null;
        }
        public DataTable getOcupaciones1(string _pais, int _anio, string _ciudad, string _hotel)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetOcupaciones";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Pais", SqlDbType.NVarChar, 100);
                parametro1.Value = _pais;
                var parametro2 = _comandosql.Parameters.Add("@Year", SqlDbType.Int);
                parametro2.Value = _anio;
                var parametro31 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 100);
                parametro31.Value = _ciudad;
                var parametro15 = _comandosql.Parameters.Add("@Hotel", SqlDbType.NVarChar, 100);
                parametro15.Value = _hotel;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                desconectar();
                return tabla;
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

            return null;
        }
        public DataTable getOcupaciones2(string _pais, int _anio, string _ciudad, string _hotel)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetOcupaciones2";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Pais", SqlDbType.NVarChar, 100);
                parametro1.Value = _pais;
                var parametro2 = _comandosql.Parameters.Add("@Year", SqlDbType.Int);
                parametro2.Value = _anio;
                var parametro31 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 100);
                parametro31.Value = _ciudad;
                var parametro15 = _comandosql.Parameters.Add("@Hotel", SqlDbType.NVarChar, 100);
                parametro15.Value = _hotel;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                desconectar();
                return tabla;
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

            return null;
        }
        public EntClientes getCliente(string _code)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetCliente";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@RFC", SqlDbType.NVarChar, 15);
                parametro1.Value = _code;

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

                return l_clientes[0];
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

            return null;
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
                        _row["Servicios"].ToString(),
                        DateTime.Parse(_row["FechaInicio"].ToString())
                    ));
                }

                desconectar();
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
        public List<Hotel> getHotelesCiudad(string _ciudad)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetHotelesCiudad";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Ciudad", SqlDbType.NVarChar, 30);
                parametro1.Value = _ciudad;

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
                        "",
                        DateTime.Parse(_row["FechaInicio"].ToString())
                    ));
                }

                desconectar();
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
        public Hotel getHotel(string _nombre)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetHotelNombre";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100);
                parametro1.Value = _nombre;

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
                        _row["Servicios"].ToString(),
                        DateTime.Parse(_row["FechaInicio"].ToString())
                    ));
                }

                desconectar();
                return l_hoteles[0];
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

            return null;
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
        public List<TipoHab> getTiposHabitacionesHotel(string _hotel)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetTiposHabitacionHotel";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Hotel", SqlDbType.NVarChar, 100);
                parametro1.Value = _hotel;

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
                        "",
                        _row["Amenidades"].ToString(),
                        "",
                        decimal.Parse(_row["PrecioNoche"].ToString())
                    ));

                    l_tipoHabs[l_tipoHabs.Count - 1].Habitaciones = int.Parse(_row["Habitaciones"].ToString());
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
        public List<EntServicios> GetFacturaServicios(Guid _codigo)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetFacturaServicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Codigo", SqlDbType.UniqueIdentifier);
                parametro1.Value = _codigo;

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
        public List<string> getCiudades()
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetCiudades";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<string> l_Habs = new List<string>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_Habs.Add(_row["Ciudad"].ToString());
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

            return new List<string>();
        }

        public List<EntReservacion> getReservaciones(string _rfc)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetReservaciones";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@RFC", SqlDbType.NVarChar, 15);
                parametro1.Value = _rfc;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<EntReservacion> l_Habs = new List<EntReservacion>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_Habs.Add(new EntReservacion(
                        Guid.Parse(_row["CodReservacion"].ToString()),
                        _row["Cliente"].ToString(),
                        _row["Ciudad"].ToString(),
                        _row["NombreHotel"].ToString(),
                        _row["NivelHabitacion"].ToString(),
                        int.Parse(_row["CantHabitaciones"].ToString()),
                        int.Parse(_row["CantPersonas"].ToString()),
                        DateTime.Parse(_row["Entrada"].ToString()),
                        DateTime.Parse(_row["Salida"].ToString()),
                        _row["Estatus"].ToString(),
                        _row["idCheckIn"] != DBNull.Value ? int.Parse(_row["idCheckIn"].ToString()) : 0,
                        _row["idCheckOut"] != DBNull.Value ? int.Parse(_row["idCheckOut"].ToString()) : 0
                    ));

                    l_Habs[l_Habs.Count - 1].Hotel = int.Parse(_row["Hotel"].ToString());

                    l_Habs[l_Habs.Count - 1].Anticipo = decimal.Parse(_row["Anticipo"].ToString());
                    l_Habs[l_Habs.Count - 1].Monto = decimal.Parse(_row["PrecioInicial"].ToString());
                    l_Habs[l_Habs.Count - 1].Servicios = decimal.Parse(_row["PrecioServicios"].ToString());
                    l_Habs[l_Habs.Count - 1].Total = decimal.Parse(_row["PrecioTotal"].ToString());
                }

                desconectar();
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

            return new List<EntReservacion>();
        }
        public EntReservacion getReservacion(Guid _rfc)
        {
            var msg = "";
            DataTable tabla = new DataTable();

            try
            {
                conectar();

                string qry = "GetReservacion";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Codigo", SqlDbType.UniqueIdentifier);
                parametro1.Value = _rfc;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                List<EntReservacion> l_Habs = new List<EntReservacion>();
                foreach (DataRow _row in tabla.Rows)
                {
                    l_Habs.Add(new EntReservacion(
                        Guid.Parse(_row["CodReservacion"].ToString()),
                        _row["Cliente"].ToString(),
                        _row["Ciudad"].ToString(),
                        _row["NombreHotel"].ToString(),
                        _row["NivelHabitacion"].ToString(),
                        int.Parse(_row["CantHabitaciones"].ToString()),
                        int.Parse(_row["CantPersonas"].ToString()),
                        DateTime.Parse(_row["Entrada"].ToString()),
                        DateTime.Parse(_row["Salida"].ToString()),
                        _row["Estatus"].ToString(),
                        _row["idCheckIn"] != DBNull.Value ? int.Parse(_row["idCheckIn"].ToString()) : 0,
                        _row["idCheckOut"] != DBNull.Value ? int.Parse(_row["idCheckOut"].ToString()) : 0
                    ));

                    l_Habs[l_Habs.Count - 1].Hotel = int.Parse(_row["Hotel"].ToString());
                    l_Habs[l_Habs.Count - 1].Dias = int.Parse(_row["Dias"].ToString());
                    l_Habs[l_Habs.Count - 1].PrecioNoche = decimal.Parse(_row["PrecioNoche"].ToString());
                    l_Habs[l_Habs.Count - 1].NoHabitacion = int.Parse(_row["NoHabitacion"].ToString());

                    l_Habs[l_Habs.Count - 1].Anticipo = decimal.Parse(_row["Anticipo"].ToString());
                    l_Habs[l_Habs.Count - 1].Monto = decimal.Parse(_row["PrecioInicial"].ToString());
                    l_Habs[l_Habs.Count - 1].Servicios = decimal.Parse(_row["PrecioServicios"].ToString());
                    l_Habs[l_Habs.Count - 1].Total = decimal.Parse(_row["PrecioTotal"].ToString());
                }

                desconectar();
                return l_Habs[0];
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

            return null;
        }

        public string BuscarClientePorCorreo(string _correo)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            string l_return = null;

            try
            {
                conectar();

                string qry = "BuscarCliente";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@CorreoElectronico", SqlDbType.NVarChar, 40);
                parametro1.Value = _correo;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                foreach (DataRow _row in tabla.Rows)
                {
                    l_return = _row["RFC"].ToString();
                }
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

            return l_return;
        }
        public string BuscarClientePorRFC(string _rfc)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            string l_return = null;

            try
            {
                conectar();

                string qry = "BuscarClienteRFC";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@RFC", SqlDbType.NVarChar, 15);
                parametro1.Value = _rfc;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                foreach (DataRow _row in tabla.Rows)
                {
                    l_return = _row["RFC"].ToString();
                }
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

            return l_return;
        }
        public string BuscarClientePorApellidos(string _ap1, string _ap2)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            string l_return = null;

            try
            {
                conectar();

                string qry = "BuscarClienteAp";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@ApellidoPaterno", SqlDbType.NVarChar, 50);
                parametro1.Value = _ap1;
                var parametro2 = _comandosql.Parameters.Add("@ApellidoMaterno", SqlDbType.NVarChar, 50);
                parametro2.Value = _ap2;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

                foreach (DataRow _row in tabla.Rows)
                {
                    l_return = _row["RFC"].ToString();
                }
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

            return l_return;
        }

    }
}
