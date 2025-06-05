using PROYECTO_MAD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_MAD
{
    public partial class Usuarios : Form
    {
        public List<Usuario> m_usuarios;
        public bool m_registrando = false;
        public bool m_editando = false;
        public int m_usuarioActual = -1;
        Usuario m_curUsuario;

        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            m_usuarios = new EnlaceDB().getUsuarios();

            dataGridView1.Rows.Clear();
            foreach (Usuario _usuario in m_usuarios)
            {
                dataGridView1.Rows.Add();
                DataGridViewRow l_row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                l_row.Cells["NoNomina"].Value = _usuario.NoNomina;
                l_row.Cells["Nombre"].Value = _usuario.Nombre;
                l_row.Cells["ApellidoPaterno"].Value = _usuario.ApellidoPaterno;
                l_row.Cells["CorreoElectronico"].Value = _usuario.CorreoElectronico;
                l_row.Cells["TelCelular"].Value = _usuario.TelCelular;
                l_row.Cells["TelCasa"].Value = _usuario.TelCasa;
                l_row.Cells["FechaNacimiento"].Value = _usuario.FechaNacimiento;
                l_row.Cells["TipoUsuario"].Value = _usuario.TipoUsuario;
                l_row.Cells["Estado"].Value = _usuario.Estado;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_registrando) {
                m_registrando = true;
                m_usuarioActual = -1;

                MessageBox.Show(this, "Estas en Modo Registro.\nSi Presionas este Boton de Nuevo Registraras un Usuario.", "Informacion");
            } else {
                if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Correo Electronico.", "Validacion"); return; }
                if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar una Contraseña.", "Validacion"); return; }
                if (textBox3.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Nombre.", "Validacion"); return; }
                if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono de Casa.", "Validacion"); return; }
                if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Paterno.", "Validacion"); return; }
                if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Materno.", "Validacion"); return; }
                if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono Celular.", "Validacion"); return; }
                if (textBox6.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Numero de Nomina.", "Validacion"); return; }

                if (!Regex.IsMatch(textBox1.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$")) {
                    MessageBox.Show(this, "La contraseña debe tener minimo 8 caracteres, 1 caracter especial, 1 minúscula y 1 mayúsucula", "Formato Incorrecto");
                    return;
                }

                int l_nomina = -1;
                if (!int.TryParse(textBox6.Text, out l_nomina)) { MessageBox.Show(this, "El Numero de Nomina debe ser un Numero Valido.", "Validacion"); return; }

                if (dateTimePicker1.Value.Year < DateTime.Now.Year - 100 || dateTimePicker1.Value.Year > DateTime.Now.Year) { MessageBox.Show(this, "Debe Colocar una Fecha de Nacimiento Valida.", "Validacion"); return; }
                if (dateTimePicker1.Value.Year > DateTime.Now.Year - 17) { MessageBox.Show(this, "Debe ser Mayor de Edad.", "Validacion"); return; }

                if (MessageBox.Show(this, "Quiere Registrarse con estos Datos?", "Informacion", MessageBoxButtons.YesNo) != DialogResult.Yes) { return; }

                Usuario l_usuario = new Usuario(
                    l_nomina,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text,
                    textBox2.Text,
                    textBox1.Text,
                    textBox8.Text,
                    textBox7.Text,
                    dateTimePicker1.Value,
                    radioButton1.Checked
                );

                EnlaceDB l_enlace = new EnlaceDB();
                if (l_enlace.Registrar(l_usuario, true)) {
                    MessageBox.Show(this, "Usuario Registrado Correctamente!!!", "Informacion");
                    Usuarios_Load(this, new EventArgs());
                    m_registrando = false;
                } else {
                    MessageBox.Show(this, "El Correo que intenta colocar ya se encuentra en uso...", "Error");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (!m_editando) {
                if (m_usuarioActual <= 0) {
                    MessageBox.Show(this, "Necesitas Seleccionar un Usuario para Realizar esta Accion.", "Advertencia");
                    return;
                }

                m_editando = true;

                MessageBox.Show(this, "Estas en Modo Edicion.\nSi Presionas este Boton de Nuevo Editaras el Usuario Seleccionado.", "Informacion");

                textBox6.Enabled = false;
            } else {
                if (textBox2.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Correo Electronico.", "Validacion"); return; }
                if (textBox7.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono de Casa.", "Validacion"); return; }
                if (textBox4.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Paterno.", "Validacion"); return; }
                if (textBox5.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Apellido Materno.", "Validacion"); return; }
                if (textBox8.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Telefono Celular.", "Validacion"); return; }
                if (textBox3.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar un Nombre.", "Validacion"); return; }

                if (dateTimePicker1.Value.Year < DateTime.Now.Year - 100 || dateTimePicker1.Value.Year > DateTime.Now.Year) { MessageBox.Show(this, "Debe Colocar una Fecha de Nacimiento Valida.", "Validacion"); return; }
                if (dateTimePicker1.Value.Year > DateTime.Now.Year - 17) { MessageBox.Show(this, "Debe ser Mayor de Edad.", "Validacion"); return; }

                if (MessageBox.Show(this, "Quieres Editar este Usuario?", "Informacion", MessageBoxButtons.YesNo) != DialogResult.Yes) { return; }

                Usuario l_usuario = new Usuario(
                    int.Parse(textBox6.Text),
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text,
                    textBox2.Text,
                    "",
                    textBox8.Text,
                    textBox7.Text,
                    dateTimePicker1.Value,
                    radioButton1.Checked
                );

                l_usuario.Estado = comboBox1.SelectedIndex == 1;

                EnlaceDB l_enlace = new EnlaceDB();
                if (l_enlace.Editar(l_usuario)) {
                    MessageBox.Show(this, "Usuario Editado con Exito.", "Informacion");
                    Usuarios_Load(this, new EventArgs());
                    m_editando = false;
                    textBox6.Enabled = true;
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click_1(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void miPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Perfil usuariosform = new Perfil();
            usuariosform.Show();
            this.Close();
        }

        private void verEditarRegistrarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuariosform = new Usuarios();
            usuariosform.Show();
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

        private void verServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicios serviciosform = new Servicios();
            serviciosform.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            string l_busqueda = textBox9.Text;

            dataGridView1.ClearSelection();
            foreach (DataGridViewRow _row in dataGridView1.Rows) {
                if (!_row.Cells["NoNomina"].Value.ToString().Equals(l_busqueda)) { continue; }
                _row.Selected = true;
                dataGridView1_Click(dataGridView1, new DataGridViewCellEventArgs(0, 0));
                return;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Usuario l_usuario = null;

            foreach (Usuario _usuario in m_usuarios) { 
                if (!dataGridView1.SelectedCells[0].Value.ToString().Equals(_usuario.NoNomina.ToString())) { continue; }
                l_usuario = _usuario;

                break;
            }

            if (l_usuario == null) { return; }
            m_curUsuario = l_usuario;

            m_usuarioActual = l_usuario.NoNomina;

            if (m_registrando) {
                m_registrando = false;
                textBox1.Text = "";

                MessageBox.Show(this, "Has salido del Modo Registro", "Informacion");
            }
            if (m_editando)
            {
                m_editando = false;
                textBox6.Enabled = true;

                MessageBox.Show(this, "Has salido del Modo Edicion", "Informacion");
            }

            textBox2.Text = l_usuario.CorreoElectronico;
            textBox3.Text = l_usuario.Nombre;
            textBox4.Text = l_usuario.ApellidoPaterno;
            textBox5.Text = l_usuario.ApellidoMaterno;
            dateTimePicker1.Value = l_usuario.FechaNacimiento;
            textBox6.Text = l_usuario.NoNomina.ToString();
            textBox7.Text = l_usuario.TelCasa;
            textBox8.Text = l_usuario.TelCelular;
            radioButton1.Checked = l_usuario.TipoUsuario;
            radioButton2.Checked = !l_usuario.TipoUsuario;
            comboBox1.SelectedIndex = l_usuario.Estado ? 1 : 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) { MessageBox.Show(this, "Debe Colocar una Contraseña.", "Validacion"); return; }

            if (!Regex.IsMatch(textBox1.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$")) {
                MessageBox.Show(this, "La contraseña debe tener minimo 8 caracteres, 1 caracter especial, 1 minúscula y 1 mayúsucula", "Formato Incorrecto");
                return;
            }

            if (MessageBox.Show(this, "Quieres Actualizar la Contraseña de este Usuario?", "Informacion", MessageBoxButtons.YesNo) != DialogResult.Yes) { return; }

            EnlaceDB l_enlace = new EnlaceDB();
            if (l_enlace.ActualizarContra(int.Parse(textBox6.Text), textBox1.Text)) {
                MessageBox.Show(this, "Contraseña Actualizada con Exito.", "Informacion");
            }
        }

        private void verReservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiltrarCliente identificateform = new FiltrarCliente();
            identificateform.Show();
            this.Close();
        }
    }
}
