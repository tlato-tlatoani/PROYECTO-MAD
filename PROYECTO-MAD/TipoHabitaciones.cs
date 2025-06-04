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

namespace PROYECTO_MAD
{
    public partial class TIPO_DE_HAB : Form
    {
        public List<TipoHab> m_tiposHabitaciones;
        public List<Hotel> m_hoteles;
        public bool m_registrando = false;
        public bool m_editando = false;
        public int m_actual = 0;

        public TIPO_DE_HAB()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TIPO_DE_HAB_Load(object sender, EventArgs e)
        {
            m_hoteles = new EnlaceDB().getHoteles();
            m_tiposHabitaciones = new EnlaceDB().getTiposHabitaciones();

            comboBox2.Items.Clear();
            foreach (Hotel _hotel in m_hoteles)
            {
                comboBox2.Items.Add(_hotel.NombreHotel);
            }


            dataGridView1.Rows.Clear();
            foreach (TipoHab _tipoHab in m_tiposHabitaciones)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["Codigo"].Value = _tipoHab.CodTDH;
                l_row.Cells["Nivel"].Value = _tipoHab.NivelHabitacion;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void reservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrarCliente identificateform = new FiltrarCliente();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_registrando)
            {
                m_registrando = true;
                m_editando = false;
                m_actual = -1;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Registraras un Tipo de Habitacion.", "Informacion");
            }
            else
            {
                if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar el Nivel de Habitacion.", "Validacion"); return; }
                if (comboBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Elegir el Hotel.", "Validacion"); return; }
                if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar el Numero de Camas.", "Validacion"); return; }
                if (textBox6.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar la Cantidad de Personas por Habitacion.", "Validacion"); return; }
                if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar el Precio de Noche por Persona.", "Validacion"); return; }
                if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar la Localizacion.", "Validacion"); return; }
                if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar las Amenidades.", "Validacion"); return; }
                if (listBox1.SelectedItems.Count <= 0) { MessageBox.Show(this, "Debe Elegir almenos un Tipo de Cama.", "Validacion"); return; }

                int l_camas = -1;
                if (!int.TryParse(textBox1.Text, out l_camas)) { MessageBox.Show(this, "El Numero de Camas debe ser un Entero Valido.", "Validacion"); return; }

                int l_personas = -1;
                if (!int.TryParse(textBox6.Text, out l_personas)) { MessageBox.Show(this, "La Cantidad de Personas debe ser un Entero Valido.", "Validacion"); return; }

                decimal l_precio = -1;
                if (!decimal.TryParse(textBox4.Text, out l_precio)) { MessageBox.Show(this, "El Precio de Persona por Noche debe ser un Decimal Valido.", "Validacion"); return; }

                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar este Tipo de Habitacion?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    string l_tipoCamas = "";
                    foreach (string _cama in listBox1.SelectedItems) { l_tipoCamas += _cama + ","; }
                    if (l_tipoCamas.Length > 0) { l_tipoCamas.Remove(l_tipoCamas.Length - 1); }
                    
                    TipoHab l_tipoHab = new TipoHab(
                        0,
                        textBox2.Text,
                        l_camas,
                        l_tipoCamas,
                        l_personas,
                        textBox7.Text,
                        textBox5.Text,
                        comboBox2.Text,
                        l_precio
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.RegistrarTipoHabitacion(l_tipoHab, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Tipo de Habitacion Registrado con Exito.", "Informacion");
                        TIPO_DE_HAB_Load(this, new EventArgs());
                        m_registrando = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!m_editando)
            {
                if (m_actual < 0)
                {
                    MessageBox.Show(this, "Necesitas Seleccionar un Tipo de Habitacion para Realizar esta Accion.", "Advertencia");
                    return;
                }

                m_editando = true;

                MessageBox.Show(this, "Estas en Modo Edicion.\nSi Presionas este Boton de Nuevo Editaras el Tipo de Habitacion Seleccionado.", "Informacion");
            }
            else
            {
                if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar el Nivel de Habitacion.", "Validacion"); return; }
                if (comboBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Elegir el Hotel.", "Validacion"); return; }
                if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar el Numero de Camas.", "Validacion"); return; }
                if (textBox6.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar la Cantidad de Personas por Habitacion.", "Validacion"); return; }
                if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar el Precio de Noche por Persona.", "Validacion"); return; }
                if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar la Localizacion.", "Validacion"); return; }
                if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar las Amenidades.", "Validacion"); return; }
                if (listBox1.SelectedItems.Count <= 0) { MessageBox.Show(this, "Debe Elegir almenos un Tipo de Cama.", "Validacion"); return; }

                int l_camas = -1;
                if (!int.TryParse(textBox1.Text, out l_camas)) { MessageBox.Show(this, "El Numero de Camas debe ser un Entero Valido.", "Validacion"); return; }

                int l_personas = -1;
                if (!int.TryParse(textBox6.Text, out l_personas)) { MessageBox.Show(this, "La Cantidad de Personas debe ser un Entero Valido.", "Validacion"); return; }

                decimal l_precio = -1;
                if (!decimal.TryParse(textBox4.Text, out l_precio)) { MessageBox.Show(this, "El Precio de Persona por Noche debe ser un Decimal Valido.", "Validacion"); return; }

                DialogResult l_editar = MessageBox.Show(this, "Quieres Editar este Tipo de Habitacion?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    string l_tipoCamas = "";
                    foreach (string _cama in listBox1.SelectedItems) { l_tipoCamas += _cama + ","; }
                    if (l_tipoCamas.Length > 0) { l_tipoCamas.Remove(l_tipoCamas.Length - 1); }

                    TipoHab l_tipoHab = new TipoHab(
                        int.Parse(textBox3.Text),
                        textBox2.Text,
                        l_camas,
                        l_tipoCamas,
                        l_personas,
                        textBox7.Text,
                        textBox5.Text,
                        comboBox2.Text,
                        l_precio
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.EditarTipoHabitacion(l_tipoHab, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Cliente Editado con Exito.", "Informacion");
                        TIPO_DE_HAB_Load(this, new EventArgs());
                        m_editando = false;
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            TipoHab l_TipoHab = null;

            foreach (TipoHab _TipoHab in m_tiposHabitaciones)
            {
                if (!dataGridView1.SelectedCells[0].Value.ToString().Equals(_TipoHab.CodTDH.ToString())) { continue; }
                l_TipoHab = _TipoHab;

                break;
            }

            if (l_TipoHab == null) { return; }

            m_actual = l_TipoHab.CodTDH;

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

            textBox2.Text = l_TipoHab.NivelHabitacion;
            textBox3.Text = l_TipoHab.CodTDH.ToString();
            comboBox2.Text = l_TipoHab.nombreHotel;
            textBox1.Text = l_TipoHab.NoCamas.ToString();
            textBox4.Text = l_TipoHab.PrecioNoche.ToString();
            textBox6.Text = l_TipoHab.CantPersonasMax.ToString();
            textBox7.Text = l_TipoHab.Locacion;
            textBox5.Text = l_TipoHab.Amenidades;

            listBox1.ClearSelected();
            foreach (string _cama in l_TipoHab.TipoCama.Split(',')) {
                listBox1.SelectedItems.Add(_cama);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l_busqueda = textBox9.Text;
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow _row in dataGridView1.Rows)
            {
                if (!_row.Cells["Codigo"].Value.ToString().TrimEnd().Equals(l_busqueda)) { continue; }
                _row.Selected = true;
                dataGridView1_Click(dataGridView1, new DataGridViewCellEventArgs(0, 0));
                return;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
