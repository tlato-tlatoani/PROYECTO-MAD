using PROYECTO_MAD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PROYECTO_MAD
{
    public partial class Perfil : Form
    {
        public Perfil()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void miPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Perfil usuariosform = new Perfil();
            usuariosform.Show();
            this.Close();
        }

        private void verEditarRegistrarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void verHotelesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Hoteles hotelesform = new Hoteles();
            hotelesform.Show();
            this.Close();
        }

        private void habitacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Habitaciones habitacionform = new Habitaciones();
            habitacionform.Show();
            this.Close();
        }

        private void tiposDeHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            TIPO_DE_HAB tiposhabitacionform = new TIPO_DE_HAB();
            tiposhabitacionform.Show();
            this.Close();
        }

        private void verServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Servicios serviciosform = new Servicios();
            serviciosform.Show();
            this.Close();
        }

        private void verClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clientes clientesform = new Clientes();
            clientesform.Show();
            this.Close();

        }

        private void verHistorialDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Historial historialform = new Historial();
            historialform.Show();
            this.Close();
        }

        private void reporteDeOcupaciónPorHotelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Reporte_de_ocupación_por_hotel reportehotelform = new Reporte_de_ocupación_por_hotel();
            reportehotelform.Show();
            this.Close();
        }

        private void reporteDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Reporte_de_Ventas reporteventasform = new Reporte_de_Ventas();
            reporteventasform.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            InicioDeSesion.m_instance.Show();
        }

        private void Perfil_Load(object sender, EventArgs e)
        {
            textBox2.Text = Program.m_usuario.CorreoElectronico.Trim();
            textBox3.Text = Program.m_usuario.Nombre.Trim();
            textBox7.Text = Program.m_usuario.TelCasa.Trim();
            textBox8.Text = Program.m_usuario.TelCelular.Trim();
            textBox4.Text = Program.m_usuario.ApellidoPaterno.Trim();
            textBox5.Text = Program.m_usuario.ApellidoMaterno.Trim();
            dateTimePicker1.Value = Program.m_usuario.FechaNacimiento;
            textBox6.Text = Program.m_usuario.NoNomina.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Correo Electronico.", "Validacion"); return; }
            if (textBox3.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Nombre.", "Validacion"); return; }
            if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono de Casa.", "Validacion"); return; }
            if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Paterno.", "Validacion"); return; }
            if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Materno.", "Validacion"); return; }
            if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono Celular.", "Validacion"); return; }

            if (dateTimePicker1.Value.Year < DateTime.Now.Year - 100 || dateTimePicker1.Value.Year > DateTime.Now.Year) { MessageBox.Show(this, "Debe Colocar una Fecha de Nacimiento Valida.", "Validacion"); return; }
            if (dateTimePicker1.Value.Year > DateTime.Now.Year - 17) { MessageBox.Show(this, "Debe ser Mayor de Edad.", "Validacion"); return; }

            if (MessageBox.Show(this, "Quieres Editar tu Usuario?", "Informacion", MessageBoxButtons.YesNo) != DialogResult.Yes) { return; }

            Usuario l_usuario = new Usuario(
                Program.m_usuario.NoNomina,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox2.Text,
                "",
                textBox8.Text,
                textBox7.Text,
                dateTimePicker1.Value,
                Program.m_usuario.TipoUsuario
            );

            EnlaceDB l_enlace = new EnlaceDB();
            if (l_enlace.Editar(l_usuario)) {
                MessageBox.Show(this, "Usuario Editado con Exito.", "Informacion");
                Program.m_usuario = l_usuario;
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar una Contraseña.", "Validacion"); return; }

            if (!Regex.IsMatch(textBox1.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$")) {
                MessageBox.Show(this, "La contraseña debe tener minimo 8 caracteres, 1 caracter especial, 1 minúscula y 1 mayúsucula", "Formato Incorrecto");
                return;
            }

            EnlaceDB l_enlace = new EnlaceDB();
            if (l_enlace.ActualizarContra(Program.m_usuario.NoNomina, textBox1.Text)) {
                MessageBox.Show(this, "Contraseña Actualizada con Exito.", "Informacion");
                Program.m_usuario.RealContrasenna = textBox1.Text;
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void verReservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrarCliente identificateform = new FiltrarCliente();
            identificateform.Show();
            this.Close();
        }

    }
}
