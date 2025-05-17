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
    public partial class Form2 : Form
    {
        public List<EntServicios> m_servicios;

        public Form2()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 identificateform = new Form1();
            identificateform.Show();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            m_servicios = new EnlaceDB().getServicios();

            listBox1.Items.Clear();
            foreach (EntServicios _serv in m_servicios) {
                listBox1.Items.Add(_serv.Nombre + ": " + _serv.Precio + "$");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            DialogResult l_editar = MessageBox.Show(this, "Quieres realizar el Check Out de Esta Reservacion?", "Advertencia", MessageBoxButtons.YesNo);
            if (l_editar == DialogResult.Yes)
            {
                string l_servicios = "";
                foreach (string _serv in listBox1.SelectedItems)
                {
                    l_servicios += _serv.Split(':')[0] + ",";
                }
                l_servicios = l_servicios.Remove(l_servicios.Length - 1);

                new EnlaceDB().CheckOut(Reservacion.m_actual, dateTimePicker3.Value, decimal.Parse(textBox6.Text), textBox1.Text, Program.m_usuario.NoNomina);
                new EnlaceDB().FacturarServicios(Reservacion.m_actual, l_servicios);

                MessageBox.Show(this, "Check Out Realizada.", "Informacion");
            }
        }
    }
}
