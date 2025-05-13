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
    public partial class Servicios : Form
    {
        public List<EntServicios> m_servicios;
        EntServicios m_curServicio = null;

        public bool m_registrando = false;
        public bool m_editando = false;
        public int m_actual = 0;
        public Servicios()
        {
            InitializeComponent();
        }

        private void verServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicios serviciosform = new Servicios();
            serviciosform.Show();
            this.Close();
        }

        private void verPerfilToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (!m_registrando)
            {
                m_registrando = true;
                m_editando = false;
                m_actual = -1;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Registraras un Servicio.", "Informacion");
            }
            else
            {
                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar este Servicio?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    EntServicios l_servicio = new EntServicios(
                        0,
                        textBox9.Text,
                        decimal.Parse(textBox6.Text),
                        richTextBox1.Text
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.RegistrarServicio(l_servicio, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Servicio Registrado con Exito.", "Informacion");
                        Servicios_Load(this, new EventArgs());
                        m_registrando = false;
                    }
                }
            }
        }

        private void Servicios_Load(object sender, EventArgs e)
        {
            m_servicios = new EnlaceDB().getServicios();

            dataGridView1.Rows.Clear();
            foreach (EntServicios _tipoHab in m_servicios)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["Codigo"].Value = _tipoHab.CodServicio;
                l_row.Cells["Nombre"].Value = _tipoHab.Nombre;
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
                    EntServicios l_servicio = new EntServicios(
                        m_curServicio.CodServicio,
                        textBox9.Text,
                        decimal.Parse(textBox6.Text),
                        richTextBox1.Text
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.EditarServicio(l_servicio, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Cliente Editado con Exito.", "Informacion");
                        Servicios_Load(this, new EventArgs());
                        m_editando = false;
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            m_curServicio = null;

            foreach (EntServicios _TipoHab in m_servicios)
            {
                if (!dataGridView1.SelectedCells[0].Value.ToString().Equals(_TipoHab.CodServicio.ToString())) { continue; }
                m_curServicio = _TipoHab;

                break;
            }

            if (m_curServicio == null) { return; }

            m_actual = m_curServicio.CodServicio;

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

            textBox9.Text = m_curServicio.Nombre;
            textBox6.Text = m_curServicio.Precio.ToString();
            richTextBox1.Text = m_curServicio.Descripcion;
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
    }
}
