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

namespace PROYECTO_MAD
{
    public partial class Reporte_de_ocupación_por_hotel : Form
    {
        public List<Hotel> m_hoteles;
        public Hotel m_curHotel;
        public Reporte_de_ocupación_por_hotel()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            DataTable l_tabla1 = new EnlaceDB().getOcupaciones1(textBox1.Text, int.Parse(textBox2.Text), textBox3.Text, listBox1.Text);
            DataTable l_tabla2 = new EnlaceDB().getOcupaciones2(textBox1.Text, int.Parse(textBox2.Text), textBox3.Text, listBox1.Text);

            dataGridView1.Rows.Clear();
            foreach (DataRow _row in l_tabla1.Rows) {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];

                l_row.Cells["Ciudad"].Value = _row["Ciudad"].ToString();
                l_row.Cells["Hotel"].Value = _row["NombreHotel"].ToString();
                l_row.Cells["Year"].Value = _row["Anio"].ToString();
                l_row.Cells["Mes"].Value = _row["Mes"].ToString();
                l_row.Cells["TipoHabitacion"].Value = _row["NivelHabitacion"].ToString();
                l_row.Cells["Cantidad"].Value = _row["Habitaciones"].ToString();
                l_row.Cells["Porcentaje"].Value = _row["Porcentaje"].ToString();
                l_row.Cells["Personas"].Value = _row["Personas"].ToString();
            }

            dataGridView2.Rows.Clear();
            foreach (DataRow _row in l_tabla2.Rows)
            {
                dataGridView2.Rows.Add();
                DataGridViewRow l_row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];

                l_row.Cells["Ciudad2"].Value = _row["Ciudad"].ToString();
                l_row.Cells["Hotel2"].Value = _row["NombreHotel"].ToString();
                l_row.Cells["Year2"].Value = _row["Anio"].ToString();
                l_row.Cells["Mes2"].Value = _row["Mes"].ToString();
                l_row.Cells["Porcentaje2"].Value = _row["Porcentaje"].ToString();
            }
        }

        private void Reporte_de_ocupación_por_hotel_Load(object sender, EventArgs e)
        {
            DataTable l_tabla = new EnlaceDB().getVentas("", 2025, "", "");
            m_hoteles = new EnlaceDB().getHoteles();

            foreach (Hotel _hotel in m_hoteles) { listBox1.Items.Add(_hotel.NombreHotel); }
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_curHotel = m_hoteles[listBox1.SelectedIndex];
            if (m_curHotel == null) { return; }

            textBox1.Text = m_curHotel.Pais;
            textBox2.Text = DateTime.Now.Year.ToString();
            textBox3.Text = m_curHotel.Ciudad;

            DataTable l_tabla1 = new EnlaceDB().getOcupaciones1(m_curHotel.Pais, DateTime.Now.Year, m_curHotel.Ciudad, m_curHotel.NombreHotel);
            DataTable l_tabla2 = new EnlaceDB().getOcupaciones2(m_curHotel.Pais, DateTime.Now.Year, m_curHotel.Ciudad, m_curHotel.NombreHotel);

            dataGridView1.Rows.Clear();
            foreach (DataRow _row in l_tabla1.Rows)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];

                l_row.Cells["Ciudad"].Value = _row["Ciudad"].ToString();
                l_row.Cells["Hotel"].Value = _row["NombreHotel"].ToString();
                l_row.Cells["Year"].Value = _row["Anio"].ToString();
                l_row.Cells["Mes"].Value = _row["Mes"].ToString();
                l_row.Cells["TipoHabitacion"].Value = _row["NivelHabitacion"].ToString();
                l_row.Cells["Cantidad"].Value = _row["Habitaciones"].ToString();
                l_row.Cells["Porcentaje"].Value = _row["Porcentaje"].ToString();
                l_row.Cells["Personas"].Value = _row["Personas"].ToString();
            }

            dataGridView2.Rows.Clear();
            foreach (DataRow _row in l_tabla2.Rows)
            {
                dataGridView2.Rows.Add();
                DataGridViewRow l_row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];

                l_row.Cells["Ciudad2"].Value = _row["Ciudad"].ToString();
                l_row.Cells["Hotel2"].Value = _row["NombreHotel"].ToString();
                l_row.Cells["Year2"].Value = _row["Anio"].ToString();
                l_row.Cells["Mes2"].Value = _row["Mes"].ToString();
                l_row.Cells["Porcentaje2"].Value = _row["Porcentaje"].ToString();
            }
        }
    }
}
