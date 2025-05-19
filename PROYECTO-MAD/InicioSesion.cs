using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROYECTO_MAD.Resources;

namespace PROYECTO_MAD
{
    public partial class InicioDeSesion : Form
    {
        public static InicioDeSesion m_instance;
        public InicioDeSesion()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnlaceDB enlace = new EnlaceDB();
            bool l_tipoUsuario = radioButton1.Checked;
            if ((Program.m_usuario = enlace.Autentificar(textBox1.Text, textBox2.Text, l_tipoUsuario)) != null)
            {
                Pantalla_principal pantallaprincipalform = new Pantalla_principal();
                pantallaprincipalform.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registro registroform = new Registro();
            registroform.Show();
            this.Hide();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void InicioDeSesion_Load(object sender, EventArgs e)
        {
            m_instance = this;
        }
    }
}
