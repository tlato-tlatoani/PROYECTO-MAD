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
    public partial class Usuarios : Form
    {
        public List<Usuario> m_usuarios;

        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            m_usuarios = new EnlaceDB().getUsuarios();

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

        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void usuarioToolStripMenuItem1_Click(object sender, EventArgs e)
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
    }
}
