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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PROYECTO_MAD
{
    public partial class Clientes : Form
    {
        public List<EntClientes> m_clientes;
        public bool m_registrando = false;
        public bool m_editando = false;
        public string m_clienteActual = "";

        public Clientes()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            m_clientes = new EnlaceDB().getClientes();

            dataGridView1.Rows.Clear();
            foreach (EntClientes _cliente in m_clientes) {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["RFC"].Value = _cliente.RFC;
                l_row.Cells["Nombre"].Value = _cliente.Nombre;
                l_row.Cells["Correo"].Value = _cliente.CorreoElectronico;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
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

        private void verServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Servicios serviciosform = new Servicios();
            serviciosform.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string l_busqueda = textBox9.Text;
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow _row in dataGridView1.Rows)
            {
                if (!_row.Cells["Correo"].Value.ToString().TrimEnd().Equals(l_busqueda)) { continue; }
                _row.Selected = true;
                dataGridView1_Click(dataGridView1, new DataGridViewCellEventArgs(0, 0));
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e) {
            if (!m_registrando) {
                m_registrando = true;
                m_clienteActual = "";

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Registraras un Cliente.", "Informacion");
            } else
            {
                if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Correo Electronico.", "Validacion"); return; }
                if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Nombre.", "Validacion"); return; }
                if (textBox11.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Paterno.", "Validacion"); return; }
                if (textBox10.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Materno.", "Validacion"); return; }
                if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar una Ciudad.", "Validacion"); return; }
                if (textBox3.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Estado.", "Validacion"); return; }
                if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Pais.", "Validacion"); return; }
                if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un RFC.", "Validacion"); return; }
                if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono de Casa.", "Validacion"); return; }
                if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono Celular.", "Validacion"); return; }
                if (comboBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Seleccionar un Estado Civil.", "Validacion"); return; }

                if (dateTimePicker1.Value.Year < DateTime.Now.Year - 100 || dateTimePicker1.Value.Year > DateTime.Now.Year) { MessageBox.Show(this, "Debe Colocar una Fecha de Nacimiento Valida.", "Validacion"); return; }
                if (dateTimePicker1.Value.Year > DateTime.Now.Year - 17) { MessageBox.Show(this, "Debe ser Mayor de Edad.", "Validacion"); return; }
                           
                DialogResult l_editar = MessageBox.Show(this, "Quieres Registrar este Cliente?", "Advertencia", MessageBoxButtons.YesNo);

                if (l_editar == DialogResult.Yes) {
                    EntClientes l_cliente = new EntClientes(
                        textBox5.Text,
                        textBox1.Text,
                        textBox11.Text,
                        textBox10.Text,
                        textBox2.Text,
                        textBox3.Text,
                        textBox4.Text,
                        textBox8.Text,
                        textBox7.Text,
                        textBox6.Text,
                        dateTimePicker1.Value,
                        comboBox1.Text
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.RegistrarCliente(l_cliente, Program.m_usuario.NoNomina)) {
                        MessageBox.Show(this, "Cliente Registrado con Exito.", "Informacion");
                        Clientes_Load(this, new EventArgs());
                        m_registrando = false;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void verEditarRegistrarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Program.m_usuario.TipoUsuario) { MessageBox.Show(this, "Necesita ser Administrador para Navegar a esta Ventana", "Advertencia"); return; }

            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void miPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            EntClientes l_cliente = null;

            foreach (EntClientes _cliente in m_clientes)
            {
                if (!dataGridView1.SelectedCells[0].Value.ToString().Equals(_cliente.RFC)) { continue; }
                l_cliente = _cliente;

                break;
            }

            if (l_cliente == null) { return; }

            m_clienteActual = l_cliente.RFC;

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

            textBox8.Text = l_cliente.CorreoElectronico.Trim();
            textBox1.Text = l_cliente.Nombre.Trim();
            textBox11.Text = l_cliente.ApellidoPaterno.Trim();
            textBox10.Text = l_cliente.ApellidoMaterno.Trim();
            dateTimePicker1.Value = l_cliente.FechaNacimiento;
            textBox5.Text = l_cliente.RFC.Trim();
            textBox2.Text = l_cliente.Ciudad.Trim();
            textBox3.Text = l_cliente.Estado.Trim();
            textBox4.Text = l_cliente.Pais.Trim();
            textBox6.Text = l_cliente.TelCasa.Trim();
            textBox7.Text = l_cliente.TelCelular.Trim();
            comboBox1.Text = l_cliente.EstadoCivil.Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!m_editando)
            {
                if (m_clienteActual.Length <= 0)
                {
                    MessageBox.Show(this, "Necesitas Seleccionar un Cliente para Realizar esta Accion.", "Advertencia");
                    return;
                }

                m_editando = true;

                MessageBox.Show(this, "Estas en Modo Edicion.\nSi Presionas este Boton de Nuevo Editaras el Cliente Seleccionado.", "Informacion");
            }
            else
            {
                if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Correo Electronico.", "Validacion"); return; }
                if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Nombre.", "Validacion"); return; }
                if (textBox11.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Paterno.", "Validacion"); return; }
                if (textBox10.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Materno.", "Validacion"); return; }
                if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar una Ciudad.", "Validacion"); return; }
                if (textBox3.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Estado.", "Validacion"); return; }
                if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Pais.", "Validacion"); return; }
                if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un RFC.", "Validacion"); return; }
                if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono de Casa.", "Validacion"); return; }
                if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono Celular.", "Validacion"); return; }
                if (comboBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Seleccionar un Estado Civil.", "Validacion"); return; }

                if (dateTimePicker1.Value.Year < DateTime.Now.Year - 100 || dateTimePicker1.Value.Year > DateTime.Now.Year) { MessageBox.Show(this, "Debe Colocar una Fecha de Nacimiento Valida.", "Validacion"); return; }
                if (dateTimePicker1.Value.Year > DateTime.Now.Year - 17) { MessageBox.Show(this, "Debe ser Mayor de Edad.", "Validacion"); return; }

                DialogResult l_editar = MessageBox.Show(this, "Quieres Editar este Cliente?", "Advertencia", MessageBoxButtons.YesNo);
                if (l_editar == DialogResult.Yes)
                {
                    EntClientes l_cliente = new EntClientes(
                        textBox5.Text,
                        textBox1.Text,
                        textBox11.Text,
                        textBox10.Text,
                        textBox2.Text,
                        textBox3.Text,
                        textBox4.Text,
                        textBox8.Text,
                        textBox7.Text,
                        textBox6.Text,
                        dateTimePicker1.Value,
                        comboBox1.Text
                    );

                    EnlaceDB l_enlace = new EnlaceDB();
                    if (l_enlace.EditarCliente(l_cliente, Program.m_usuario.NoNomina))
                    {
                        MessageBox.Show(this, "Cliente Editado con Exito.", "Informacion");
                        Clientes_Load(this, new EventArgs());
                        m_editando = false;
                    }
                }
            }
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
