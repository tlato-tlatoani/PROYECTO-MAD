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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROYECTO_MAD
{
    public partial class Reservacion : Form
    {
        public bool m_registrando = false;
        public bool m_editando = false;
        public int m_actual = 0;

        public static string m_cliente = null;
        public int m_habitaciones = -1;

        public Reservacion()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Factura facturaform = new Factura();
            facturaform.Show();
            this.Close();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Reservacion_Load(object sender, EventArgs e)
        {
            foreach (string _ciudad in (new EnlaceDB()).getCiudades())
            {
                comboBox2.Items.Add(_ciudad);
            }
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

        private void verEditarRegistrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            foreach (Hotel _hotel in (new EnlaceDB()).getHotelesCiudad(comboBox2.Text))
            {
                comboBox3.Items.Add(_hotel.NombreHotel);
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Hotel l_curHotel = new EnlaceDB().getHotel(comboBox3.Text);
            listBox1.Items.Add("Domicilio: " + l_curHotel.Locacion);
            listBox1.Items.Add("Numero de Pisos: " + l_curHotel.NoPisos);
            listBox1.Items.Add("Servicios: ");

            dataGridView2.Rows.Clear();
            foreach (TipoHab _tipohab in (new EnlaceDB()).getTiposHabitacionesHotel(comboBox3.Text)) {
                dataGridView2.Rows.Add();
                DataGridViewRow l_row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                l_row.Cells["Nombre"].Value = _tipohab.NivelHabitacion;
                l_row.Cells["Camas"].Value = _tipohab.NoCamas;
                l_row.Cells["Clientes"].Value = _tipohab.CantPersonasMax;
                l_row.Cells["Precio"].Value = _tipohab.PrecioNoche;
                l_row.Cells["Habitaciones"].Value = _tipohab.Habitaciones;
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            m_habitaciones = int.Parse(dataGridView2.SelectedCells[4].Value.ToString());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) { return; }
            int l_curValue = int.Parse(textBox1.Text);

            if (l_curValue > m_habitaciones) { textBox1.Text = m_habitaciones.ToString(); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) { return; }
            if (textBox2.Text.Length <= 0) { return; }
            int l_curHabits = int.Parse(textBox1.Text);
            int l_curPersons = int.Parse(textBox2.Text);
            int l_clientesHab = int.Parse(dataGridView2.SelectedCells[2].Value.ToString());

            if (l_curPersons > l_curHabits * l_clientesHab) { textBox2.Text = (l_curHabits * l_clientesHab).ToString(); }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!m_registrando)
            {
                m_registrando = true;
                m_editando = false;
                m_actual = -1;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Crearas una Reservacion.", "Informacion");
            }
            else
            {
                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar esta Reservacion?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    EntReservacion l_reservacion = new EntReservacion(
                        0,
                        m_cliente,
                        comboBox2.Text,
                        comboBox3.Text,
                        int.Parse(textBox1.Text),
                        int.Parse(textBox2.Text),
                        dateTimePicker1.Value,
                        dateTimePicker2.Value,
                        "Vigente",
                        0,
                        0
                    );

                    //EnlaceDB l_enlace = new EnlaceDB();
                    //if (l_enlace.RegistrarReservacion(l_hotel, Program.m_usuario.NoNomina))
                    //{
                    //    MessageBox.Show(this, "Hotel Registrado con Exito.", "Informacion");
                    //    Form3_Load(this, new EventArgs());
                    //    m_registrando = false;
                    //}
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 checkoutform = new Form2();
            checkoutform.Show();
            this.Close();
        }
    }
}
