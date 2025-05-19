using PROYECTO_MAD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void verEditarRegistrarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void reservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 identificateform = new Form1();
            identificateform.Show();
            this.Close();
        }

        private void verHotelesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hoteles hotelesform = new Hoteles();
            hotelesform.Show();
            this.Close();
        }

        private void habitacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Habitacion habitacionform = new Habitacion();
            habitacionform.Show();
            this.Close();
        }

        private void tiposDeHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TIPO_DE_HAB tiposhabitacionform = new TIPO_DE_HAB();
            tiposhabitacionform.Show();
            this.Close();
        }

        private void verServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Historial historialform = new Historial();
            historialform.Show();
            this.Close();
        }

        private void reporteDeOcupaciónPorHotelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte_de_ocupación_por_hotel reportehotelform = new Reporte_de_ocupación_por_hotel();
            reportehotelform.Show();
            this.Close();
        }

        private void reporteDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            textBox2.Text = Program.m_usuario.CorreoElectronico;
            textBox3.Text = Program.m_usuario.Nombre;
            textBox7.Text = Program.m_usuario.TelCasa;
            textBox8.Text = Program.m_usuario.TelCelular;
            textBox4.Text = Program.m_usuario.ApellidoPaterno;
            textBox5.Text = Program.m_usuario.ApellidoMaterno;
            dateTimePicker1.Value = Program.m_usuario.FechaNacimiento;
            textBox6.Text = Program.m_usuario.NoNomina.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Quieres Editar tu Usuario?", "Informacion", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            Usuario l_usuario = new Usuario(
                Program.m_usuario.NoNomina,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox2.Text,
                textBox1.Text.Length > 0 ? textBox1.Text : Program.m_usuario.RealContrasenna,
                textBox8.Text,
                textBox7.Text,
                dateTimePicker1.Value,
                Program.m_usuario.TipoUsuario
            );

            EnlaceDB l_enlace = new EnlaceDB();
            if (l_enlace.Editar(l_usuario))
            {
                MessageBox.Show(this, "Usuario Editado con Exito.", "Informacion");
                Program.m_usuario = l_usuario;
            }
        }
    }
}
