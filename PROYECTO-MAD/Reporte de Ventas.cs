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
    public partial class Reporte_de_Ventas : Form
    {
        public List<Hotel> m_hoteles;
        public Hotel m_curHotel;

        public Reporte_de_Ventas()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void verEditarRegistrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void Reporte_de_Ventas_Load(object sender, EventArgs e)
        {
            DataTable l_tabla = new EnlaceDB().getVentas("", 2025, "", "");
            m_hoteles = new EnlaceDB().getHoteles();

            foreach (Hotel _hotel in m_hoteles) { listBox1.Items.Add(_hotel.NombreHotel); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable l_tabla = new EnlaceDB().getVentas(textBox1.Text, int.Parse(textBox2.Text), textBox3.Text, listBox1.Text);
            dataGridView1.DataSource = l_tabla;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_curHotel = m_hoteles[listBox1.SelectedIndex];
            if (m_curHotel == null) { return; }

            textBox1.Text = m_curHotel.Pais;
            textBox2.Text = DateTime.Now.Year.ToString();
            textBox3.Text = m_curHotel.Ciudad;

            DataTable l_tabla = new EnlaceDB().getVentas(textBox1.Text, int.Parse(textBox2.Text), textBox3.Text, listBox1.Text);
            dataGridView1.DataSource = l_tabla;
        }

        private void verReservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrarCliente identificateform = new FiltrarCliente();
            identificateform.Show();
            this.Close();
        }
    }
}
