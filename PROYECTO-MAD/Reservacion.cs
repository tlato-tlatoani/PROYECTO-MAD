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
        public static Reservacion m_instance;

        public List<EntReservacion> m_reservaciones;
        public EntReservacion m_reservacionActual;

        public bool m_registrando = false;
        public bool m_editando = false;
        public static Guid m_actual = Guid.Empty;

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
            m_instance = this;

            m_reservaciones = new EnlaceDB().getReservaciones(m_cliente);

            foreach (string _ciudad in (new EnlaceDB()).getCiudades()) {
                comboBox2.Items.Add(_ciudad);
            }

            dataGridView3.Rows.Clear();
            foreach (EntReservacion _reservacion in m_reservaciones) {
                dataGridView3.Rows.Add();
                DataGridViewRow l_row = dataGridView3.Rows[dataGridView3.Rows.Count - 1];
                l_row.Cells["Codigo"].Value = _reservacion.CodReservacion;
                l_row.Cells["Ciudad"].Value = _reservacion.Ciudad;
                l_row.Cells["Hotel"].Value = _reservacion.HotelNombre;
                l_row.Cells["Estatus"].Value = _reservacion.Estatus;
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
            foreach (string _serv in l_curHotel.Servicios.Split(',')) {
                listBox1.Items.Add(_serv);
            }

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
            button6.Enabled = true;
            button7.Enabled = true;
            dateTimePicker3.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) { return; }
            int l_curValue = 0;  int.TryParse(textBox1.Text, out l_curValue);

            if (l_curValue > m_habitaciones) { textBox1.Text = m_habitaciones.ToString(); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) { return; }
            if (textBox2.Text.Length <= 0) { return; }
            if (dataGridView2.Rows.Count < 1) { return; }
            if (dataGridView2.ColumnCount < 1) { return; }
            if (dataGridView2.SelectedCells.Count < 1) { return; }
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
                m_actual = Guid.Empty;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Crearas una Reservacion.", "Informacion");
            }
            else
            {
                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar esta Reservacion?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    EntReservacion l_reservacion = new EntReservacion(
                        Guid.Empty,
                        m_cliente,
                        comboBox2.Text,
                        comboBox3.Text,
                        dataGridView2.SelectedRows[0].Cells[0].Value.ToString(),
                        int.Parse(textBox1.Text),
                        int.Parse(textBox2.Text),
                        dateTimePicker1.Value,
                        dateTimePicker2.Value,
                        "Vigente",
                        0,
                        0
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.RegistrarReservacion(l_reservacion, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Reservacion Creada con Exito.", "Informacion");
                        Reservacion_Load(this, new EventArgs());
                        m_registrando = false;
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 checkoutform = new Form2();
            checkoutform.Show();
            this.Close();
        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            EntReservacion l_reservacion = null;

            foreach (EntReservacion _reservacion in m_reservaciones) {
                if (!dataGridView3.SelectedCells[0].Value.ToString().Equals(_reservacion.CodReservacion.ToString())) { continue; }
                l_reservacion = _reservacion;

                break;
            }

            if (l_reservacion == null) { return; }

            m_actual = l_reservacion.CodReservacion;
            m_reservacionActual = l_reservacion;

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

            comboBox2.Text = l_reservacion.Ciudad;
            comboBox3.Text = l_reservacion.HotelNombre;
            dateTimePicker1.Value = l_reservacion.Entrada;
            dateTimePicker2.Value = l_reservacion.Salida;
 
            dataGridView2.ClearSelection();
            foreach (DataGridViewRow _row in dataGridView2.Rows)
            {
                if (!_row.Cells["Nombre"].Value.ToString().TrimEnd().Equals(l_reservacion.TipoHabitacion)) { continue; }
                _row.Selected = true;
                dataGridView2_Click(dataGridView2, new DataGridViewCellEventArgs(0, 0));
                break;
            }

            textBox1.Text = l_reservacion.CantHabitaciones.ToString();
            textBox2.Text = l_reservacion.CantPersonas.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (m_actual == Guid.Empty) { MessageBox.Show(this, "Debes elegir una Reservacion para Realizar esta Accion.", "Error"); return; }

            if ((m_reservacionActual.Entrada - DateTime.Now).TotalDays < 3)
            {
                MessageBox.Show(this, "Ya no se encuentra en días hábiles para realizar su cancelación.", "Advertencia");
                return;
            }

            DialogResult l_editar = MessageBox.Show(this, "Quieres Cancelar esta Reservacion?", "Advertencia", MessageBoxButtons.YesNo);
            if (l_editar == DialogResult.Yes) {
                new EnlaceDB().CancelarReservacion(m_actual, Program.m_usuario.NoNomina);
                MessageBox.Show(this, "Reservacion Cancelada.", "Informacion");
                Reservacion_Load(this, new EventArgs());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (m_actual == Guid.Empty) { MessageBox.Show(this, "Debes elegir una Reservacion para Realizar esta Accion.", "Error"); return; }

            DialogResult l_editar = MessageBox.Show(this, "Quieres realizar el Check In de Esta Reservacion?", "Advertencia", MessageBoxButtons.YesNo);
            if (l_editar == DialogResult.Yes)
            {
                string l_checks = new EnlaceDB().CheckIn(m_actual, dateTimePicker3.Value, Program.m_usuario.NoNomina);
                MessageBox.Show(this, "Check-In Realizada!\n\nCodigo de Reservacion: " + m_actual.ToString() + "\nHabitaciones Ocupadas:\n"+ l_checks, "Informacion");
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Form2 l_checkout = new Form2();
            l_checkout.ShowDialog(this);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
