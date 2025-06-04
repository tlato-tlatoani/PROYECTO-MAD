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

        public bool m_registrando = false;
        public bool m_editando = false;
        public static Guid m_actual = Guid.Empty;

        public static EntReservacion m_reservacionActual;
        public static EntClientes m_cliente = null;
        public static Hotel m_hotel = null;

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
            if (m_reservacionActual == null) { MessageBox.Show(this, "Debes Elegir una Reservacion antes de Realizar esta Accion"); return; }
            if (m_reservacionActual.CheckOut == null) { MessageBox.Show(this, "Se deben haber Realizado todos los Checks para entrar a esta Ventana."); return; }

            Factura facturaform = new Factura();
            facturaform.Show();
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

            m_reservaciones = new EnlaceDB().getReservaciones(m_cliente.RFC);

            comboBox2.Items.Clear();
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

            textBox3.Text = m_cliente.Nombre.Trim() + " " + m_cliente.ApellidoPaterno.Trim() + " " + m_cliente.ApellidoMaterno.Trim();
        }

        private void reservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrarCliente identificateform = new FiltrarCliente();
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

        private void verEditarRegistrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

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
            m_hotel = l_curHotel;
            listBox1.Items.Add("Domicilio: " + l_curHotel.Locacion);
            listBox1.Items.Add("Numero de Pisos: " + l_curHotel.NoPisos);
            listBox1.Items.Add("Servicios: ");
            foreach (string _serv in l_curHotel.Servicios.Split(',')) {
                listBox1.Items.Add(_serv);
            }

            dataGridView2.Rows.Clear();
            foreach (EntHabitacion _hab in (new EnlaceDB()).getHabitacionesHotel(comboBox3.Text)) {
                if(_hab.Estatus != "Desocupado") { continue; }

                dataGridView2.Rows.Add();
                DataGridViewRow l_row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                l_row.Cells["Tipo"].Value = _hab.TipoHabitacionNombre;
                l_row.Cells["Numero"].Value = _hab.NoHabitacion;
                l_row.Cells["Camas"].Value = _hab.Camas;
                l_row.Cells["Clientes"].Value = _hab.Clientes;
                l_row.Cells["Precio"].Value = _hab.Precio;
                l_row.Cells["Id"].Value = _hab.Codigo;
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!m_registrando)
            {
                m_registrando = true;
                m_editando = false;
                m_actual = Guid.Empty;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Crearas una Reservacion.", "Informacion");

                dataGridView2.Rows.Clear();
            }
            else
            {
                if (comboBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Elegir una Ciudad", "Validacion"); return; }
                if (comboBox3.Text.Length <= 0) { MessageBox.Show(this, "Debe Elegir un Hotel", "Validacion"); return; }
                if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Agregar un Anticipo", "Validacion"); return; }

                decimal l_anticipo = -1;
                if (!decimal.TryParse(textBox4.Text, out l_anticipo)) { MessageBox.Show(this, "El Anticipo debe ser un Valor Numerico Decimal Valido.", "Validacion"); return; }
                if (l_anticipo < 0) { MessageBox.Show(this, "El Anticipo no puede ser Negativo.", "Validacion"); return; }

                if ((dateTimePicker2.Value - dateTimePicker1.Value).Days < 1) { MessageBox.Show(this, "El Cliente debe Hospedarse por lo Menos un Dia para realizar la Reservacion.", "Validacion"); return; }
                if (dateTimePicker1.Value < DateTime.Now) { MessageBox.Show(this, "No se puede crear una Reservacion para un dia que ya transcurrio.", "Validacion"); return; }

                bool l_usaHabitaciones = false;
                foreach (DataGridViewRow _row in dataGridView2.Rows) { 
                    if (_row.Cells["Hospedaje"].Value == null || _row.Cells["Hospedaje"].Value.ToString().Length <= 0) { continue; }
                    l_usaHabitaciones = true;

                    int l_personas = -1;
                    if (!int.TryParse(_row.Cells["Hospedaje"].Value.ToString(), out l_personas)) { MessageBox.Show(this, "Debe Colocar un Numero Entero Valido en el Hospedaje de Habitacion.", "Validacion"); return; }
                    if (l_personas <= 0) { MessageBox.Show(this, "Una Habitacion no puede hospedar un numero menor de 0 huespedes.", "Validacion"); return; }
                    if (l_personas > int.Parse(_row.Cells["Clientes"].Value.ToString())) { MessageBox.Show(this, "El Numero de Huespedes Excede el Maximo de la Habitacion.", "Validacion"); return; }
                }

                if (!l_usaHabitaciones) { MessageBox.Show(this, "El Cliente debe Reservar minimo una Habitacion.", "Validacion"); return; }

                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar esta Reservacion?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    EntReservacion l_reservacion = new EntReservacion(
                        Guid.Empty,
                        m_cliente.RFC,
                        comboBox2.Text,
                        comboBox3.Text,
                        dateTimePicker1.Value,
                        dateTimePicker2.Value,
                        "Vigente",
                        l_anticipo
                    );

                    Guid l_resCodigo;
                    EnlaceDB l_enlace = new EnlaceDB();
                    if ((l_resCodigo = l_enlace.RegistrarReservacion(l_reservacion, Program.m_usuario.NoNomina)) != Guid.Empty)
                    {
                        foreach (DataGridViewRow _row in dataGridView2.Rows) {
                            if (_row.Cells["Hospedaje"] == null) { continue; }

                            new EnlaceDB().ReservarHabitacion(l_resCodigo, int.Parse(_row.Cells["Id"].Value.ToString()), int.Parse(_row.Cells["Hospedaje"].Value.ToString()));
                        }

                        MessageBox.Show(this, "Reservacion Creada con Exito.", "Informacion");
                        Reservacion_Load(this, new EventArgs());
                        m_registrando = false;
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Checks checkoutform = new Checks();
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
            textBox4.Text = l_reservacion.Anticipo.ToString();

            dataGridView2.Rows.Clear();
            foreach (EntHabitacion _hab in (new EnlaceDB()).getHabitacionesHotel(comboBox3.Text))
            {
                if (_hab.Reservacion != m_reservacionActual.CodReservacion) { continue; }

                dataGridView2.Rows.Add();
                DataGridViewRow l_row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                l_row.Cells["Tipo"].Value = _hab.TipoHabitacionNombre;
                l_row.Cells["Numero"].Value = _hab.NoHabitacion;
                l_row.Cells["Camas"].Value = _hab.Camas;
                l_row.Cells["Clientes"].Value = _hab.Clientes;
                l_row.Cells["Precio"].Value = _hab.Precio;
                l_row.Cells["Hospedaje"].Value = _hab.Hospedaje;
                l_row.Cells["Id"].Value = _hab.Codigo;
            }
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

            Checks l_checks = new Checks();
            l_checks.ShowDialog(this);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (DateTime.Now.Date != m_reservacionActual.Salida.Date)
            {
                MessageBox.Show(this, "No estás en una fecha valida para realizar esta acción.", "Advertencia");
                return;
            }
            Checks l_checkout = new Checks();
            l_checkout.ShowDialog(this);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
