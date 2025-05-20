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
    public partial class Hoteles : Form
    {
        public List<EntServicios> m_servicios;
        public List<Hotel> m_hoteles;

        public bool m_registrando = false;
        public bool m_editando = false;
        public int m_actual = 0;
        public Hoteles()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            m_servicios = new EnlaceDB().getServicios();
            m_hoteles = new EnlaceDB().getHoteles();

            dataGridView1.Rows.Clear();
            foreach (Hotel _hotel in m_hoteles) {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["Codigo"].Value = _hotel.CodHotel;
                l_row.Cells["Nombre"].Value = _hotel.NombreHotel;
            }

            listBox2.Items.Clear();
            foreach (EntServicios _serv in m_servicios) {
                listBox2.Items.Add(_serv.Nombre);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void hotelesToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void verEditarRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!m_registrando)
            {
                m_registrando = true;
                m_editando = false;
                m_actual = -1;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Registraras un Hotel.", "Informacion");
            }
            else
            {
                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar este Hotel?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    string l_listServicios = "";
                    foreach (string _serv in listBox2.SelectedItems) { l_listServicios += _serv + ","; }
                    if (l_listServicios.Length > 0) { l_listServicios = l_listServicios.Remove(l_listServicios.Length - 1); }

                    Hotel l_hotel = new Hotel(
                        0,
                        textBox9.Text,
                        textBox1.Text,
                        textBox4.Text,
                        textBox5.Text,
                        radioButton1.Checked,
                        textBox3.Text,
                        int.Parse(textBox6.Text),
                        l_listServicios,
                        dateTimePicker1.Value
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.RegistrarHotel(l_hotel, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Hotel Registrado con Exito.", "Informacion");
                        Form3_Load(this, new EventArgs());
                        m_registrando = false;
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Hotel l_hotel = null;

            foreach (Hotel _hotel in m_hoteles)
            {
                if (!dataGridView1.SelectedCells[0].Value.ToString().Equals(_hotel.CodHotel.ToString())) { continue; }
                l_hotel = _hotel;

                break;
            }

            if (l_hotel == null) { return; }

            m_actual = l_hotel.CodHotel;

            if (m_registrando)
            {
                m_registrando = false;
                textBox1.Text = "";

                MessageBox.Show(this, "Has salido del Modo Registro", "Informacion");
            }
            if (m_editando)
            {
                m_editando = false;

                MessageBox.Show(this, "Has salido del Modo Edicion", "Informacion");
            }

            textBox9.Text = l_hotel.NombreHotel;
            textBox6.Text = l_hotel.NoPisos.ToString();
            textBox1.Text = l_hotel.Ciudad;
            textBox4.Text = l_hotel.Estado;
            textBox5.Text = l_hotel.Pais;
            textBox3.Text = l_hotel.Locacion;
            textBox7.Text = l_hotel.CodHotel.ToString();
            radioButton1.Checked = l_hotel.ZonaTuristica;
            radioButton2.Checked = !l_hotel.ZonaTuristica;
            dateTimePicker1.Value = l_hotel.FechaInicio;

            listBox2.ClearSelected();
            foreach (string _serv in l_hotel.Servicios.Split(',')) {
                listBox2.SelectedItems.Add(_serv);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!m_editando)
            {
                if (m_actual < 0)
                {
                    MessageBox.Show(this, "Necesitas Seleccionar un Hotel para Realizar esta Accion.", "Advertencia");
                    return;
                }

                m_editando = true;

                MessageBox.Show(this, "Estas en Modo Edicion.\nSi Presionas este Boton de Nuevo Editaras el Hotel Seleccionado.", "Informacion");
            }
            else
            {
                DialogResult l_editar = MessageBox.Show(this, "Quieres Editar este Hotel?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    string l_listServicios = "";
                    foreach(string _serv in listBox2.SelectedItems) { l_listServicios += _serv + ","; }
                    if (l_listServicios.Length > 0) { l_listServicios = l_listServicios.Remove(l_listServicios.Length - 1); }

                    Hotel l_hotel = new Hotel(
                        int.Parse(textBox7.Text),
                        textBox9.Text,
                        textBox1.Text,
                        textBox4.Text,
                        textBox5.Text,
                        radioButton1.Checked,
                        textBox3.Text,
                        int.Parse(textBox6.Text),
                        l_listServicios,
                        dateTimePicker1.Value
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.EditarHotel(l_hotel, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Cliente Editado con Exito.", "Informacion");
                        Form3_Load(this, new EventArgs());
                        m_editando = false;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l_busqueda = textBox2.Text;
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow _row in dataGridView1.Rows)
            {
                if (!_row.Cells["Codigo"].Value.ToString().TrimEnd().Equals(l_busqueda)) { continue; }
                _row.Selected = true;
                dataGridView1_Click(dataGridView1, new DataGridViewCellEventArgs(0, 0));
                return;
            }
        }

        private void label11_Click_1(object sender, EventArgs e)
        {

        }
    }
}
