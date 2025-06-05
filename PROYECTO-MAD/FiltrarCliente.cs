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
    public partial class FiltrarCliente : Form
    {
        public FiltrarCliente()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EntClientes l_rfc = null;
            switch (comboBox1.SelectedIndex) {
                case 0: {
                    l_rfc = new EnlaceDB().BuscarClientePorCorreo(textBox1.Text);
                } break;
                case 1:
                    {
                        l_rfc = new EnlaceDB().BuscarClientePorRFC(textBox1.Text);
                    }
                    break;
                case 2:
                    {
                        l_rfc = new EnlaceDB().BuscarClientePorApellidos(textBox1.Text, textBox2.Text);
                    }
                    break;
            }

            if (l_rfc == null) { MessageBox.Show("El Cliente que intentanste Buscar no Existe.", "Advertencia"); return; }

            Reservacion.m_cliente = l_rfc;

            Reservacion reservacionform = new Reservacion();
            reservacionform.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Pantalla_principal reservacionform = new Pantalla_principal();
            reservacionform.Show();
            this.Close();
        }

        private void FiltrarCliente_Load(object sender, EventArgs e)
        {

        }
    }
}
