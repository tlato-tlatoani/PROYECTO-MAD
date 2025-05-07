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
    }
}
