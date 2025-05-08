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

namespace PROYECTO_MAD.Resources
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InicioDeSesion iniciosesionform = new InicioDeSesion();
            iniciosesionform.Show();
            this.Close();
        }

        private void Registro_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usuario l_usuario = new Usuario(
                int.Parse(textBox6.Text),
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
            if (l_enlace.Registrar(l_usuario, false))
            {
                Pantalla_principal pantallaprincipalform = new Pantalla_principal();
                pantallaprincipalform.Show();
                this.Close();
            }
        }
    }
}
