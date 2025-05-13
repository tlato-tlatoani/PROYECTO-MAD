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
    public partial class Habitacion : Form
    {
        public List<TipoHab> m_tiposHabitaciones;
        public List<EntHabitacion> m_habitaciones;

        public bool m_registrando = false;
        public bool m_editando = false;
        public int m_actual = 0;
        public Habitacion()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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

        private void Habitacion_Load(object sender, EventArgs e)
        {
            m_tiposHabitaciones = new EnlaceDB().getTiposHabitaciones();
            m_habitaciones = new EnlaceDB().getHabitaciones();

            comboBox1.Items.Clear();
            foreach (TipoHab _tipoHab in m_tiposHabitaciones)
            {
                comboBox1.Items.Add(_tipoHab.NivelHabitacion);
            }

            dataGridView1.Rows.Clear();
            foreach (EntHabitacion _tipoHab in m_habitaciones)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["Hotel"].Value = _tipoHab.NombreHotel;
                l_row.Cells["Numero"].Value = _tipoHab.NoHabitacion;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!m_registrando)
            {
                m_registrando = true;
                m_editando = false;
                m_actual = -1;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Registraras una Habitacion.", "Informacion");
            }
            else
            {
                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar esta Habitacion?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    EntHabitacion l_habitacion = new EntHabitacion(
                        int.Parse(textBox2.Text),
                        comboBox2.Text,
                        int.Parse(textBox6.Text),
                        comboBox1.Text,
                        ""
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.RegistrarHabitacion(l_habitacion, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Habitacion Registrada con Exito.", "Informacion");
                        Habitacion_Load(this, new EventArgs());
                        m_registrando = false;
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            EntHabitacion l_TipoHab = null;

            foreach (EntHabitacion _TipoHab in m_habitaciones)
            {
                if (!dataGridView1.SelectedCells[1].Value.ToString().Equals(_TipoHab.NoHabitacion.ToString())) { continue; }
                l_TipoHab = _TipoHab;

                break;
            }

            if (l_TipoHab == null) { return; }

            m_actual = l_TipoHab.NoHabitacion;

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

            textBox2.Text = l_TipoHab.NoHabitacion.ToString();
            comboBox1.Text = l_TipoHab.TipoHabitacionNombre;
            textBox6.Text = l_TipoHab.Piso.ToString();
            comboBox2.Text = l_TipoHab.Estatus;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l_busqueda = textBox9.Text;
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow _row in dataGridView1.Rows)
            {
                if (!_row.Cells["Numero"].Value.ToString().TrimEnd().Equals(l_busqueda)) { continue; }
                _row.Selected = true;
                dataGridView1_Click(dataGridView1, new DataGridViewCellEventArgs(0, 0));
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!m_editando)
            {
                if (m_actual < 0)
                {
                    MessageBox.Show(this, "Necesitas Seleccionar una Habitacion para Realizar esta Accion.", "Advertencia");
                    return;
                }

                m_editando = true;

                MessageBox.Show(this, "Estas en Modo Edicion.\nSi Presionas este Boton de Nuevo Editaras la Habitacion Seleccionada.", "Informacion");
            }
            else
            {
                DialogResult l_editar = MessageBox.Show(this, "Quieres Editar esta Habitacion?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    EntHabitacion l_habitacion = new EntHabitacion(
                        int.Parse(textBox2.Text),
                        comboBox2.Text,
                        int.Parse(textBox6.Text),
                        comboBox1.Text,
                        ""
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.EditarHabitacion(l_habitacion, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Cliente Editado con Exito.", "Informacion");
                        Habitacion_Load(this, new EventArgs());
                        m_editando = false;
                    }
                }
            }
        }
    }
}
