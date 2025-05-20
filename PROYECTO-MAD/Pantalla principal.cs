using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_MAD
{
    public partial class Pantalla_principal : Form
    {
        public Pantalla_principal()
        {
            InitializeComponent();
        }

        //private void CambiarFormulario(Form nuevoForm, bool cerrarAppAlSalir = false)
        //{
        //    this.Hide();

        //    if (cerrarAppAlSalir)
        //        nuevoForm.FormClosed += (s, args) => Application.Exit();
        //    else
        //        nuevoForm.FormClosed += (s, args) => this.Close();

        //    nuevoForm.Show();
        //}

        private void reservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }
            //CambiarFormulario(new Reservacion(), true);
            Form1 identificateform = new Form1();
            identificateform.Show();
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

            Habitacion habitacionform = new Habitacion();
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

        private void Pantalla_principal_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void hotelesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Servicios serviciosform = new Servicios();
            serviciosform.Show();
            this.Close();
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
    }
}
