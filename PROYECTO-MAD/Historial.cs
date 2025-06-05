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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROYECTO_MAD
{
    public partial class Historial : Form
    {
        public List<EntClientes> m_clientes;

        public Historial()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void verHotelesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hoteles hotelesform = new Hoteles();
            hotelesform.Show();
            this.Close();
        }

        private void habitacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Habitaciones habitacionform = new Habitaciones();
            habitacionform.Show();
            this.Close();
        }

        private void tiposDeHabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void verServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicios serviciosform = new Servicios();
            serviciosform.Show();
            this.Close();
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

        private void Historial_Load(object sender, EventArgs e)
        {
            m_clientes = new EnlaceDB().getClientes();
            foreach (EntClientes _cliente in m_clientes) { listBox1.Items.Add(_cliente.Nombre.Trim() + ":" + _cliente.ApellidoPaterno.Trim() + ":" + _cliente.ApellidoMaterno.Trim()); }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntClientes l_cliente = null;
            foreach (EntClientes _cliente in m_clientes) { 
                if (!_cliente.Nombre.Trim().Equals(listBox1.SelectedItem.ToString().Split(':')[0])) { continue; }
                l_cliente = _cliente;
                break;
            }

            if (l_cliente == null) { return; }

            textBox2.Text = l_cliente.ApellidoPaterno;
            textBox1.Text = l_cliente.ApellidoMaterno;

            DataTable l_informacion = new EnlaceDB().getHistorialCliente(l_cliente.RFC);
            dataGridView2.DataSource = l_informacion;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_clientes = new EnlaceDB().getClientesAp(textBox1.Text, textBox2.Text);

            listBox1.Items.Clear();
            foreach (EntClientes _cliente in m_clientes) { listBox1.Items.Add(_cliente.Nombre.Trim() + ":" + _cliente.ApellidoPaterno.Trim() + ":" + _cliente.ApellidoMaterno.Trim()); }
        }

        private void verReservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrarCliente identificateform = new FiltrarCliente();
            identificateform.Show();
            this.Close();
        }
    }
}
